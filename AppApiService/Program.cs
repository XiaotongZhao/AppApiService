using AppApiService.Common;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using AppApiService.Domain.Common;
using AppApiService.Infrastructure.Common;
using AppApiService.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigureLogging();
builder.Host.UseSerilog();
var allowAnyOrigins = "allowAnyOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAnyOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin()
                          .AllowAnyHeader()
                          .AllowAnyMethod();
                      });
});
// Add services to the container.
builder.Services.AddDbContext<EFContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerDBConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
IoCConfig.ImplementDIByScanLibrary(builder.Services, new[] { "AppApiService.Domain" });
builder.Services.AddControllers(controller => 
{
    controller.Filters.Add<LogFunActionFilter>();
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EFContext>();
    db.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowAnyOrigins);

app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "Deploy appApiService successfull!");

app.Run();

void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{environment}.json",
            optional: true)
        .Build();

    var elasticAddress = configuration["ElasticConfiguration:Uri"];
    var userName = configuration["ElasticConfiguration:UserName"] ?? string.Empty;
    var password = configuration["ElasticConfiguration:PassWord"] ?? string.Empty;
    if (!string.IsNullOrEmpty(elasticAddress) && !string.IsNullOrEmpty(environment))
    {
        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.Elasticsearch(ConfigureElasticSink(elasticAddress, userName, password, environment))
            .Enrich.WithProperty("Environment", environment)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
}

ElasticsearchSinkOptions ConfigureElasticSink(string elasticAddress, string userName, string password, string environment)
{
    var index = $"{Assembly.GetExecutingAssembly()?.GetName()?.Name?.ToLower()}-{environment?.ToLower()}-{DateTime.UtcNow:yyyy-MM}";
    return new ElasticsearchSinkOptions(new Uri(elasticAddress))
    {
        AutoRegisterTemplate = true,
        IndexFormat = index,
        ModifyConnectionSettings = connection =>
        {
            if(!string.IsNullOrEmpty(userName) && !string.IsNullOrEmpty(password))
                connection.BasicAuthentication(userName, password);
            return connection;
        }
    };
}
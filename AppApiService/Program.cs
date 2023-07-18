using AppApiService.Domain.Common;
using AppApiService.Infrastructure.Common;
using AppApiService.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;
using Serilog.Sinks.Elasticsearch;
using Serilog;
using System.Reflection;
using Serilog.Exceptions;

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
builder.Services.AddControllers();
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
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();

    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}
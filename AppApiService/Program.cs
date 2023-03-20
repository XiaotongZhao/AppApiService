using AppApiService.Common;
using AppApiService.Domain.Common;
using AppApiService.Infrastructure.Common;
using AppApiService.Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
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
var seq = builder.Configuration.GetSection("Seq");
builder.Services.AddLogging(loggingBuilder =>
{
    loggingBuilder.AddSeq(seq);
});
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

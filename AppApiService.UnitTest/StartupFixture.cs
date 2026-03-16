using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AppApiService.UnitTest;

public class StartupFixture : IDisposable
{
    public IServiceProvider ServiceProvider { get; set; }
    public StartupFixture()
    {
        var services = new ServiceCollection();
        implementDIByScanLibrary(services);
        ServiceProvider = services.BuildServiceProvider();
    }

    public void Dispose()
    {
    }

    public void implementDIByScanLibrary(ServiceCollection services)
    {
        var interfacesAndImplementClasses = Assembly.Load("AppApiService.Domain").GetTypes()
            .Where(type => type.IsClass && type.GetInterfaces().Length > 0)
            .Select(type => new { ImplementInterfaceClass = type, Interfaces = type.GetInterfaces().ToList() }).ToList();

        foreach (var interfaceClass in interfacesAndImplementClasses)
        {
            interfaceClass.Interfaces.ForEach(Interface =>
            {
                services.AddScoped(Interface, interfaceClass.ImplementInterfaceClass);
            });
        }

    }
}
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace AppApiService.Infrastructure.Common;

public class IoCConfig
{
    public static void ImplementDIByScanLibrary(IServiceCollection services, string[] projects)
    {
        foreach (var project in projects)
        {
            var interfacesAndImplementClasses = Assembly.Load(project).GetTypes()
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
}

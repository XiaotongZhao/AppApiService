namespace AppApiService.Domain.DevOps.ServiceTask;

public interface IServiceTask
{
    Task<bool> CreateServerService(Service service);
    Task<Service> GetServiceDetailById(int id);

}

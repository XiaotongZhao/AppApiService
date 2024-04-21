namespace AppApiService.Domain.DevOps;

public interface IServerService
{
    Task<bool> AddServer(string name, string description, string ipAddress, string userName, string password);
    Task<bool> RemoveServer(int id);
    Task<List<Server>> GetServers(string keyword);
    Task<bool> CheckServerIsAlive(int id);
}

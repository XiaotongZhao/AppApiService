namespace AppApiService.Domain.DevOps;

public interface IServerService
{
    Task<bool> AddServer(Server server);
    Task<bool> UpdateServer(Server server);
    Task<bool> RemoveServer(int id);
    IQueryable<Server> GetServers(string keyword);
    Task<Server> GetServerById(int id);
    Task<bool> CheckServerIsAlive(int id);
}

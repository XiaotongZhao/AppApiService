namespace AppApiService.Domain.DevOps;

public interface IServerService
{
    Task<List<ServerUploadFile>> GetServerUploadFilesByServerId(int serverId);
    Task<bool> AddServer(Server server);
    Task<bool> UpdateServerDetail(ServerDetail server);
    Task<bool> RemoveServer(int id);
    IQueryable<Server> GetServers(string keyword);
    Task<ServerDetail> GetServerDetailById(int id);
    Task CheckServerIsAlive(int id);
}

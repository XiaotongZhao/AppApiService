
using Renci.SshNet;

namespace AppApiService.Domain.DevOps;

public class ServerService : IServerService
{
    private readonly IUnitOfWork unitOfWork;

    public ServerService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task CheckServerIsAlive(int id)
    {
        var server = await unitOfWork.Get().Set<Server>().FindAsync(id);
        var errorMessage = string.Empty;
        if (server != null)
        {
            try
            {
                var userName = server.UserName;
                var password = server.Password;
                var ipAddress = server.IpAddress;
                var port = server.Port ?? 0;
                using (var client = new SshClient(ipAddress, port, userName, password))
                {
                    client.Connect();
                    server.IsConnect = client.IsConnected;
                }
            }
            catch (Exception ex)
            {
                errorMessage = ex.Message;
                server.IsConnect = false;
            }
            finally
            {
                await unitOfWork.Get().SaveChangesAsync();
            }
        }
        else
        {
            throw new Exception("server is not exist!");
        }
        if (!string.IsNullOrEmpty(errorMessage))
            throw new Exception(errorMessage);
    }

    public IQueryable<Server> GetServers(string keyword)
    {
        var query = unitOfWork.Get().Set<Server>().Where(a => a.Name.Contains(keyword) || a.IpAddress.Contains(keyword) || a.Description.Contains(keyword));
        return query;
    }

    public async Task<bool> RemoveServer(int id)
    {
        var server = await unitOfWork.Get().Set<Server>().FindAsync(id);
        if (server != null)
            server.IsDeleted = true;
        var influenceCount = await unitOfWork.Get().SaveChangesAsync();
        return influenceCount > 0;
    }

    public async Task<bool> AddServer(Server server)
    {
        await unitOfWork.Get().Set<Server>().AddAsync(server);
        var influenceCount = await unitOfWork.Get().SaveChangesAsync();
        return influenceCount > 0;
    }

    public async Task<bool> UpdateServerDetail(ServerDetail serverDetail)
    {
        unitOfWork.Get().Set<Server>().Update(serverDetail.Server);
        unitOfWork.Get().Set<ServerUploadFile>().UpdateRange(serverDetail.ServerUploadFiles);
        var influenceCount = await unitOfWork.Get().SaveChangesAsync();
        return influenceCount > 0;
    }

    public async Task<ServerDetail> GetServerDetailById(int id)
    {
        var serverDetail = new ServerDetail();
        var server = await unitOfWork.Get().Set<Server>().FindAsync(id);
        var serverUploadFiles = await unitOfWork.Get().Set<ServerUploadFile>().Where(a => a.ServerId == id).ToListAsync();
        if (server != null) 
        {
            serverDetail.Server = server;
            serverDetail.ServerUploadFiles = serverUploadFiles;
        }
        return serverDetail;
    }

    public async Task<ServerUploadFile> GetServerUploadFileById(int id)
    {
        var serverUploadFile = await unitOfWork.Get().Set<ServerUploadFile>().FindAsync(id);
        return serverUploadFile;
    }
}


namespace AppApiService.Domain.DevOps;

public class ServerService : IServerService
{
    private readonly IUnitOfWork unitOfWork;

    public ServerService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> AddServer(string name, string description, string ipAddress, string userName, string password)
    {
        {
            var server = new Server()
            {
                Name = name,
                Description = description,
                IpAddress = ipAddress,
                UserName = userName,
                Password = password
            };
            await unitOfWork.Get().Set<Server>().AddAsync(server);
            var influenceCount = await unitOfWork.Get().SaveChangesAsync();
            return influenceCount > 0;
        }
    }

    public Task<bool> CheckServerIsAlive(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<List<Server>> GetServers(string keyword)
    {
        var query = string.IsNullOrEmpty(keyword) ? await unitOfWork.Get().Set<Server>().ToListAsync() :
            await unitOfWork.Get().Set<Server>().Where(a => a.Name.Contains(keyword) || a.IpAddress.Contains(keyword) || a.Description.Contains(keyword)).ToListAsync();
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
}

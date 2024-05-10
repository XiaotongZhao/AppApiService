using AppApiService.Domain.DevOps;
using AppApiService.Infrastructure.Common;
using AppApiService.ViewModel;

namespace AppApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServerController : ControllerBase
{
    private readonly IServerService serverService;
    private readonly ILogger<ServerController> logger;

    public ServerController(ILogger<ServerController> logger, IServerService serverService)
    {
        this.logger = logger;
        this.serverService = serverService;
    }

    [HttpPost, Route("AddServer")]
    public async Task<bool> AddServer(Server server)
    {
        var res = await serverService.AddServer(server);
        return res;
    }

    [HttpPost, Route("UpdateServer")]
    public async Task<bool> UpdateServer(Server server)
    {
        var res = await serverService.UpdateServer(server);
        return res;
    }

    [HttpGet, Route("CheckServerIsAlive")]
    public async Task<bool> CheckServerIsAlive(int id)
    {
        var res = await serverService.CheckServerIsAlive(id);
        return res;
    }

    [HttpGet, Route("RemoveServer")]
    public async Task<bool> RemoveServer(int id)
    {
        var res = await serverService.RemoveServer(id);
        return res;
    }

    [HttpPost, Route("GetServers")]
    public async Task<DataSource<Server>> GetServers(SeachModel seachModel)
    {
        var res = await serverService.GetServers(seachModel.Keyword).TakePageDataAndCountAsync(seachModel.Skip, seachModel.Size);
        return res;
    }

    [HttpGet, Route("GetServerById")]
    public async Task<Server> GetServerById(int id)
    {
        var server = await serverService.GetServerById(id);
        return server;
    }
}

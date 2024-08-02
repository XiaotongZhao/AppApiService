using AppApiService.Domain.DevOps;
using AppApiService.Infrastructure.Common;
using AppApiService.ViewModel;
using Newtonsoft.Json;

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
    public async Task<bool> UpdateServer(string serverContent, IFormFile[] files)
    {
        if (!string.IsNullOrEmpty(serverContent))
        {
            var server = JsonConvert.DeserializeObject<Server>(serverContent);
            if (server != null) 
            {
                var serverUploadFiles = await serverService.GetServerUploadFilesByServerId(server.Id);
                foreach ( var file in files) 
                {

                }
            }
            return true;
        }
        else
        {
            return false;
        }
    }

    [HttpGet, Route("CheckServerIsAlive")]
    public async Task CheckServerIsAlive(int id)
    {
        await serverService.CheckServerIsAlive(id);
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

    [HttpGet, Route("GetServerDetailById")]
    public async Task<ServerDetail> GetServerDetailById(int id)
    {
        var serverDetail = await serverService.GetServerDetailById(id);
        return serverDetail;
    }
}

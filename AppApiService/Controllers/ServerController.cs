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
                var serverDetail = new ServerDetail()
                {
                    Server = server
                };
                if (files.Length > 0)
                {
                    serverDetail.ServerUploadFiles = new List<ServerUploadFile>();
                    foreach (var file in files)
                    {
                        byte[] fileContent;
                        using (var memoryStream = new MemoryStream())
                        {
                            await file.CopyToAsync(memoryStream);
                            fileContent = memoryStream.ToArray();
                        }
                        var serverUploadFile = new ServerUploadFile()
                        {
                            ServerId = server.Id,
                            Name = file.FileName,
                            ContentType = file.ContentType,
                            FileContent = fileContent,
                        };
                        serverDetail.ServerUploadFiles.Add(serverUploadFile);
                    }
                }
                var res = await serverService.UpdateServerDetail(serverDetail);
                return res;
            }
            else
                throw new Exception("server doesn't exist!!");
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

    [HttpGet, Route("DownloadFile")]
    public async Task<IActionResult> DownloadFile(int serverUploadFileId)
    {
        var uploadFile = await serverService.GetServerUploadFileById(serverUploadFileId);
        if (uploadFile != null)
        {
            var res = File(uploadFile.FileContent, uploadFile.ContentType, uploadFile.Name);
            return res;
        }
        else
        {
            throw new Exception("File doesn't exist !!!!");
        }
    }


    [HttpGet, Route("DeleteServerUploadFileById")]
    public async Task<bool> DeleteServerUploadFileById(int serverUploadFileId)
    {
        var res = await serverService.DeleteServerUploadFileById(serverUploadFileId);
        return res;
    }
}

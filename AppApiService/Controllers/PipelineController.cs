using AppApiService.Domain.DevOps;
using AppApiService.Domain.DevOps.ServiceTask;
using Microsoft.AspNetCore.Mvc;

namespace AppApiService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PipelineController : ControllerBase
{
    private readonly IDeployPipelineService deployPipelineService;
    private readonly ILogger<PipelineController> logger;

    public PipelineController(ILogger<PipelineController> logger, IDeployPipelineService deployPipelineService)
    {
        this.logger = logger;
        this.deployPipelineService = deployPipelineService;
    }

    [HttpPost, Route("CreatePipeline")]
    public async Task<bool> CreatePipeline(Pipeline pipeline)
    {
        var res = await deployPipelineService.CreatePipeline(pipeline);
        return res;
    }

    [HttpGet, Route("DeployPipeline")]
    public async Task DeployPipeline(int pipelineId, int serverId)
    {
        await deployPipelineService.DeployPipeline(pipelineId, serverId);
    }
}

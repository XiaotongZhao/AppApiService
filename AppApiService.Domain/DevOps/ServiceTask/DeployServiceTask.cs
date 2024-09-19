namespace AppApiService.Domain.DevOps.ServiceTask;

public class DeployServiceTask : IDeployServiceTask
{
    private IUnitOfWork unitOfWork;
    public void DeployPipelineTask(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> CreatePipeline(Pipeline pipeline)
    {
        var pipelines = unitOfWork.Get().Set<Pipeline>();
        await pipelines.AddAsync(pipeline);
        var influenceCount = await unitOfWork.Get().SaveChangesAsync();
        return influenceCount > 0;
    }

    public async Task DeployPipeline(int pipelineId, int serverId)
    {
        var pipeline = await unitOfWork.Get().Set<Pipeline>().FindAsync(pipelineId);
        if(pipeline != null) 
        {
            var deployPipeline = new DeployPipeline
            {
                PipelineId = pipelineId,
                ServerId = serverId,
                DeployPipelineStatus = CommonValue.DeployPipelineStatus.ReadyToDeploy
            };
            var tasks = pipeline.Tasks;
            foreach(var task in tasks) 
            {
                var deployPipelineTak = new DeployPipelineTask()
                {
                    TaskId = task.Id,
                    TaskStatus = CommonValue.DeployPipelineTaskStatus.ReadyToExcecute
                };
                deployPipeline.DeployPipelineTasks.Add(deployPipelineTak);
            }
            unitOfWork.Get().Set<DeployPipeline>().Add(deployPipeline);
            var influceCount = await unitOfWork.Get().SaveChangesAsync();
            if(influceCount > 0) 
            {

            }
        }
    }

    public Task<DeployPipeline> GetDeployPipelineDetailByDeployPipelineId(int deployPipelineId)
    {
        throw new NotImplementedException();
    }

    public Task<Pipeline> GetPipelineDetailById(int id)
    {
        throw new NotImplementedException();
    }
}

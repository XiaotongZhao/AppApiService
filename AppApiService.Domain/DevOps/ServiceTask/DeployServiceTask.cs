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

    public async Task DeployPipeline(int pipelineId)
    {
        var pipeline = await unitOfWork.Get().Set<Pipeline>().FindAsync(pipelineId);
        if(pipeline != null) 
        {
            var tasks = pipeline.Tasks;

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

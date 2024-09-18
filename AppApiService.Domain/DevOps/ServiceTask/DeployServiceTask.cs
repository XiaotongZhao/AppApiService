namespace AppApiService.Domain.DevOps.ServiceTask;

public class DeployServiceTask : IDeployServiceTask
{
    public Task<bool> CreatePipeline(Pipeline pipeline)
    {
        throw new NotImplementedException();
    }

    public Task DeployPipeline(int pipelineId)
    {
        throw new NotImplementedException();
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

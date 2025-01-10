namespace AppApiService.Domain.DevOps.ServiceTask;

public interface IDeployPipelineService
{
    Task<bool> CreatePipeline(Pipeline pipeline);
    Task<bool> EditPipeline(Pipeline pipeline);
    Task<bool> DeletePipelineById(int id);
    Task<Pipeline> GetPipelineDetailById(int id);
    Task DeployPipeline(int pipelineId, int serverId);
    Task<DeployPipeline> GetDeployPipelineDetailByDeployPipelineId(int deployPipelineId);
    IQueryable<Pipeline> GetPipelines(string keyword);
    IQueryable<DeployPipeline> GetDeployPipelinesByPipelineId(int pipelineId);
}

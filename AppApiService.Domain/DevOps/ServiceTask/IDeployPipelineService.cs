﻿namespace AppApiService.Domain.DevOps.ServiceTask;

public interface IDeployPipelineService
{
    Task<bool> CreatePipeline(Pipeline pipeline);
    Task<Pipeline> GetPipelineDetailById(int id);
    Task DeployPipeline(int pipelineId, int serverId);
    Task<DeployPipeline> GetDeployPipelineDetailByDeployPipelineId(int deployPipelineId);
}

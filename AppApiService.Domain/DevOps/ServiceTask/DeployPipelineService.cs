﻿using Microsoft.Extensions.DependencyInjection;
using AppApiService.Domain.DevOps.AgentServer;
using Renci.SshNet;
using static AppApiService.Domain.Common.CommonValue;

namespace AppApiService.Domain.DevOps.ServiceTask;

public class DeployPipelineService : IDeployPipelineService
{
    private IUnitOfWork unitOfWork;
    private readonly IServiceScopeFactory scopeFactory;
    public DeployPipelineService(IUnitOfWork unitOfWork, IServiceScopeFactory scopeFactory)
    {
        this.unitOfWork = unitOfWork;
        this.scopeFactory = scopeFactory;
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
        if (pipeline != null)
        {
            var deployPipeline = new DeployPipeline
            {
                PipelineId = pipelineId,
                ServerId = serverId,
                DeployPipelineStatus = CommonValue.DeployPipelineStatus.ReadyToDeploy,
                DeployPipelineTasks = new List<DeployPipelineTask>()
            };
            var tasks = pipeline.Tasks.OrderBy(task => task.StepNo);
            foreach (var task in tasks)
            {
                var deployPipelineTak = new DeployPipelineTask()
                {
                    TaskId = task.Id,
                    TaskName = task.TaskName,
                    Command = task.Script,
                    TaskStatus = CommonValue.DeployPipelineTaskStatus.ReadyToExcecute
                };
                deployPipeline.DeployPipelineTasks.Add(deployPipelineTak);
            }
            unitOfWork.Get().Set<DeployPipeline>().Add(deployPipeline);
            var influceCount = await unitOfWork.Get().SaveChangesAsync();
            if (influceCount > 0)
            {
                var task = Task.Run(async () =>
                 {
                     await startToDeployPipeline(deployPipeline);
                 });
            }
        }
    }

    private async Task startToDeployPipeline(DeployPipeline deployPipeline)
    {
        using (var scope = scopeFactory.CreateScope())
        {
            var currentUnitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();
            deployPipeline.DeployPipelineStatus = CommonValue.DeployPipelineStatus.Deploying;
            currentUnitOfWork.Get().Update(deployPipeline);
            var checkDeployPipepineStatus = await currentUnitOfWork.Get().SaveChangesAsync() > 0;
            if (checkDeployPipepineStatus)
            {
                var serverId = deployPipeline.ServerId;
                var server = await currentUnitOfWork.Get().Set<Server>().FindAsync(serverId);
                if (server != null)
                {
                    using var client = new SshClient(server.IpAddress, server.UserName, server.Password);
                    try
                    {
                        client.Connect();
                        var tasks = deployPipeline.DeployPipelineTasks;
                        foreach (var task in tasks)
                        {
                            task.TaskStatus = CommonValue.DeployPipelineTaskStatus.Pending;
                            var checkTaskUpdate = await currentUnitOfWork.Get().SaveChangesAsync() > 0;
                            if (checkTaskUpdate)
                            {
                                var command = task.Command;
                                task.TaskStatus = CommonValue.DeployPipelineTaskStatus.Executing;
                                await currentUnitOfWork.Get().SaveChangesAsync();
                                using (var sshCommand = client.RunCommand(command))
                                {
                                    var outputMessage = sshCommand.Result;
                                    var exitStatus = sshCommand.ExitStatus;
                                    if (exitStatus != 0)
                                    {
                                        task.TaskStatus = CommonValue.DeployPipelineTaskStatus.Failure;
                                    }
                                    else
                                    {
                                        task.TaskStatus = CommonValue.DeployPipelineTaskStatus.Success;
                                    }
                                    task.OutputResult = outputMessage;
                                    task.OutputLog = sshCommand.CommandText;
                                }
                            }
                            await currentUnitOfWork.Get().SaveChangesAsync();
                            if (task.TaskStatus == CommonValue.DeployPipelineTaskStatus.Failure)
                            {
                                var errorMessage = $"Task {task.TaskName} execute failed, output message : {task.OutputResult}";
                                throw new Exception(errorMessage);
                            }
                        }
                        deployPipeline.DeployPipelineStatus = CommonValue.DeployPipelineStatus.Success;
                    }
                    catch (Exception ex)
                    {
                        deployPipeline.ErrorMessage = ex.Message;
                        deployPipeline.DeployPipelineStatus = CommonValue.DeployPipelineStatus.Failure;
                    }
                    finally
                    {
                        client.Disconnect();
                        await currentUnitOfWork.Get().SaveChangesAsync();
                    }
                }
            }
        }
    }

    public async Task<DeployPipeline> GetDeployPipelineDetailByDeployPipelineId(int deployPipelineId)
    {
        var deployPipeline = await unitOfWork.Get().Set<DeployPipeline>().FindAsync(deployPipelineId);
        return deployPipeline;
    }

    public async Task<Pipeline> GetPipelineDetailById(int id)
    {
        var pipeline = await unitOfWork.Get().Set<Pipeline>().FindAsync(id);
        return pipeline;
    }

    public IQueryable<Pipeline> GetPipelines(string keyword)
    {
        var query = unitOfWork.Get().Set<Pipeline>().Where(a => a.Name.Contains(keyword))
            .Select(a => new Pipeline
            {
                Id = a.Id,
                Name = a.Name,
                Description = a.Description,
                CreatedOn = a.CreatedOn,
                LastModifyOn = a.LastModifyOn
            });
        return query;
    }

    public IQueryable<DeployPipeline> GetDeployPipelinesByPipelineId(int pipelineId)
    {
        var query = unitOfWork.Get().Set<DeployPipeline>().Where(a => a.PipelineId == pipelineId)
            .Select(a => new DeployPipeline 
            {
                Id = a.Id,
                PipelineId = a.PipelineId,
                ServerId = a.ServerId,
                ErrorMessage = a.ErrorMessage,
                DeployPipelineStatus = a.DeployPipelineStatus,
                CreatedOn = a.CreatedOn
            }).OrderByDescending(a => a.Id);
        return query;
    }
}

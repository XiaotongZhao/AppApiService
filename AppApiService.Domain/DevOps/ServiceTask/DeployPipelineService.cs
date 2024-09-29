using Renci.SshNet;

namespace AppApiService.Domain.DevOps.ServiceTask;

public class DeployPipelineService : IDeployPipelineService
{
    private IUnitOfWork unitOfWork;
    public DeployPipelineService(IUnitOfWork unitOfWork)
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
                await startToDeployPipeline(deployPipeline);
            }
        }
    }

    private async Task startToDeployPipeline(DeployPipeline deployPipeline)
    {
        deployPipeline.DeployPipelineStatus = CommonValue.DeployPipelineStatus.Deploying;
        var checkDeployPipepineStatus = await unitOfWork.Get().SaveChangesAsync() > 0;
        try 
        {
            if (checkDeployPipepineStatus)
            {
                var serverId = deployPipeline.ServerId;
                var server = await unitOfWork.Get().Set<Server>().FindAsync(serverId);
                if (server != null)
                {
                    var host = server.IpAddress;
                    var userName = server.UserName;
                    var password = server.Password;
                    using (var client = new SshClient(host, userName, password))
                    {
                        client.Connect();
                        var tasks = deployPipeline.DeployPipelineTasks;
                        foreach (var task in tasks)
                        {
                            task.TaskStatus = CommonValue.DeployPipelineTaskStatus.Pending;
                            unitOfWork.Get().Set<DeployPipelineTask>().Update(task);
                            var checkTaskUpdate = await unitOfWork.Get().SaveChangesAsync() > 0;
                            if (checkTaskUpdate)
                            {
                                var command = task.Command;
                                task.TaskStatus = CommonValue.DeployPipelineTaskStatus.Executing;
                                await unitOfWork.Get().SaveChangesAsync();
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
                            await unitOfWork.Get().SaveChangesAsync();
                            if(task.TaskStatus == CommonValue.DeployPipelineTaskStatus.Failure)
                            {
                                var errorMessage = $"Task {task.TaskName} execute failed, output message : {task.OutputResult}";
                                throw new Exception(errorMessage);
                            }
                        }
                        client.Disconnect();
                    }
                    deployPipeline.DeployPipelineStatus = CommonValue.DeployPipelineStatus.Success;
                    await unitOfWork.Get().SaveChangesAsync();
                }
            }
        }
        catch (Exception ex) 
        {
            deployPipeline.ErrorMessage = ex.Message;
            deployPipeline.DeployPipelineStatus = CommonValue.DeployPipelineStatus.Failure;
            await unitOfWork.Get().SaveChangesAsync();
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

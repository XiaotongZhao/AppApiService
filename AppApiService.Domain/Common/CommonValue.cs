namespace AppApiService.Domain.Common;

public static class CommonValue
{
    public enum DeployPipelineStatus
    {
        Success,
        Failure,
        Deploying,
        ReadyToDeploy
    }

    public enum DeployPipelineTaskStatus
    {
        Success,
        Failure,
        Pending,
        ReadyToExcecute
    }
}

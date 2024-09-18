namespace AppApiService.Domain.Common;

public static class CommonValue
{
    public enum DeployServiceStatus
    {
        Success,
        Failure,
        Deploying
    }

    public enum DeployServiceTaskStatus
    {
        Success,
        Failure,
        Pending
    }
}

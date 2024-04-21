namespace AppApiService.Domain.DevOps.Artifactory.Nexus;

public interface INexusService
{
    public Task<string> CreateNexusService(int serverId);
}

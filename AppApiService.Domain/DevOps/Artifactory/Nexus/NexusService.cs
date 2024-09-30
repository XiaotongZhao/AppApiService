using AppApiService.Domain.DevOps.AgentServer;
using Microsoft.Extensions.Logging;
using Renci.SshNet;

namespace AppApiService.Domain.DevOps.Artifactory.Nexus;

public class NexusService : INexusService
{
    private readonly ILogger<NexusService> logger;

    private readonly IUnitOfWork unitOfWork;

    public NexusService(IUnitOfWork unitOfWork, ILogger<NexusService> logger)
    {
        this.logger = logger;
        this.unitOfWork = unitOfWork;
    }
    public async Task<string> CreateNexusService(int serverId)
    {
        var reponseMessage = string.Empty;
        var server = await unitOfWork.Get().Set<Server>().FindAsync(serverId);
        if (server != null)
        {
            var host = server.IpAddress;
            var userName = server.UserName;
            var password = server.Password;
            var localFilePath = "UploadFiles/nexus-3.67.1-01-java8-unix.tar.gz";
            var remoteDirectory = "/home/zhaoxiaotong/nexus/nexus-3.67.1-01-java8-unix.tar.gz";
            using (var client = new SshClient(host, userName, password))
            {
                client.Connect();
                using (var sshCommand = client.RunCommand("mkdir nexus"))
                {
                    logger.LogInformation("nexus folder is created !");
                }

                using (var scp = new ScpClient(client.ConnectionInfo))
                {
                    scp.Connect();
                    scp.Upload(new System.IO.FileInfo(localFilePath), remoteDirectory);
                    scp.Disconnect();
                }

                var commands = @"
                   cd nexus;
                   tar -zxvf nexus-3.67.1-01-java8-unix.tar.gz;
                   cd ~/nexus/nexus-3.67.1-01/bin;
                   pwd;
                   ./nexus start;
                   ./nexus status;
                 ";
                using (var sshCommand = client.RunCommand(commands))
                {
                    logger.LogInformation(sshCommand.Result);
                    reponseMessage = sshCommand.Result;
                }
                client.Disconnect();
            }
            return reponseMessage;
        }
        else
            return "Server is not exist !!!!";
    }
}

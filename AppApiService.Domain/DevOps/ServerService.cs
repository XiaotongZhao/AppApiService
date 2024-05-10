﻿
using Renci.SshNet;

namespace AppApiService.Domain.DevOps;

public class ServerService : IServerService
{
    private readonly IUnitOfWork unitOfWork;

    public ServerService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> AddServer(Server server)
    {
        await unitOfWork.Get().Set<Server>().AddAsync(server);
        var influenceCount = await unitOfWork.Get().SaveChangesAsync();
        return influenceCount > 0;
    }

    public async Task<bool> UpdateServer(Server server)
    {
         unitOfWork.Get().Set<Server>().Update(server);
        var influenceCount = await unitOfWork.Get().SaveChangesAsync();
        return influenceCount > 0;
    }

    public async Task<bool> CheckServerIsAlive(int id)
    {
        var server = await unitOfWork.Get().Set<Server>().FindAsync(id);
        if (server != null)
        {
            var userName = server.UserName;
            var password = server.Password;
            var ipAddress = server.IpAddress;
            var port = server.Port ?? 0;
            using (var client = new SshClient(ipAddress, port, userName, password))
            {
                client.Connect();
                return client.IsConnected;
            }
        }
        else
            throw new Exception("Server is not exist!");

    }

    public IQueryable<Server> GetServers(string keyword)
    {
        var query = unitOfWork.Get().Set<Server>().Where(a => a.Name.Contains(keyword) || a.IpAddress.Contains(keyword) || a.Description.Contains(keyword));
        return query;
    }

    public async Task<bool> RemoveServer(int id)
    {
        var server = await unitOfWork.Get().Set<Server>().FindAsync(id);
        if (server != null)
            server.IsDeleted = true;
        var influenceCount = await unitOfWork.Get().SaveChangesAsync();
        return influenceCount > 0;
    }

    public async Task<Server> GetServerById(int id)
    {
        var server = await unitOfWork.Get().Set<Server>().FindAsync(id);
        return server;
    }
}

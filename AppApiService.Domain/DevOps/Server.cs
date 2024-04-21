using AppApiService.Domain.TestService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppApiService.Domain.DevOps;

[EntityTypeConfiguration(typeof(ServerConfiguration))]
public class Server : EntityBase<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string IpAddress { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
}

public class ServerConfiguration : IEntityTypeConfiguration<Server>
{
    public void Configure(EntityTypeBuilder<Server> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}
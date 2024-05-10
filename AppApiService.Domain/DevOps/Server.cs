namespace AppApiService.Domain.DevOps;

[EntityTypeConfiguration(typeof(ServerConfiguration))]
public class Server : EntityBase<int>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string IpAddress { get; set; }
    public string Password { get; set; }
    public string UserName { get; set; }
    public int? Port { get; set; }
    public virtual List<ServerUploadFile> ServerUploadFiles { get; set; }
}

public class ServerConfiguration : IEntityTypeConfiguration<Server>
{
    public void Configure(EntityTypeBuilder<Server> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}

[EntityTypeConfiguration(typeof(ServerUploadFileConfiguration))]
public class ServerUploadFile : EntityBase<int>
{
    public int ServerId {  get; set; }
    public virtual Server Server { get; set; }
    public string Name { get; set; }
    public byte[] FileContent { get; set; }
}

public class ServerUploadFileConfiguration : IEntityTypeConfiguration<ServerUploadFile>
{
    public void Configure(EntityTypeBuilder<ServerUploadFile> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}
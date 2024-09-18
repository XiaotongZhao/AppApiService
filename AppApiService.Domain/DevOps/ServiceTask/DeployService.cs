using static AppApiService.Domain.Common.CommonValue;

namespace AppApiService.Domain.DevOps.ServiceTask;

[EntityTypeConfiguration(typeof(DeployServiceConfiguration))]
public class DeployService : EntityBase<int>
{
    public int ServiceId { get; set; }
    public int ServerId { get; set; }
    public DeployServiceStatus DeployServiceStatus { get; set; }
}

public class DeployServiceConfiguration : IEntityTypeConfiguration<DeployService>
{
    public void Configure(EntityTypeBuilder<DeployService> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}

[EntityTypeConfiguration(typeof(DeployServiceTaskConfiguration))]
public class DeployServiceTask : EntityBase<int>
{
    public int ServiceId { get; set; }
    public string OutputResult { get; set; }
    public string OutputLog { get; set; }
    public DeployServiceTaskStatus TaskStatus { get; set; }
}
public class DeployServiceTaskConfiguration : IEntityTypeConfiguration<DeployServiceTask>
{
    public void Configure(EntityTypeBuilder<DeployServiceTask> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}
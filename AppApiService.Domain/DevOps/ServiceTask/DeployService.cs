namespace AppApiService.Domain.DevOps.ServiceTask;

[EntityTypeConfiguration(typeof(DeployServiceConfiguration))]
public class DeployService : EntityBase<int>
{
    public int ServiceId { get; set; }
    public string Result { get; set; }
    public int ServerId { get; set; }
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
    public int StepNo { get; set; }
    public int ServiceId { get; set; }
    public int TaskId { get; set; }
    public string TaskStatus { get; set; }
    public string ExcuteResult { get; set; }
    public string OutputLog { get; set; }
}
public class DeployServiceTaskConfiguration : IEntityTypeConfiguration<DeployServiceTask>
{
    public void Configure(EntityTypeBuilder<DeployServiceTask> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}
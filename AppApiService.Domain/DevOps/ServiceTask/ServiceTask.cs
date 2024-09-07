namespace AppApiService.Domain.DevOps.ServiceTask;

[EntityTypeConfiguration(typeof(ServiceConfiguration))]
public class Service : EntityBase<int>
{ 
    public string Name { get; set; }
    public string Status { get; set; }
    public int ServerId { get; set; }
    public virtual List<ServiceTask> ServiceTasks { get; set; }
}

public class ServiceConfiguration : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}


[EntityTypeConfiguration(typeof(ServiceTaskConfiguration))]
public class ServiceTask : EntityBase<int>
{
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public int StepNo { get; set; }
    public string Log { get; set; }
    public int ServiceId { get; set; }
}

public class ServiceTaskConfiguration : IEntityTypeConfiguration<ServiceTask>
{
    public void Configure(EntityTypeBuilder<ServiceTask> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}

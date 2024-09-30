using static AppApiService.Domain.Common.CommonValue;

namespace AppApiService.Domain.DevOps.ServiceTask;

[EntityTypeConfiguration(typeof(DeployPipelineConfiguration))]
public class DeployPipeline : EntityBase<int>
{
    public int PipelineId { get; set; }
    public int ServerId { get; set; }
    public string? ErrorMessage { get; set; }
    public DeployPipelineStatus DeployPipelineStatus { get; set; }
    public virtual List<DeployPipelineTask> DeployPipelineTasks { get; set; }
}

public class DeployPipelineConfiguration : IEntityTypeConfiguration<DeployPipeline>
{
    public void Configure(EntityTypeBuilder<DeployPipeline> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}

[EntityTypeConfiguration(typeof(DeployPipelineTaskConfiguration))]
public class DeployPipelineTask : EntityBase<int>
{
    public int DeployPipelineId { get; set; }
    public string TaskName { get; set; }
    public int TaskId { get; set; }
    public string Command { get; set; }
    public string? OutputResult { get; set; }
    public string? OutputLog { get; set; }
    public DeployPipelineTaskStatus TaskStatus { get; set; }
}
public class DeployPipelineTaskConfiguration : IEntityTypeConfiguration<DeployPipelineTask>
{
    public void Configure(EntityTypeBuilder<DeployPipelineTask> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}
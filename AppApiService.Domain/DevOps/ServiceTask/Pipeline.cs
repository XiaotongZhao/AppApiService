namespace AppApiService.Domain.DevOps.ServiceTask;

[EntityTypeConfiguration(typeof(PipelineConfiguration))]
public class Pipeline : EntityBase<int>
{ 
    public string Name { get; set; }
    public string Description { get; set; }
    public virtual List<PipelineTask> Tasks { get; set; }
}

public class PipelineConfiguration : IEntityTypeConfiguration<Pipeline>
{
    public void Configure(EntityTypeBuilder<Pipeline> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}


[EntityTypeConfiguration(typeof(PipelineTaskConfiguration))]
public class PipelineTask : EntityBase<int>
{
    public int StepNo { get; set; }
    public string TaskName { get; set; }
    public string TaskDescription { get; set; }
    public int PipelineId { get; set; }
    public string Script { get; set; }
    public int? ServerFileId { get; set; }
}

public class PipelineTaskConfiguration : IEntityTypeConfiguration<PipelineTask>
{
    public void Configure(EntityTypeBuilder<PipelineTask> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}
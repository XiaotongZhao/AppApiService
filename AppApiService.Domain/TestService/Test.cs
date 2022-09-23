namespace AppApiService.Domain.TestService;

[EntityTypeConfiguration(typeof(TestConfiguration))]
public class Test : EntityBase<int>
{
    public string Name { get; set; }
}

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}

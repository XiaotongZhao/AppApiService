using System.ComponentModel.DataAnnotations.Schema;

namespace AppApiService.Domain.TestService;

[EntityTypeConfiguration(typeof(TestConfiguration))]
public class Test : EntityBase<int>
{
    [Column(TypeName = "nvarchar(200)")]
    public string Name { get; set; }
    public int Age { get; set; }
    [Column(TypeName = "nvarchar(500)")]
    public string Address { get; set; }
    [Column(TypeName = "nvarchar(500)")]
    public string Country { get; set; }
}

public class TestConfiguration : IEntityTypeConfiguration<Test>
{
    public void Configure(EntityTypeBuilder<Test> builder)
    {
        builder.HasQueryFilter(a => !a.IsDeleted);
    }
}

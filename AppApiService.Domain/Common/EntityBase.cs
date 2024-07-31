using System.ComponentModel.DataAnnotations;

namespace AppApiService.Domain.Common;

public abstract class EntityBase<TKey>
{
    public TKey Id { get; set; }
    [DataType(DataType.DateTime)]
    public DateTime? CreatedOn { get; set; } = DateTime.Now;
    [DataType(DataType.DateTime)]
    public DateTime? LastModifyOn { get; set; }

    public bool IsDeleted { get; set; }
}

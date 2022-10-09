namespace AppApiService.Infrastructure.Common;

public class DataSource<TSource>
{
    public List<TSource>? Data { get; set; }
    public int Count { get; set; }
}

public class BaseSearch
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Skip { get; set; }
    public int Size { get; set; }
}
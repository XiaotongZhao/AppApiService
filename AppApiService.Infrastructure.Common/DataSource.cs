namespace AppApiService.Infrastructure.Common;

public class DataSource<TSource>
{
    public List<TSource>? Data { get; set; }
    public int Count { get; set; }
}

public class BaseSearch
{
    public int Skip { get; set; }
    public int Size { get; set; }
}
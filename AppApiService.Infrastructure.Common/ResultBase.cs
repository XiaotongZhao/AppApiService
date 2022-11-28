namespace AppApiService.Infrastructure.Common;

public class ResultBase<T> where T : class
{
    public List<T> DataList { get; set; }
    public int Count { get; set; }
}

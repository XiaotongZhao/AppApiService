using Newtonsoft.Json.Linq;

namespace AppApiService.Domain.DynamicRequestDataService;

public interface IDynamicDataService
{
    Task<List<DataMap>> GetDataMapList();
    Task<bool> AddDataMap(DataMap test);
    Task<bool> AddDataMaps(List<DataMap> dataMaps);
    Task<JObject> TestMapJsonObject(JObject data);
}

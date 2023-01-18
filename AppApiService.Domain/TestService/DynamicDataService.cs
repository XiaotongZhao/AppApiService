using AppApiService.Domain.DynamicRequestDataService;
using Newtonsoft.Json.Linq;

namespace AppApiService.Domain.TestService;

public class DynamicDataService : IDynamicDataService
{
    private readonly IUnitOfWork unitOfWork;

    public DynamicDataService(IUnitOfWork unitOfWork)
    {
        this.unitOfWork = unitOfWork;
    }

    public async Task<bool> AddDataMap(DataMap dataMap)
    {
        await unitOfWork.Get().Set<DataMap>().AddAsync(dataMap);
        var insertCount = await unitOfWork.Get().SaveChangesAsync();
        return insertCount > 0;
    }

    public async Task<bool> AddDataMaps(List<DataMap> dataMaps)
    {
        await unitOfWork.Get().Set<DataMap>().AddRangeAsync(dataMaps);
        var insertCount = await unitOfWork.Get().SaveChangesAsync();
        return insertCount > 0;
    }

    public async Task<List<DataMap>> GetDataMapList()
    {
        return await unitOfWork.Get().Set<DataMap>().ToListAsync();
    }

    public void MapJsonPropertyValue(JObject finalData, KeyValuePair<string, JToken?> perproty, Dictionary<string, DataMap> dicNameAndDataMap)
    {
        var propertyName = perproty.Key;
        var propertyValue = perproty.Value;
        if(dicNameAndDataMap.Keys.Any(key => key == propertyName) && propertyValue != null) 
        {
            var dataMap = dicNameAndDataMap[propertyName];
            var mapName = dataMap.MapName;
            if (dataMap.MapType == DataType.String)
            {
                finalData.Add(mapName, propertyValue.ToString());
            }
            else if (dataMap.MapType == DataType.Int)
            {
                finalData.Add(mapName, (int)propertyValue);
            }
            else if (dataMap.MapType == DataType.Object)
            {
                var childDataMaps = dataMap.ChildDataMaps;
                var childObject = new JObject();
                if (childDataMaps != null)
                {
                    var dicNameAndChildDataMap = childDataMaps.ToDictionary(key => key.Name, value => value);
                    var childDataObject = propertyValue.ToObject<JObject>();
                    if (childDataObject != null)
                    {
                        foreach (var property in childDataObject)
                        {
                            MapJsonPropertyValue(childObject, property, dicNameAndChildDataMap);
                        }
                    }
                }
                finalData.Add(mapName, childObject);
            }
            else if (dataMap.MapType == DataType.ArrayObject) 
            {
                var childDataMaps = dataMap.ChildDataMaps;
                var arrays = new JArray();
                if (childDataMaps != null)
                {
                    var dicNameAndChildDataMap = childDataMaps.ToDictionary(key => key.Name, value => value);
                    foreach (var e in propertyValue)
                    {
                        var elementObject = e.ToObject<JObject>();
                        if (elementObject != null)
                        {
                            var data = new JObject();
                            foreach (var property in elementObject)
                            {
                                MapJsonPropertyValue(data, property, dicNameAndChildDataMap);
                            }
                            arrays.Add(data);
                        }
                    }
                }
                finalData.Add(mapName, arrays);
            }
        }
    }


    public async Task<JObject> TestMapJsonObject(JObject data)
    {
        var res = new JObject();

        var dataMaps = await unitOfWork.Get().Set<DataMap>().ToListAsync();
        var dicPropertyNameAndDataMap = await unitOfWork.Get().Set<DataMap>().ToDictionaryAsync(key => key.Name, value => value);
        foreach (var e in data)
        {
            MapJsonPropertyValue(res, e, dicPropertyNameAndDataMap);
        }
        return res;
    }
}

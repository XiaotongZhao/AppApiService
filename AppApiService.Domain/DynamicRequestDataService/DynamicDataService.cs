using Newtonsoft.Json.Linq;

namespace AppApiService.Domain.DynamicRequestDataService;

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

    public void MapJsonPropertyValue(JObject finalData, KeyValuePair<string, JToken?> perproty, Dictionary<string, List<DataMap>> dicNameAndDataMap)
    {
        var propertyName = perproty.Key;
        var propertyValue = perproty.Value;
        if (dicNameAndDataMap.Keys.Any(key => key == propertyName) && propertyValue != null)
        {
            var dataMaps = dicNameAndDataMap[propertyName];
            foreach (var dataMap in dataMaps)
            {
                var mapName = dataMap.MapName;
                var parentMapName = dataMap.ParentMapName;
                if (dataMap.MapType == DataType.String && dataMap.Type == DataType.Array)
                {
                    var arrayData = propertyValue.ToArray();
                    var firstValue = arrayData.FirstOrDefault() ?? string.Empty;
                    finalData.Add(mapName, firstValue.ToString());
                }
                else if (dataMap.MapType == DataType.String)
                {
                    if (dataMap.ChildDataValueMaps != null && dataMap.ChildDataValueMaps.Any())
                    {
                        var mapValue = dataMap.ChildDataValueMaps.Find(a => a.DataValue == propertyValue.ToString())?.DataMapValue;
                        finalData.Add(mapName, mapValue);
                    }
                    else
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
                        var keyAndDataMaps = childDataMaps.GroupBy(key => key.Name)
                            .Select(a => new { a.Key, dataMaps = a.Select(b => b).ToList() }).ToList();
                        var dicNameAndChildDataMaps = keyAndDataMaps.ToDictionary(key => key.Key, value => value.dataMaps);
                        var childDataObject = propertyValue.ToObject<JObject>();
                        if (childDataObject != null)
                        {
                            foreach (var property in childDataObject)
                            {
                                MapJsonPropertyValue(childObject, property, dicNameAndChildDataMaps);
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
                        var keyAndDataMaps = childDataMaps.GroupBy(key => key.Name)
                       .Select(a => new { a.Key, dataMaps = a.Select(b => b).ToList() }).ToList();
                        var dicNameAndChildDataMaps = keyAndDataMaps.ToDictionary(key => key.Key, value => value.dataMaps);
                        foreach (var e in propertyValue)
                        {
                            var elementObject = e.ToObject<JObject>();
                            if (elementObject != null)
                            {
                                var data = new JObject();
                                foreach (var property in elementObject)
                                {
                                    MapJsonPropertyValue(data, property, dicNameAndChildDataMaps);
                                }
                                arrays.Add(data);
                            }
                        }
                    }
                    if (!string.IsNullOrEmpty(parentMapName))
                    {
                        var childObject = new JObject
                        {
                            { mapName, arrays }
                        };
                        finalData.Add(parentMapName, childObject);
                    }
                    else
                        finalData.Add(mapName, arrays);
                }
            }

        }
    }


    public async Task<JObject> TestMapJsonObject(JObject data)
    {
        var res = new JObject();

        var dataMaps = await unitOfWork.Get().Set<DataMap>().ToListAsync();
        var keyAndDataMaps = await unitOfWork.Get().Set<DataMap>()
            .GroupBy(key => key.Name)
            .Select(a => new { a.Key, dataMaps = a.Select(b => b) }).ToListAsync();
        var dicPropertyNameAndDataMaps = keyAndDataMaps.ToDictionary(key => key.Key, value => value.dataMaps.ToList());
        foreach (var e in data)
        {
            MapJsonPropertyValue(res, e, dicPropertyNameAndDataMaps);
        }
        return res;
    }
}

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



    public async Task<JObject> TestMapJsonObject(JObject data)
    {
        var res = new JObject();

        var dataMaps = await unitOfWork.Get().Set<DataMap>().ToListAsync();
        var dicPropertyNameAndDataMap = await unitOfWork.Get().Set<DataMap>().ToDictionaryAsync(key => key.Name, value => value);
        foreach (var e in data)
        {
            var propertyName = e.Key;
            var propertyValue = e.Value;
            if (dicPropertyNameAndDataMap.Keys.Any(key => key == propertyName))
            {
                var dataMap = dicPropertyNameAndDataMap[propertyName];
                var mapName = dataMap.MapName;
                if (dataMap.MapType == DataType.String)
                {
                    res.Add(mapName, propertyValue?.ToString());
                }
                else if (dataMap.MapType == DataType.Int)
                {
                    res.Add(mapName, (int)(propertyValue ?? 0));
                }
                else if (dataMap.MapType == DataType.Object)
                {
                    var childDataMaps = dataMap.ChildDataMaps;
                    var jObject = new JObject();
                    if (childDataMaps != null && propertyValue != null)
                    {
                        var dicChildDataMaps = childDataMaps.ToDictionary(key => key.Name, value => value);
                        foreach (var element in propertyValue)
                        {
                            var name = element.ToObject<JProperty>()?.Name;
                            var value = element.ToObject<JProperty>()?.Value;

                            if (dicChildDataMaps.Keys.Any(key => key == name))
                            {
                                var childDataMap = dicChildDataMaps[name];
                                jObject.Add(childDataMap.MapName, value);
                            }
                        }
                        res.Add(mapName, jObject);
                    }

                }
                else if (dataMap.MapType == DataType.ArrayObject)
                {
                    var childDataMaps = dataMap.ChildDataMaps;
                    var arrays = new JArray();
                    if (propertyValue != null && childDataMaps != null)
                    {
                        foreach (var childElements in propertyValue)
                        {
                            var jObject = new JObject();
                            foreach (var childElement in childElements)
                            {
                                if (childElement != null)
                                {
                                    var key = childElement.ToObject<JProperty>()?.Name;
                                    var value = childElement.ToObject<JProperty>()?.Value;
                                    var childDataMap = childDataMaps.Find(a => a.Name == key);
                                    if (childDataMap != null)
                                    {
                                        var childMapName = childDataMap.MapName;
                                        if (childDataMap.MapType == DataType.String && value != null)
                                            jObject.Add(childMapName, value.ToString());
                                        else if (childDataMap.MapType == DataType.Int && value != null)
                                            jObject.Add(childMapName, (int)value);
                                    }
                                }
                            }
                            arrays.Add(jObject);
                        }
                        res.Add(mapName, arrays);
                    }
                }
            }
        }
        return res;
    }
}

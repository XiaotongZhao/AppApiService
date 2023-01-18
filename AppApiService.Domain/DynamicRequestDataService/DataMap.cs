namespace AppApiService.Domain.DynamicRequestDataService;

public class DataMap : EntityBase<int>
{
    public string Name { get; set; }
    public string MapName { get; set; }
    public DataType MapType { get; set; }
    public int? DataMapId { get; set; }
    public virtual List<DataMap>? ChildDataMaps {get; set; }
}

public enum DataType
{
    Int,
    String,
    Decimal,
    ArrayObject,
    Object
}

public class NameAndPropertyType
{
    public string Name { get; set; }
    public DataType DataType { get; set; }
}
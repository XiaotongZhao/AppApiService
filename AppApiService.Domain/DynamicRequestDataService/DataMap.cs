namespace AppApiService.Domain.DynamicRequestDataService;

public class DataMap : EntityBase<int>
{
    public string Name { get; set; }
    public string MapName { get; set; }
    public DataType? Type { get; set; }
    public string? ParentMapName { get; set; }
    public DataType MapType { get; set; }
    public int? DataMapId { get; set; }
    public virtual List<DataMap>? ChildDataMaps {get; set; }
    public virtual List<DataValueMap>? DataValueMaps { get; set; }
}

public class DataValueMap : EntityBase<int>
{
    public int DataMapId { get; set; }
    public string DataValue { get; set; }
    public string DataMapValue { get; set; }
}


public enum DataType
{
    Int,
    String,
    Decimal,
    ArrayObject,
    Object,
    Array
}

public class NameAndPropertyType
{
    public string Name { get; set; }
    public DataType DataType { get; set; }
}
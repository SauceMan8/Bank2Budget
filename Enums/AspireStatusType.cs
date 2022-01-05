public class AspireStatusType 
{
    public static AspireStatusType Undefined {get;} = new AspireStatusType(-1, "");
    public static AspireStatusType Pending {get;} = new AspireStatusType(0, "🅿️");
    public static AspireStatusType Complete {get;} = new AspireStatusType(1, "✅");

    public string Name { get; private set; }
    public int Value { get; private set; }

    private AspireStatusType(int val, string name) 
    {
        Value = val;
        Name = name;
    }

    public static IEnumerable<AspireStatusType> List()
    {
        return new[]{Pending,Complete};
    }

    public static AspireStatusType FromString(string AspireStatusTypeString)
    {
        return List().Single(r => String.Equals(r.Name, AspireStatusTypeString, StringComparison.OrdinalIgnoreCase));
    }

    public static AspireStatusType FromValue(int value)
    {
        return List().Single(r => r.Value == value);
    }
}
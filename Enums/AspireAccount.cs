public class AspireAccount 
{
    public static AspireAccount Undefined {get;} = new AspireAccount(-1, "");
    public static AspireAccount MACUDebit {get;} = new AspireAccount(0, "💰 MACU Checking");
    public static AspireAccount MACUCredit {get;} = new AspireAccount(1, "💳 MACU Credit Card");
    public static AspireAccount MACUSavings {get;} = new AspireAccount(2, "MACU Savings");
    public static AspireAccount MedSavings {get;} = new AspireAccount(3, "Medical Savings");

    public string Name { get; private set; }
    public int Value { get; private set; }

    private AspireAccount(int val, string name) 
    {
        Value = val;
        Name = name;
    }

    public static IEnumerable<AspireAccount> List()
    {
        return new[]{MACUDebit,MACUCredit,MACUSavings,MedSavings};
    }

    public static AspireAccount FromString(string AspireAccountString)
    {
        return List().Single(r => String.Equals(r.Name, AspireAccountString, StringComparison.OrdinalIgnoreCase));
    }

    public static AspireAccount FromValue(int value)
    {
        return List().Single(r => r.Value == value);
    }
}
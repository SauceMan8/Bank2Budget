namespace Bank2Budget
{
    public class MACUTransactionType
    {
        public static MACUTransactionType Undefined { get; } = new MACUTransactionType(-1, "Undefined");
        public static MACUTransactionType Debit { get; } = new MACUTransactionType(0, "Debit");
        public static MACUTransactionType Credit { get; } = new MACUTransactionType(1, "Credit");

        public string Name { get; private set; }
        public int Value { get; private set; }

        private MACUTransactionType(int val, string name)
        {
            Value = val;
            Name = name;
        }

        public static IEnumerable<MACUTransactionType> List()
        {
            return new[] { Debit, Credit };
        }

        public static MACUTransactionType FromString(string MACUTransactionTypeString)
        {
            return List().Single(r => String.Equals(r.Name, MACUTransactionTypeString, StringComparison.OrdinalIgnoreCase));
        }

        public static MACUTransactionType FromValue(int value)
        {
            return List().Single(r => r.Value == value);
        }
    }
}
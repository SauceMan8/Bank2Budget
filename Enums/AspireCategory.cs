namespace Bank2Budget
{
    public class AspireCategory
    {
        public static AspireCategory Undefined { get; } = new AspireCategory(-1, "");
        public static AspireCategory DineOut { get; } = new AspireCategory(0, "Dining Out");
        public static AspireCategory Groceries { get; } = new AspireCategory(1, "Groceries");
        public static AspireCategory Gasoline { get; } = new AspireCategory(2, "Gasoline");
        public static AspireCategory EveryElse { get; } = new AspireCategory(3, "Everything Else");
        public static AspireCategory Fun { get; } = new AspireCategory(4, "Fun Money");
        public static AspireCategory HomeImprove { get; } = new AspireCategory(5, "Home Improvement");
        public static AspireCategory Electic { get; } = new AspireCategory(6, "Electric Bill");
        public static AspireCategory RentInsurance { get; } = new AspireCategory(7, "Rent Insurance");
        public static AspireCategory CarInsurance { get; } = new AspireCategory(8, "Car Insurance");
        public static AspireCategory NaturalGas { get; } = new AspireCategory(9, "Natural Gas Bill");
        public static AspireCategory Rent { get; } = new AspireCategory(10, "Rent");
        public static AspireCategory Cell { get; } = new AspireCategory(11, "Cell Phone Bill");
        public static AspireCategory GoogleStorage { get; } = new AspireCategory(12, "Google Storage");
        public static AspireCategory DisneyP { get; } = new AspireCategory(13, "Disney+");
        public static AspireCategory Spotify { get; } = new AspireCategory(14, "Spotify/Hulu");
        public static AspireCategory NSO { get; } = new AspireCategory(15, "Nintendo Switch Online");
        public static AspireCategory ZanderID { get; } = new AspireCategory(16, "Zander ID Theft");
        public static AspireCategory Prime { get; } = new AspireCategory(17, "Amazon Prime");
        public static AspireCategory Costco { get; } = new AspireCategory(18, "Costco");
        public static AspireCategory AntiVirus { get; } = new AspireCategory(19, "Anti-Virus");
        public static AspireCategory Auto { get; } = new AspireCategory(20, "Auto Maintenance");
        public static AspireCategory Clothes { get; } = new AspireCategory(21, "Clothing");
        public static AspireCategory Tithes { get; } = new AspireCategory(22, "Tithes");
        public static AspireCategory Gifts { get; } = new AspireCategory(23, "Gifts");
        public static AspireCategory Car { get; } = new AspireCategory(24, "Car");
        public static AspireCategory HomePayment { get; } = new AspireCategory(25, "Home Down Payment");
        public static AspireCategory Tech { get; } = new AspireCategory(26, "Tech");
        public static AspireCategory Baby { get; } = new AspireCategory(27, "Baby");
        public static AspireCategory LIS { get; } = new AspireCategory(28, "Large Item Saving");
        public static AspireCategory InTax { get; } = new AspireCategory(29, "Income Taxes");
        public static AspireCategory MiscDeduct { get; } = new AspireCategory(30, "Misc Deductions");
        public static AspireCategory Medical { get; } = new AspireCategory(31, "Medical");
        public static AspireCategory Available { get; } = new AspireCategory(32, "Available to budget");
        public static AspireCategory CreditCard { get; } = new AspireCategory(33, "💳 MACU Credit Card");

        public string Name { get; private set; }
        public int Value { get; private set; }

        private AspireCategory(int val, string name)
        {
            Value = val;
            Name = name;
        }

        public static IEnumerable<AspireCategory> List()
        {
            return new[] { DineOut, Groceries, Gasoline, EveryElse, Fun, HomeImprove, Electic, RentInsurance, CarInsurance, NaturalGas, Rent, Cell, GoogleStorage, DisneyP, Spotify, NSO, ZanderID, Prime, Costco, AntiVirus, Auto, Clothes, Tithes, Gifts, Car,
                            HomePayment, Tech, Baby, LIS, InTax, MiscDeduct, Medical, Available, CreditCard };

        }

        public static AspireCategory FromString(string? AspireCategoryString)
        {
            if (AspireCategoryString == null) return AspireCategory.Undefined;
            return List().Single(r => String.Equals(r.Name, AspireCategoryString, StringComparison.OrdinalIgnoreCase));
        }

        public static AspireCategory FromValue(int? value)
        {
            if (value == null) return AspireCategory.Undefined;
            return List().Single(r => r.Value == value);
        }
    }
}
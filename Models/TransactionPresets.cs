using Newtonsoft.Json;

namespace Bank2Budget
{    
    public class TransactionPresets
    {
        public string? TransactionDescription;

        public TransactionType TransactionType = TransactionType.Undefined;

        public int? TransactionMax;

        public int? TransactionMin;

        public List<TransactionDiscription>? BudgetEntry;

        public static List<TransactionPresets>? ReadTransactionJson()
        {
            var transactions = JsonConvert.DeserializeObject<List<TransactionPresets>>(File.ReadAllText(@"DescriptionReplacement.json"));

            return transactions;
        }
    }
}
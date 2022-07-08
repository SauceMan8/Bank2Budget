
namespace Bank2Budget
{
    public class AspireTransaction
    {
        public DateOnly Date;
        public decimal? Outflow;
        public decimal? Inflow;
        public AspireCategory Category = AspireCategory.Undefined;
        public AspireAccount Account = AspireAccount.Undefined;
        public string? Memo;
        public AspireStatusType Status = AspireStatusType.Undefined;

        public string GetCSVLine()
        {
            return $"{Date.ToString("MM/dd/yyyy")},{Outflow:0.00},{Inflow:0.00},{Category.Name},{Account.Name},{Memo},{Status.Name}";
        }
    }
}
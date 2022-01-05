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
        return $"{Date.ToString("MM/dd/yyyy")},{Outflow},{Inflow},{Category.Name},{Account.Name},{Memo},{Status.Name}";
    }
}
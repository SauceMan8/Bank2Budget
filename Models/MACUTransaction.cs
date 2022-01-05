namespace Bank2Budget
{    
    public class MACUTransaction
    {
        public DateOnly PostingDate;
        public DateOnly EffectiveDate;
        public MACUTransactionType TransactionType = MACUTransactionType.Undefined;
        public decimal Amount;
        public string? Description;
        public string? ExtDescription;
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Bank2Budget
{
    // dotnet run ExportedTransactionsCheck.csv ExportedTransactionsCredit.csv NewAll.csv
    class Program
    {
        static void Main(string[] args)
        {
            var inputFile1 = "ExportedTransactionsCheck.csv";
            var inputFile2 = "ExportedTransactionsCredit.csv";
            var outputFile = "NewAll.csv";
            if (args != null && args.Count() > 0)
            {
                inputFile1 = args[0];
                inputFile2 = args[1];
                outputFile = args[2];
            }

            var macuTransacts = FileHelper.GetMACUTransactions(inputFile1);
            var macuTransacts2 = FileHelper.GetMACUTransactions(inputFile2);
            var aspireTransacts = MappingHelper.GetAspireTransactionsFromMACUTransactions(macuTransacts, AspireAccount.MACUDebit);
            aspireTransacts.AddRange(MappingHelper.GetAspireTransactionsFromMACUTransactions(macuTransacts2, AspireAccount.MACUCredit));
            
            var orderedTrasactions = aspireTransacts.OrderBy(t => t.Date).ToList();
            FileHelper.WriteNewFile(outputFile, orderedTrasactions);
        }
    }

    public static class MappingHelper
    {
        public static List<AspireTransaction> GetAspireTransactionsFromMACUTransactions(List<MACUTransaction> oldTransactions, AspireAccount account)
        {
            var newTrasactions = new List<AspireTransaction>();
            foreach (var trasaction in oldTransactions)
            {
                newTrasactions.AddRange(GetAspireTransactionFromMACUTransaction(trasaction, account));
            }

            return newTrasactions;
        }

        public static List<AspireTransaction> GetAspireTransactionFromMACUTransaction(MACUTransaction oldTransaction, AspireAccount account)
        {
            var trasactions = new List<AspireTransaction>();

            if (oldTransaction == null)
                return trasactions;

            oldTransaction.Description = oldTransaction.Description?.Replace("Loan Advance Cre ","");

            var replacedTransaction = TrasactionReplacementHelper.Static.GetAspireTransactions(oldTransaction, account);
            if (replacedTransaction != null) 
                return replacedTransaction;

            var newTrasaction = new AspireTransaction();

            newTrasaction.Date = oldTransaction.EffectiveDate;
            newTrasaction.Status = AspireStatusType.Complete;

            if (oldTransaction.Amount > 0)
                newTrasaction.Inflow = oldTransaction.Amount;
            else
                newTrasaction.Outflow = oldTransaction.Amount * (-1);

            newTrasaction.Memo = oldTransaction.Description;

            newTrasaction.Account = account;

            trasactions.Add(newTrasaction);
            return trasactions;
        }
    }

    public static class FileHelper 
    {
        public static List<MACUTransaction> GetMACUTransactions(string FilePath)
        {
            StreamReader sr = new StreamReader(FilePath);
            var MACUTransactions = new List<MACUTransaction>();
            string? line;
            string[] row = new string [13];
            var isFirst = true;
            while ((line = sr.ReadLine()) != null)
            {
                if (isFirst)
                {
                    isFirst = false;
                    continue;
                }
                row = line.Split("\",\"");

                MACUTransactions.Add(new MACUTransaction
                {
                    PostingDate = DateOnly.Parse(row[1]),
                    EffectiveDate = DateOnly.Parse(row[2]),
                    TransactionType = MACUTransactionType.FromString(row[3]),
                    Amount = decimal.Parse(row[4]),
                    Description = row[7],
                    ExtDescription = row[12]
                });
            }

            sr.Close();
            return MACUTransactions;
        }

        public static void WriteNewFile(string fileName, List<AspireTransaction> transactions)
        {
            var lines = new List<string>();

            foreach (var transact in transactions)
            {
                lines.Add(transact.GetCSVLine());
            }

            File.WriteAllLines(fileName, lines);
        }
    }

    public class TrasactionReplacementHelper
    {
        public static List<TransactionPresets>? _replacements;
        public List<TransactionPresets>? Replacements => _replacements ??= TransactionPresets.ReadTransactionJson();

        private static TrasactionReplacementHelper? _static;

        public static TrasactionReplacementHelper Static => _static ??= new TrasactionReplacementHelper();

        public List<AspireTransaction>? GetAspireTransactions(MACUTransaction oldTransaction, AspireAccount account)
        {
            var description = oldTransaction.Description;
            if (Replacements == null || description == null)
                return null;

            var transactionType = oldTransaction.Amount > 0 ? TransactionType.Credit : TransactionType.Charge;

            foreach (var replacement in Replacements)
            {
                if (replacement.TransactionDescription == null) continue;

                if (description.Contains(replacement.TransactionDescription, StringComparison.CurrentCultureIgnoreCase) 
                    && replacement.TransactionType == transactionType
                    && (replacement.TransactionMax == null || replacement.TransactionMax >= Math.Abs(oldTransaction.Amount))
                    && (replacement.TransactionMin == null || replacement.TransactionMin >= Math.Abs(oldTransaction.Amount)))
                {
                    if (replacement.BudgetEntry == null) continue;
                    var transactions = new List<AspireTransaction>();
                    if (replacement.BudgetEntry.Count == 0) return transactions;
                    foreach (var entry in replacement.BudgetEntry)
                    {
                        decimal? inflow = null;
                        decimal? outflow = null;
                        if (entry.Inflow != null)
                        {
                            if (entry.Inflow.Contains("{Amount}", StringComparison.CurrentCultureIgnoreCase))
                                inflow = oldTransaction.Amount;
                            else if (decimal.TryParse(entry.Inflow, out var value))
                                inflow = value;
                        }
                        if (entry.Outflow != null)
                        {
                            if (entry.Outflow.Contains("{Amount}", StringComparison.CurrentCultureIgnoreCase))
                                outflow = oldTransaction.Amount;
                            else if (decimal.TryParse(entry.Outflow, out var value))
                                outflow = value;
                        }

                        var trasaction = new AspireTransaction
                        {
                            Date = entry.Date ?? oldTransaction.EffectiveDate,
                            Inflow = inflow,
                            Outflow = outflow * (-1),
                            Category = AspireCategory.FromString(entry.Category),
                            Memo = entry.Memo?.Replace("{Amount}", $"{oldTransaction.Amount:0.00}", StringComparison.CurrentCultureIgnoreCase),
                            Account = entry.Account != null ? AspireAccount.FromString(entry.Account) : account,
                            Status = entry.Status != null ? AspireStatusType.FromString(entry.Status) : AspireStatusType.Complete
                        };
                        transactions.Add(trasaction);
                    }
                    return transactions;
                }
            }
            return null;
        }
    }
}
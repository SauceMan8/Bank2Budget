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
            var macuTransacts = FileHelper.GetMACUTransactions(args[0]);
            var macuTransacts2 = FileHelper.GetMACUTransactions(args[1]);
            var aspireTransacts = MappingHelper.GetAspireTransactionsFromMACUTransactions(macuTransacts, AspireAccount.MACUDebit);
            aspireTransacts.AddRange(MappingHelper.GetAspireTransactionsFromMACUTransactions(macuTransacts2, AspireAccount.MACUCredit));
            
            var orderedTrasactions = aspireTransacts.OrderBy(t => t.Date).ToList();
            FileHelper.WriteNewFile(args[2], orderedTrasactions);
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

            if (oldTransaction.Description?.Contains("MELALEUCA", StringComparison.CurrentCultureIgnoreCase) ?? false && oldTransaction.Amount > 0)
            {
                trasactions = new List<AspireTransaction>
                {
                    new AspireTransaction
                    {
                        Date = oldTransaction.EffectiveDate,
                        Inflow = oldTransaction.Amount,
                        Category = AspireCategory.Available,
                        Memo = $"PayCheck ({oldTransaction.Amount:0.00})",
                        Account = account,
                        Status = AspireStatusType.Complete
                    },
                    new AspireTransaction 
                    {
                        Date = oldTransaction.EffectiveDate,
                        Outflow = 0,
                        Category = AspireCategory.InTax,
                        Memo = "PayCheck Deductions (pre deposit)",
                        Account = account,
                        Status = AspireStatusType.Complete
                    },
                    new AspireTransaction 
                    {
                        Date = oldTransaction.EffectiveDate,
                        Outflow = 0,
                        Category = AspireCategory.MiscDeduct,
                        Memo = "PayCheck Deductions (pre deposit)",
                        Account = account,
                        Status = AspireStatusType.Complete
                    }
                };
                return trasactions;
            }

            if (oldTransaction.Description?.Contains("To Loan", StringComparison.CurrentCultureIgnoreCase) ?? false && oldTransaction.Amount < 0)
            {
                trasactions = new List<AspireTransaction>
                {
                    new AspireTransaction
                    {
                        Date = oldTransaction.EffectiveDate,
                        Outflow = oldTransaction.Amount * (-1),
                        Category = AspireCategory.CreditCard,
                        Memo = "Account Transfer",
                        Account = account,
                        Status = AspireStatusType.Complete
                    }
                };
                return trasactions;
            }

            if (oldTransaction.Description?.Contains("From Share", StringComparison.CurrentCultureIgnoreCase) ?? false && oldTransaction.Amount > 0)
                return trasactions;

            if (oldTransaction.Description?.Contains("ROCKY MOUNTAIN", StringComparison.CurrentCultureIgnoreCase) ?? false && oldTransaction.Amount < 0)
            {
                trasactions = new List<AspireTransaction>
                {
                    new AspireTransaction
                    {
                        Date = oldTransaction.EffectiveDate,
                        Outflow = oldTransaction.Amount * (-1),
                        Category = AspireCategory.Electic,
                        Memo = "Rocky Mountain Power",
                        Account = account,
                        Status = AspireStatusType.Complete
                    }
                };
                return trasactions;
            }

            if (oldTransaction.Description?.Contains("INTERMOUNTAIN GA", StringComparison.CurrentCultureIgnoreCase) ?? false && oldTransaction.Amount < 0)
            {
                trasactions = new List<AspireTransaction>
                {
                    new AspireTransaction
                    {
                        Date = oldTransaction.EffectiveDate,
                        Outflow = oldTransaction.Amount * (-1),
                        Category = AspireCategory.NaturalGas,
                        Memo = "Intermountain Gas",
                        Account = account,
                        Status = AspireStatusType.Complete
                    }
                };
                return trasactions;
            }

            if (oldTransaction.Description?.Contains("WALGREENS", StringComparison.CurrentCultureIgnoreCase) ?? false && oldTransaction.Amount < 0)
            {
                trasactions = new List<AspireTransaction>
                {
                    new AspireTransaction
                    {
                        Date = oldTransaction.EffectiveDate,
                        Outflow = oldTransaction.Amount * (-1),
                        Memo = "Walgreens",
                        Account = account,
                        Status = AspireStatusType.Complete
                    }
                };
                return trasactions;
            }

            if (oldTransaction.Description?.Contains("Google One", StringComparison.CurrentCultureIgnoreCase) ?? false && oldTransaction.Amount < 0)
            {
                trasactions = new List<AspireTransaction>
                {
                    new AspireTransaction
                    {
                        Date = oldTransaction.EffectiveDate,
                        Outflow = oldTransaction.Amount * (-1),
                        Category = AspireCategory.GoogleStorage,
                        Memo = "Google Storage",
                        Account = account,
                        Status = AspireStatusType.Complete
                    }
                };
                return trasactions;
            }

            if (oldTransaction.Description?.Contains("Payment to Zander Insurance", StringComparison.CurrentCultureIgnoreCase) ?? false && oldTransaction.Amount < 0)
            {
                trasactions = new List<AspireTransaction>
                {
                    new AspireTransaction
                    {
                        Date = oldTransaction.EffectiveDate,
                        Outflow = oldTransaction.Amount * (-1),
                        Category = AspireCategory.ZanderID,
                        Memo = "Zander ID Theft",
                        Account = account,
                        Status = AspireStatusType.Complete
                    }
                };
                return trasactions;
            }

            if (oldTransaction.Description?.Contains("Loan Advance Cre COSTCO WHSE", StringComparison.CurrentCultureIgnoreCase) ?? false && oldTransaction.Amount < 0)
            {
                trasactions = new List<AspireTransaction>
                {
                    new AspireTransaction
                    {
                        Date = oldTransaction.EffectiveDate,
                        Outflow = oldTransaction.Amount * (-1),
                        Memo = "Costco",
                        Account = account,
                        Status = AspireStatusType.Complete
                    }
                };
                return trasactions;
            }

            if (oldTransaction.Description?.Contains("Rent CO: Eden Operating", StringComparison.CurrentCultureIgnoreCase) ?? false && oldTransaction.Amount < 0)
            {
                trasactions = new List<AspireTransaction>
                {
                    new AspireTransaction
                    {
                        Date = oldTransaction.EffectiveDate,
                        Outflow = oldTransaction.Amount * (-1),
                        Category = AspireCategory.Rent,
                        Memo = "Rent",
                        Account = account,
                        Status = AspireStatusType.Complete
                    }
                };
                return trasactions;
            }

            if (oldTransaction.Description?.Contains("The Church Of Jesus Christ", StringComparison.CurrentCultureIgnoreCase) ?? false && oldTransaction.Amount < 0)
            {
                trasactions = new List<AspireTransaction>
                {
                    new AspireTransaction
                    {
                        Date = oldTransaction.EffectiveDate,
                        Outflow = oldTransaction.Amount * (-1),
                        Category = AspireCategory.Tithes,
                        Memo = "Tithing",
                        Account = account,
                        Status = AspireStatusType.Complete
                    }
                };
                return trasactions;
            }

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
}
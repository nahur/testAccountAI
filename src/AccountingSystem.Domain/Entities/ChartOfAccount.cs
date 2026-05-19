using System;
using System.Collections.Generic;

namespace AccountingSystem.Domain.Entities
{
    public class ChartOfAccount
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public int? ParentAccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
        
        // Navigation properties
        public ChartOfAccount ParentAccount { get; set; }
        public ICollection<ChartOfAccount> ChildAccounts { get; set; } = new List<ChartOfAccount>();
        public ICollection<JournalEntry> JournalEntries { get; set; } = new List<JournalEntry>();
    }

    public enum AccountType
    {
        Asset = 1,
        Liability = 2,
        Equity = 3,
        Revenue = 4,
        Expense = 5
    }
}
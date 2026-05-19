using System;
using System.Collections.Generic;

namespace AccountingSystem.Domain.Entities
{
    public class BankAccount
    {
        public int Id { get; set; }
        public string AccountName { get; set; }
        public string AccountNumber { get; set; }
        public string BankName { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyCode { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties
        public ICollection<BankReconciliation> Reconciliations { get; set; } = new List<BankReconciliation>();
    }

    public class BankReconciliation
    {
        public int Id { get; set; }
        public int BankAccountId { get; set; }
        public DateTime StatementDate { get; set; }
        public decimal StatementBalance { get; set; }
        public decimal LedgerBalance { get; set; }
        public decimal Difference { get; set; }
        public bool IsReconciled { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties
        public BankAccount BankAccount { get; set; }
    }
}
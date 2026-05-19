using System;

namespace AccountingSystem.Domain.Entities
{
    public class Expense
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public int? VendorId { get; set; }
        public string ReceiptPath { get; set; }
        public int ChartOfAccountId { get; set; }
        public ExpenseStatus Status { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties
        public Vendor Vendor { get; set; }
        public ChartOfAccount ChartOfAccount { get; set; }
    }

    public enum ExpenseStatus
    {
        Draft = 1,
        Submitted = 2,
        Approved = 3,
        Rejected = 4
    }
}
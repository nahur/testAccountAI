using System;
using System.Collections.Generic;

namespace AccountingSystem.Domain.Entities
{
    public class JournalEntry
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime EntryDate { get; set; }
        public decimal DebitAmount { get; set; }
        public decimal CreditAmount { get; set; }
        public string Reference { get; set; }
        public bool IsPosted { get; set; }
        public int ChartOfAccountId { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties
        public ChartOfAccount ChartOfAccount { get; set; }
    }
}
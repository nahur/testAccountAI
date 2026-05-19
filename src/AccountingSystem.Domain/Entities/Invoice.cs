using System;
using System.Collections.Generic;

namespace AccountingSystem.Domain.Entities
{
    public class Invoice
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public int CustomerId { get; set; }
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal Total { get; set; }
        public InvoiceStatus Status { get; set; }
        public string Notes { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties
        public Customer Customer { get; set; }
        public ICollection<InvoiceLineItem> LineItems { get; set; } = new List<InvoiceLineItem>();
    }

    public class InvoiceLineItem
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal LineTotal { get; set; }
        
        // Navigation properties
        public Invoice Invoice { get; set; }
    }

    public enum InvoiceStatus
    {
        Draft = 1,
        Sent = 2,
        Paid = 3,
        Overdue = 4,
        Cancelled = 5
    }
}
using System;

namespace AccountingSystem.Domain.Entities
{
    public class Payment
    {
        public int Id { get; set; }
        public int InvoiceId { get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public PaymentMethod Method { get; set; }
        public string Reference { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties
        public Invoice Invoice { get; set; }
    }

    public enum PaymentMethod
    {
        Cash = 1,
        Check = 2,
        CreditCard = 3,
        BankTransfer = 4,
        Other = 5
    }
}
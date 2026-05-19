using System;
using System.Collections.Generic;

namespace AccountingSystem.Domain.Entities
{
    public class Customer
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties
        public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
    }
}
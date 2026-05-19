using System;
using System.Collections.Generic;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Application.DTOs
{
    public class ChartOfAccountDTO
    {
        public int Id { get; set; }
        public string AccountCode { get; set; }
        public string AccountName { get; set; }
        public AccountType AccountType { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
        public int? ParentAccountId { get; set; }
        public DateTime CreatedDate { get; set; }
    }

    public class JournalEntryDTO
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
    }

    public class InvoiceDTO
    {
        public int Id { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DueDate { get; set; }
        public int CustomerId { get; set; }
        public decimal Total { get; set; }
        public InvoiceStatus Status { get; set; }
    }

    public class ExpenseDTO
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime ExpenseDate { get; set; }
        public decimal Amount { get; set; }
        public string Category { get; set; }
        public int ChartOfAccountId { get; set; }
    }

    public class CustomerDTO
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
    }
}
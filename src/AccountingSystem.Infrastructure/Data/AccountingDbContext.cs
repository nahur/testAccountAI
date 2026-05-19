using Microsoft.EntityFrameworkCore;
using AccountingSystem.Domain.Entities;

namespace AccountingSystem.Infrastructure.Data
{
    public class AccountingDbContext : DbContext
    {
        public AccountingDbContext(DbContextOptions<AccountingDbContext> options) : base(options)
        {
        }

        public DbSet<ChartOfAccount> ChartOfAccounts { get; set; }
        public DbSet<JournalEntry> JournalEntries { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<InvoiceLineItem> InvoiceLineItems { get; set; }
        public DbSet<Expense> Expenses { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankReconciliation> BankReconciliations { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<ExchangeRate> ExchangeRates { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ChartOfAccount configuration
            modelBuilder.Entity<ChartOfAccount>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<ChartOfAccount>()
                .HasIndex(x => x.AccountCode)
                .IsUnique();
            modelBuilder.Entity<ChartOfAccount>()
                .HasOne(x => x.ParentAccount)
                .WithMany(x => x.ChildAccounts)
                .HasForeignKey(x => x.ParentAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            // JournalEntry configuration
            modelBuilder.Entity<JournalEntry>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<JournalEntry>()
                .HasOne(x => x.ChartOfAccount)
                .WithMany(x => x.JournalEntries)
                .HasForeignKey(x => x.ChartOfAccountId);

            // Invoice configuration
            modelBuilder.Entity<Invoice>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Invoice>()
                .HasIndex(x => x.InvoiceNumber)
                .IsUnique();
            modelBuilder.Entity<Invoice>()
                .HasOne(x => x.Customer)
                .WithMany(x => x.Invoices)
                .HasForeignKey(x => x.CustomerId);

            // InvoiceLineItem configuration
            modelBuilder.Entity<InvoiceLineItem>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<InvoiceLineItem>()
                .HasOne(x => x.Invoice)
                .WithMany(x => x.LineItems)
                .HasForeignKey(x => x.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Expense configuration
            modelBuilder.Entity<Expense>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Expense>()
                .HasOne(x => x.Vendor)
                .WithMany(x => x.Expenses)
                .HasForeignKey(x => x.VendorId)
                .OnDelete(DeleteBehavior.SetNull);
            modelBuilder.Entity<Expense>()
                .HasOne(x => x.ChartOfAccount)
                .WithMany()
                .HasForeignKey(x => x.ChartOfAccountId);

            // BankAccount configuration
            modelBuilder.Entity<BankAccount>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<BankAccount>()
                .HasIndex(x => x.AccountNumber)
                .IsUnique();
            modelBuilder.Entity<BankAccount>()
                .HasMany(x => x.Reconciliations)
                .WithOne(x => x.BankAccount)
                .HasForeignKey(x => x.BankAccountId)
                .OnDelete(DeleteBehavior.Cascade);

            // Customer configuration
            modelBuilder.Entity<Customer>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Customer>()
                .HasIndex(x => x.Email)
                .IsUnique();

            // Vendor configuration
            modelBuilder.Entity<Vendor>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Vendor>()
                .HasIndex(x => x.Email)
                .IsUnique();

            // Payment configuration
            modelBuilder.Entity<Payment>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Payment>()
                .HasOne(x => x.Invoice)
                .WithMany()
                .HasForeignKey(x => x.InvoiceId);

            // Currency configuration
            modelBuilder.Entity<Currency>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<Currency>()
                .HasIndex(x => x.CurrencyCode)
                .IsUnique();

            // ExchangeRate configuration
            modelBuilder.Entity<ExchangeRate>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<ExchangeRate>()
                .HasOne(x => x.FromCurrency)
                .WithMany(x => x.ExchangeRates)
                .HasForeignKey(x => x.FromCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ExchangeRate>()
                .HasOne(x => x.ToCurrency)
                .WithMany()
                .HasForeignKey(x => x.ToCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
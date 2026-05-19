using System;
using System.Collections.Generic;

namespace AccountingSystem.Domain.Entities
{
    public class Currency
    {
        public int Id { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string Symbol { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties
        public ICollection<ExchangeRate> ExchangeRates { get; set; } = new List<ExchangeRate>();
    }

    public class ExchangeRate
    {
        public int Id { get; set; }
        public int FromCurrencyId { get; set; }
        public int ToCurrencyId { get; set; }
        public decimal Rate { get; set; }
        public DateTime EffectiveDate { get; set; }
        public DateTime CreatedDate { get; set; }
        
        // Navigation properties
        public Currency FromCurrency { get; set; }
        public Currency ToCurrency { get; set; }
    }
}
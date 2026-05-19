using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingSystem.Application.DTOs;

namespace AccountingSystem.Application.Services
{
    public interface IChartOfAccountService
    {
        Task<ChartOfAccountDTO> GetAccountByIdAsync(int id);
        Task<IEnumerable<ChartOfAccountDTO>> GetAllAccountsAsync();
        Task<IEnumerable<ChartOfAccountDTO>> GetAccountsByTypeAsync(string accountType);
        Task<ChartOfAccountDTO> CreateAccountAsync(ChartOfAccountDTO dto);
        Task<ChartOfAccountDTO> UpdateAccountAsync(int id, ChartOfAccountDTO dto);
        Task<bool> DeleteAccountAsync(int id);
        Task<decimal> GetAccountBalanceAsync(int id);
    }
}
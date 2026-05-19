using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingSystem.Application.DTOs;
using AccountingSystem.Domain.Entities;
using AccountingSystem.Domain.Interfaces;

namespace AccountingSystem.Application.Services
{
    public class ChartOfAccountService : IChartOfAccountService
    {
        private readonly IRepository<ChartOfAccount> _repository;

        public ChartOfAccountService(IRepository<ChartOfAccount> repository)
        {
            _repository = repository;
        }

        public async Task<ChartOfAccountDTO> GetAccountByIdAsync(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null)
                return null;

            return MapToDTO(account);
        }

        public async Task<IEnumerable<ChartOfAccountDTO>> GetAllAccountsAsync()
        {
            var accounts = await _repository.GetAllAsync();
            return accounts.Select(MapToDTO).ToList();
        }

        public async Task<IEnumerable<ChartOfAccountDTO>> GetAccountsByTypeAsync(string accountType)
        {
            if (!Enum.TryParse<AccountType>(accountType, out var type))
                throw new ArgumentException("Invalid account type");

            var accounts = await _repository.FindAsync(a => a.AccountType == type);
            return accounts.Select(MapToDTO).ToList();
        }

        public async Task<ChartOfAccountDTO> CreateAccountAsync(ChartOfAccountDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.AccountCode))
                throw new ArgumentException("Account code is required");

            if (string.IsNullOrWhiteSpace(dto.AccountName))
                throw new ArgumentException("Account name is required");

            var account = new ChartOfAccount
            {
                AccountCode = dto.AccountCode,
                AccountName = dto.AccountName,
                AccountType = dto.AccountType,
                IsActive = true,
                ParentAccountId = dto.ParentAccountId,
                CreatedDate = DateTime.UtcNow
            };

            var createdAccount = await _repository.AddAsync(account);
            await _repository.SaveChangesAsync();

            return MapToDTO(createdAccount);
        }

        public async Task<ChartOfAccountDTO> UpdateAccountAsync(int id, ChartOfAccountDTO dto)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null)
                throw new KeyNotFoundException($"Account with id {id} not found");

            account.AccountName = dto.AccountName ?? account.AccountName;
            account.AccountCode = dto.AccountCode ?? account.AccountCode;
            account.IsActive = dto.IsActive;
            account.ModifiedDate = DateTime.UtcNow;

            var updatedAccount = await _repository.UpdateAsync(account);
            await _repository.SaveChangesAsync();

            return MapToDTO(updatedAccount);
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            return await _repository.DeleteAsync(id);
        }

        public async Task<decimal> GetAccountBalanceAsync(int id)
        {
            var account = await _repository.GetByIdAsync(id);
            if (account == null)
                return 0;

            return account.Balance;
        }

        private ChartOfAccountDTO MapToDTO(ChartOfAccount account)
        {
            return new ChartOfAccountDTO
            {
                Id = account.Id,
                AccountCode = account.AccountCode,
                AccountName = account.AccountName,
                AccountType = account.AccountType,
                Balance = account.Balance,
                IsActive = account.IsActive,
                ParentAccountId = account.ParentAccountId,
                CreatedDate = account.CreatedDate
            };
        }
    }
}
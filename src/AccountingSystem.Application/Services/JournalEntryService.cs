using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AccountingSystem.Application.DTOs;
using AccountingSystem.Domain.Entities;
using AccountingSystem.Domain.Interfaces;

namespace AccountingSystem.Application.Services
{
    public class JournalEntryService : IJournalEntryService
    {
        private readonly IRepository<JournalEntry> _entryRepository;
        private readonly IRepository<ChartOfAccount> _accountRepository;

        public JournalEntryService(IRepository<JournalEntry> entryRepository, IRepository<ChartOfAccount> accountRepository)
        {
            _entryRepository = entryRepository;
            _accountRepository = accountRepository;
        }

        public async Task<JournalEntryDTO> GetEntryByIdAsync(int id)
        {
            var entry = await _entryRepository.GetByIdAsync(id);
            return entry == null ? null : MapToDTO(entry);
        }

        public async Task<IEnumerable<JournalEntryDTO>> GetAllEntriesAsync()
        {
            var entries = await _entryRepository.GetAllAsync();
            return entries.Select(MapToDTO).ToList();
        }

        public async Task<IEnumerable<JournalEntryDTO>> GetEntriesByAccountAsync(int accountId)
        {
            var entries = await _entryRepository.FindAsync(e => e.ChartOfAccountId == accountId);
            return entries.Select(MapToDTO).ToList();
        }

        public async Task<JournalEntryDTO> CreateEntryAsync(JournalEntryDTO dto)
        {
            if (!await ValidateDoubleEntryAsync(dto.DebitAmount, dto.CreditAmount))
                throw new InvalidOperationException("Debit and credit amounts must be equal");

            var entry = new JournalEntry
            {
                Description = dto.Description,
                EntryDate = dto.EntryDate,
                DebitAmount = dto.DebitAmount,
                CreditAmount = dto.CreditAmount,
                Reference = dto.Reference,
                ChartOfAccountId = dto.ChartOfAccountId,
                IsPosted = false,
                CreatedDate = DateTime.UtcNow
            };

            var createdEntry = await _entryRepository.AddAsync(entry);
            await _entryRepository.SaveChangesAsync();

            return MapToDTO(createdEntry);
        }

        public async Task<JournalEntryDTO> UpdateEntryAsync(int id, JournalEntryDTO dto)
        {
            var entry = await _entryRepository.GetByIdAsync(id);
            if (entry == null)
                throw new KeyNotFoundException($"Entry with id {id} not found");

            if (entry.IsPosted)
                throw new InvalidOperationException("Cannot update a posted entry");

            entry.Description = dto.Description ?? entry.Description;
            entry.DebitAmount = dto.DebitAmount;
            entry.CreditAmount = dto.CreditAmount;
            entry.Reference = dto.Reference ?? entry.Reference;

            var updated = await _entryRepository.UpdateAsync(entry);
            await _entryRepository.SaveChangesAsync();

            return MapToDTO(updated);
        }

        public async Task<bool> DeleteEntryAsync(int id)
        {
            var entry = await _entryRepository.GetByIdAsync(id);
            if (entry != null && entry.IsPosted)
                throw new InvalidOperationException("Cannot delete a posted entry");

            return await _entryRepository.DeleteAsync(id);
        }

        public async Task<bool> PostEntryAsync(int id)
        {
            var entry = await _entryRepository.GetByIdAsync(id);
            if (entry == null)
                throw new KeyNotFoundException($"Entry with id {id} not found");

            entry.IsPosted = true;
            await _entryRepository.UpdateAsync(entry);
            await _entryRepository.SaveChangesAsync();

            return true;
        }

        public Task<bool> ValidateDoubleEntryAsync(decimal debit, decimal credit)
        {
            return Task.FromResult(Math.Abs(debit - credit) < 0.01m);
        }

        private JournalEntryDTO MapToDTO(JournalEntry entry)
        {
            return new JournalEntryDTO
            {
                Id = entry.Id,
                Description = entry.Description,
                EntryDate = entry.EntryDate,
                DebitAmount = entry.DebitAmount,
                CreditAmount = entry.CreditAmount,
                Reference = entry.Reference,
                IsPosted = entry.IsPosted,
                ChartOfAccountId = entry.ChartOfAccountId,
                CreatedDate = entry.CreatedDate
            };
        }
    }
}
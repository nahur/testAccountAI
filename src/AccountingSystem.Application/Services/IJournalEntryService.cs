using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AccountingSystem.Application.DTOs;

namespace AccountingSystem.Application.Services
{
    public interface IJournalEntryService
    {
        Task<JournalEntryDTO> GetEntryByIdAsync(int id);
        Task<IEnumerable<JournalEntryDTO>> GetAllEntriesAsync();
        Task<IEnumerable<JournalEntryDTO>> GetEntriesByAccountAsync(int accountId);
        Task<JournalEntryDTO> CreateEntryAsync(JournalEntryDTO dto);
        Task<JournalEntryDTO> UpdateEntryAsync(int id, JournalEntryDTO dto);
        Task<bool> DeleteEntryAsync(int id);
        Task<bool> PostEntryAsync(int id);
        Task<bool> ValidateDoubleEntryAsync(decimal debit, decimal credit);
    }
}
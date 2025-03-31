using Core.Entities;

namespace Core.Interfaces;

public interface IJournalRepository
{
    Task<JournalEntry> AddAsync(JournalEntry entry);
    Task<JournalEntry?> GetJournalByIdAsync(int journalId);
}

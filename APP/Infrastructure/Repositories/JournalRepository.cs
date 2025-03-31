using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class JournalRepository : IJournalRepository
{
	private readonly AppDbContext _context;

	public JournalRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<JournalEntry> AddAsync(JournalEntry entry)
	{
		_context.JournalEntries.Add(entry);
		await _context.SaveChangesAsync();
		return entry;
	}

	public async Task<JournalEntry?> GetJournalByIdAsync(int journalId)
	{
		return await _context.JournalEntries.FindAsync(journalId);
	}
}

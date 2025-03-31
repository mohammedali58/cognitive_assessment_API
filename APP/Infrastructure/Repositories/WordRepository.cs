using Core.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class WordRepository : IWordRepository
{
	private readonly AppDbContext _context;

	public WordRepository(AppDbContext context)
	{
		_context = context;
	}

	public async Task<int> GetWordScoresAsync(List<string> wordList)
	{
		var existingList = await _context.Words
													.Where(w => wordList.Contains(w.Text))
													.Select(w => w.Text.ToLower())
													.ToListAsync();

		return existingList.Count();
	}

}

using APP.Application.Common;
using Core.Interfaces;

namespace Application.Services;

public class JournalScoringService : IJournalScoringService
{
	private readonly IWordRepository _wordRepo;

	public JournalScoringService(IWordRepository wordRepo)
	{
		_wordRepo = wordRepo;
	}

	public async Task<int> ScoreTextAsync(string text)
	{
		var words = text.ToLower().Split(" ").ToList();
		var wordsScore = await _wordRepo.GetWordScoresAsync(words);
		return wordsScore;
	}
}

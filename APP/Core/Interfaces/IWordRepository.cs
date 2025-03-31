using Core.Entities;

namespace Core.Interfaces;

public interface IWordRepository
{
	Task<int> GetWordScoresAsync(List<string> wordList);
}

namespace APP.Application.Common
{
	public interface IJournalScoringService
	{
		Task<int> ScoreTextAsync(string text);
	}
}

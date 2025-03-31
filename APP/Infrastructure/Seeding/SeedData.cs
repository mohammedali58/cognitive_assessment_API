using Core.Entities;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace APP.Infrastructure.Seeding
{
	public static class DataSeeder
	{
		public static void SeedData(ModelBuilder modelBuilder)
		{
			var basePath = AppContext.BaseDirectory;
			var filePath = Path.Combine(basePath, "categories_dictionary.json");
			if (!File.Exists(filePath))
			{
				Console.WriteLine("file does not exists!");
				return;
			}

			string jsonData = File.ReadAllText(filePath);
			var wordData = JsonSerializer.Deserialize<Dictionary<string, List<string>>>(jsonData);

			if (wordData is null)
			{
				Console.WriteLine("no word data found in the seeding file!");
				return;
			}

			var wordList = new List<Word>();
			int initialListId = 1;

			foreach (var category in wordData)
			{
				foreach (var word in category.Value)
				{
					wordList.Add(new Word { Id = initialListId++, Category = category.Key, Text = word });
				}
			}

			modelBuilder.Entity<Word>().HasData(wordList);

		}
	}
}

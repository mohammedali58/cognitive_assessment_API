using Microsoft.EntityFrameworkCore;
using Core.Entities;
using APP.Infrastructure.Seeding;

namespace Infrastructure.Data;

public class AppDbContext : DbContext
{
	public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

	public DbSet<User> Users => Set<User>();
	public DbSet<JournalEntry> JournalEntries => Set<JournalEntry>();
	public DbSet<Word> Words => Set<Word>();

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

		DataSeeder.SeedData(modelBuilder);

		//modelBuilder.Entity<Word>().HasData(
		//    new Word { Id = 1, Text = "happy", Category = "positive", Score = 1 },
		//    new Word { Id = 2, Text = "joy", Category = "positive", Score = 1 },
		//    new Word { Id = 3, Text = "love", Category = "positive", Score = 1 },
		//    new Word { Id = 4, Text = "excited", Category = "positive", Score = 1 },
		//    new Word { Id = 5, Text = "great", Category = "positive", Score = 1 },
		//    new Word { Id = 6, Text = "sad", Category = "negative", Score = -1 },
		//    new Word { Id = 7, Text = "angry", Category = "negative", Score = -1 },
		//    new Word { Id = 8, Text = "hate", Category = "negative", Score = -1 },
		//    new Word { Id = 9, Text = "terrible", Category = "negative", Score = -1 },
		//    new Word { Id = 10, Text = "bad", Category = "negative", Score = -1 }
		//);
	}
}

namespace Core.Entities;

public class Word
{
    public int Id { get; set; }
    public required string Text { get; set; }
    public required string Category { get; set; }
}

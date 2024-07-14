namespace PhilosopherBot.Models;

public class Quote
{
    public int Id { get; set; }
    public string Author { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string? CommentAboutText { get; set; }
    public string? ImagePath { get; set; }
    public string? CommentAboutImage { get; set; }
    public string? StickerUrl { get; set; }
}
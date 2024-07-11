using System.Text.Json;

namespace PhilosopherBot.Utils;

public class Quote
{
    public int Id { get; set; }
    public string Author { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string? ImagePath { get; set; }
    public string? StickerUrl { get; set; }

    public static Dictionary<string, List<Quote>> ParseQuotesJsonToDictionary(string jsonPath)
    {
        try
        {
            var jsonAsString = File.ReadAllText(jsonPath);
            var result = JsonSerializer.Deserialize<Dictionary<string, List<Quote>>>(jsonAsString);
            return result ?? new Dictionary<string, List<Quote>>();
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine("Такога файлу з цытатамі не існуе: ", ex.Message);
            return new Dictionary<string, List<Quote>>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine("Не атрымліваецца распарсіць JSON: ", ex.Message);
            return new Dictionary<string, List<Quote>>();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Нечаканая памылка: ", ex.Message);
            return new Dictionary<string, List<Quote>>();
        }
    }
}
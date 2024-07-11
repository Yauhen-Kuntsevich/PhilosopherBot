using System.Text.Json;

namespace PhilosopherBot.Utils;

class Quote
{
    public int Id { get; set; }
    public string Author { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string? ImagePath { get; set; }
    public string? StickerUrl { get; set; }

    public Quote GetRandomQuoteByTopic(Dictionary<string, List<Quote>> quotesDict, string topic)
    {
        var quotes = new List<Quote>();

        if (quotesDict.ContainsKey(topic))
        {
            quotes = quotesDict[topic];
        }

        var rnd = new Random();
        var randomIndex = rnd.Next(0, quotes.Count);
        var randomQuote = quotes[randomIndex];

        return randomQuote;
    }

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
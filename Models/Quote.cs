using System.Text.Json;

namespace PhilosopherBot.Models;

class Quote
{
    public int Id { get; set; }
    public string Author { get; set; } = null!;
    public string Text { get; set; } = null!;

    public Quote GetRandomQuoteByTopic(string jsonPath, string topic)
    {
        var quotesDict = ParseQuotesJsonToDictionary(jsonPath);
        var quotes = new List<Quote>();

        if (quotesDict.ContainsKey(topic))
        {
            quotes = quotesDict[topic];
        }

        Console.WriteLine(quotes.Count);

        var rnd = new Random();
        var randomIndex = rnd.Next(0, quotes.Count);
        Console.WriteLine(randomIndex);
        var randomQuote = quotes[randomIndex];
        Console.WriteLine(randomQuote.Id);

        return randomQuote;
    }

    private Dictionary<string, List<Quote>> ParseQuotesJsonToDictionary(string jsonPath)
    {
        var jsonAsString = File.ReadAllText(jsonPath);
        var result = JsonSerializer.Deserialize<Dictionary<string, List<Quote>>>(jsonAsString);

        if (result == null)
        {
            return new Dictionary<string, List<Quote>>();
        }

        return result;
    }
}
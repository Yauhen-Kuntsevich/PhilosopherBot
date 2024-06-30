using System.Text.Json;

namespace PhilosopherBot.Models;

class RandomQuote
{
    public int Id { get; set; }
    public string Author { get; set; } = null!;
    public string Text { get; set; } = null!;
    public string Image { get; set; } = null!;

    public RandomQuote GetRandomQuoteByTopic(string jsonPath, string topic)
    {
        var quotesDict = ParseQuotesJsonToDictionary(jsonPath);
        var quotes = new List<RandomQuote>();

        if (quotesDict.ContainsKey(topic))
        {
            quotes = quotesDict[topic];
        }

        var rnd = new Random();
        var randomIndex = rnd.Next(0, quotes.Count);
        var randomQuote = quotes[randomIndex];

        return randomQuote;
    }

    public Dictionary<string, List<RandomQuote>> ParseQuotesJsonToDictionary(string jsonPath)
    {
        var jsonAsString = File.ReadAllText(jsonPath);
        var result = JsonSerializer.Deserialize<Dictionary<string, List<RandomQuote>>>(jsonAsString);

        return result ?? new Dictionary<string, List<RandomQuote>>();
    }
}
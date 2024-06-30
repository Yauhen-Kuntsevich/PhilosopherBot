using System.Text.Json;

namespace PhilosopherBot.Models;

class RandomQuote
{
    public int Id { get; set; }
    public string Author { get; set; } = null!;
    public string Text { get; set; } = null!;

    public RandomQuote GetRandomQuoteByTopic(string jsonPath, string topic)
    {
        var quotesDict = ParseQuotesJsonToDictionary(jsonPath);
        var quotes = new List<RandomQuote>();

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

    private Dictionary<string, List<RandomQuote>> ParseQuotesJsonToDictionary(string jsonPath)
    {
        var jsonAsString = File.ReadAllText(jsonPath);
        var result = JsonSerializer.Deserialize<Dictionary<string, List<RandomQuote>>>(jsonAsString);

        if (result == null)
        {
            return new Dictionary<string, List<RandomQuote>>();
        }

        return result;
    }
}
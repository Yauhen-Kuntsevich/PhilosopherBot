using PhilosopherBot.Models;

namespace PhilosopherBot.Repositories;

public class QuotesRepository
{
    private readonly Dictionary<string, Quote[]> _quotesDict;

    public QuotesRepository(Dictionary<string, Quote[]> quotesDict)
    {
        _quotesDict = quotesDict;
    }

    public string[] GetAllTopics()
    {
        return _quotesDict.Keys.ToArray();
    }

    public Quote[] GetQuotesByTopic(string topic)
    {
        return _quotesDict.TryGetValue(topic, out var quotes) ? quotes : [];
    }

    public Quote[] GetQuotesByPhilosopher(string philosopherName)
    {
        return _quotesDict.Values
                   .SelectMany(quotes => quotes.Select(q => q))
                   .Where(q => q.Author.Equals(philosopherName))
                   .ToArray();
    }

    public string[] GetAllPhilosophers()
    {
        return _quotesDict.Values
                   .SelectMany(quotes => quotes.Select(q => q.Author))
                   .Where(a => !string.IsNullOrEmpty(a))
                   .Distinct()
                   .ToArray();
    }
}

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

    public List<Quote> GetQuotesByPhilosopher(string philosopherName)
    {
        var quotesByPhilosopher = new List<Quote>();

        foreach (var key in _quotesDict.Keys)
        {
            foreach (var quote in _quotesDict[key])
            {
                if (quote.Author.Equals(philosopherName))
                {
                    quotesByPhilosopher.Add(quote);
                }
            }
        }

        return quotesByPhilosopher;
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

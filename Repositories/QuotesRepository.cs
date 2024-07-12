using PhilosopherBot.Models;

namespace PhilosopherBot.Repositories;

public class QuotesRepository
{
    private readonly Dictionary<string, Quote[]> _quotesDict;

    public QuotesRepository(Dictionary<string, Quote[]> quotesDict)
    {
        _quotesDict = quotesDict;
    }

    public List<string> GetAllTopics()
    {
        var topics = new List<string>();

        foreach (var key in _quotesDict.Keys)
        {
            topics.Add(key);
        }

        return topics;
    }

    public Quote[] GetQuotesByTopic(string topic)
    {
        foreach (var key in _quotesDict.Keys)
        {
            if (key.Equals(topic))
            {
                return _quotesDict[key];
            }
        }

        return [];
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

    public List<string> GetAllPhilosophers()
    {
        var authors = new List<string>();

        foreach (var key in _quotesDict.Keys)
        {
            foreach (var quote in _quotesDict[key])
            {
                authors.Add(quote.Author);
            }
        }

        return authors.Where(a => a != null).Distinct().ToList();
    }
}

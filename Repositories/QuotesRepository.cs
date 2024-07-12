using System.Text.Json;

namespace PhilosopherBot.Models;

public class QuotesRepository
{
    private readonly string _pathToQuotes = "./Data/quotes.json";

    public List<string> GetAllTopics()
    {
        var quotesDict = GetQuotesDictionaryFromJson();
        var topics = new List<string>();

        foreach (var key in quotesDict.Keys)
        {
            topics.Add(key);
        }

        return topics;
    }

    public List<Quote> GetQuotesByTopic(string topic)
    {
        var quotesDict = GetQuotesDictionaryFromJson();

        foreach (var key in quotesDict.Keys)
        {
            if (key.Equals(topic))
            {
                return quotesDict[key];
            }
        }

        return new List<Quote>();
    }

    public List<Quote> GetQuotesByPhilosopher(string philosopherName)
    {
        var quotesDict = GetQuotesDictionaryFromJson();
        var quotesByPhilosopher = new List<Quote>();

        foreach (var key in quotesDict.Keys)
        {
            foreach (var quote in quotesDict[key])
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
        var quotesDict = GetQuotesDictionaryFromJson();
        var authors = new List<string>();

        foreach (var key in quotesDict.Keys)
        {
            foreach (var quote in quotesDict[key])
            {
                authors.Add(quote.Author);
            }
        }

        return authors.Where(a => a != null).Distinct().ToList();
    }

    private Dictionary<string, List<Quote>> GetQuotesDictionaryFromJson()
    {
        try
        {
            var jsonAsString = File.ReadAllText(_pathToQuotes);
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
            Console.WriteLine($"Нечаканая памылка: {ex.Message}");
            return new Dictionary<string, List<Quote>>();
        }
    }

}

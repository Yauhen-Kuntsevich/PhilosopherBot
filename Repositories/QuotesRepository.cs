using System.Text.Json;

namespace PhilosopherBot.Models;

public class QuotesRepository
{
    private readonly string _pathToQuotes = "./Data/quotes.json";

    public Dictionary<string, List<Quote>> GetQuotesDictionary()
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

    public List<string> GetAllAuthors()
    {
        var quotesDict = GetQuotesDictionary();
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
}

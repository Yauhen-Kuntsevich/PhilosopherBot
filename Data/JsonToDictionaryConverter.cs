using System.Text.Json;
using PhilosopherBot.Models;

namespace PhilosopherBot.Data;

public class JsonToDictionaryConverter
{
    private readonly string _pathToJson = "./Data/quotes.json";

    public Dictionary<string, Quote[]> Convert()
    {
        try
        {
            var jsonAsString = File.ReadAllText(_pathToJson);
            var result = JsonSerializer.Deserialize<Dictionary<string, Quote[]>>(jsonAsString);
            return result ?? new Dictionary<string, Quote[]>();
        }
        catch (FileNotFoundException ex)
        {
            Console.WriteLine($"Такога файлу з цытатамі не існуе:  {ex.Message}");
            return new Dictionary<string, Quote[]>();
        }
        catch (JsonException ex)
        {
            Console.WriteLine($"Не атрымліваецца распарсіць JSON: {ex.Message}");
            return new Dictionary<string, Quote[]>();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Нечаканая памылка: {ex.Message}");
            return new Dictionary<string, Quote[]>();
        }
    }
}

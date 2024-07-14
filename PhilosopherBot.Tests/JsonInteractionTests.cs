using PhilosopherBot.Data;
using PhilosopherBot.Models;

namespace PhilosopherBot.Tests.Data;

public class JsonInteractionTests
{
    [Fact]
    public void ParseJsonToQuotesDict_ValidJsonExists_ReturnsNotEmptyDictionary()
    {
        var sut = new JsonInteraction("./quotes.json");

        var result = sut.ParseJsonToQuotesDict();

        Assert.IsType<Dictionary<string, Quote[]>>(result);
        Assert.NotEmpty(result);
    }

    [Fact]
    public void ParseJsonToQuotesDict_FileNotFound_ReturnsNewEmptyDictionary()
    {
        var sut = new JsonInteraction("./do-not-exist.json");

        var result = sut.ParseJsonToQuotesDict();

        Assert.IsType<Dictionary<string, Quote[]>>(result);
        Assert.Empty(result);
    }

    [Fact]
    public void ParseJsonToQuotesDict_JsonIsInvalid_ReturnsEmptyDictionary()
    {
        var sut = new JsonInteraction("./invalid_quotes.json");

        var result = sut.ParseJsonToQuotesDict();

        Assert.IsType<Dictionary<string, Quote[]>>(result);
        Assert.Empty(result);
    }
}

using PhilosopherBot.Models;
using PhilosopherBot.Repositories;

namespace PhilosopherBot.Tests;

public class QuotesRepositoryTests
{
    private readonly Dictionary<string, Quote[]> _quotesDict;

    public QuotesRepositoryTests()
    {
        var awesomeQuote = new Quote
        {
            Id = 1,
            Author = "Файны аўтар",
            Text = "Файная цытата",
            ImagePath = "./path/to/awesome/quote"
        };

        var veryAwesomeQuote = new Quote
        {
            Id = 2,
            Author = "Вельмі файны аўтар",
            Text = "Вельмі файная цытата",
            ImagePath = "./path/to/very/awesome/quote",
        };

        var mostAwesomeQuote = new Quote
        {
            Id = 3,
            Author = "Самы файны аўтар",
            Text = "Самая файная цытата",
            ImagePath = "./path/to/most/awesome/quote"
        };

        _quotesDict = new Dictionary<string, Quote[]>
        {
            { "Файная тэма", new[] { awesomeQuote, veryAwesomeQuote, mostAwesomeQuote } },
            { "Вельмі файная тэма", new[] { awesomeQuote, mostAwesomeQuote, veryAwesomeQuote }},
            { "Самая файная тэма", new[] { mostAwesomeQuote, awesomeQuote, veryAwesomeQuote }}
        };
    }

    [Fact]
    public void GetAllTopics_DictionaryIsValid_ReturnsArrayOfStringsWithKeys()
    {
        var sut = new QuotesRepository(_quotesDict);

        var result = sut.GetAllTopics();

        Assert.IsType<string[]>(result);
        Assert.Equal(3, result.Length);
    }

    [Fact]
    public void GetQuotesByTopic_DictionaryIsValidAndTopicIsFound_ReturnsArrayOfQuotesByTopic()
    {
        var sut = new QuotesRepository(_quotesDict);

        var result = sut.GetQuotesByTopic("Файная тэма");
        var result2 = sut.GetQuotesByTopic("Вельмі файная тэма");

        Assert.IsType<Quote[]>(result);
        Assert.IsType<Quote[]>(result2);
        Assert.Equal(_quotesDict["Файная тэма"], result);
        Assert.Equal(_quotesDict["Вельмі файная тэма"], result2);
    }

    [Fact]
    public void GetQuotesByTopic_DictionaryIsValidButTopicIsNotFound_ReturnNewEmptyArrayOfQuoetes()
    {
        var sut = new QuotesRepository(_quotesDict);

        var result = sut.GetQuotesByTopic("Так сабе цытата");

        Assert.IsType<Quote[]>(result);
        Assert.Empty(result);
    }

    [Fact]
    public void GetQuotesByPhilosopher_DictionaryIsValidAndPhilosopherIsFound_ReturnsArrayOfQuotesOfPhilosopher()
    {
        var awesomeQuote = new Quote
        {
            Id = 1,
            Author = "Файны аўтар",
            Text = "Файная цытата",
            ImagePath = "./path/to/awesome/quote",
            StickerUrl = null
        };

        var veryAwesomeQuote = new Quote
        {
            Id = 2,
            Author = "Вельмі файны аўтар",
            Text = "Вельмі файная цытата",
            ImagePath = "./path/to/very/awesome/quote",
            StickerUrl = null
        };

        var mostAwesomeQuote = new Quote
        {
            Id = 3,
            Author = "Самы файны аўтар",
            Text = "Самая файная цытата",
            ImagePath = "./path/to/most/awesome/quote",
            StickerUrl = null
        };
        var sut = new QuotesRepository(_quotesDict);

        var result = sut.GetQuotesByPhilosopher("Файны аўтар");
        var result2 = sut.GetQuotesByPhilosopher("Самы файны аўтар");

        Assert.IsType<Quote[]>(result);
        Assert.IsType<Quote[]>(result2);
        Assert.NotEmpty(result);
        Assert.NotEmpty(result2);
    }

    [Fact]
    public void GetAllPhilosophers_DictionaryIsValid_ReturnsArrayOfPhilosophersNames()
    {
        var sut = new QuotesRepository(_quotesDict);

        var result = sut.GetAllPhilosophers();

        Assert.IsType<string[]>(result);
        Assert.NotEmpty(result);
    }
}
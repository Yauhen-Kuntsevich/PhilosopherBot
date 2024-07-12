using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using PhilosopherBot.Models;

namespace PhilosopherBot.Handlers;

public class TopicChoiceHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly long _chatId;
    private readonly Dictionary<string, List<Quote>> _quotesDict;
    private readonly string _topic;

    public TopicChoiceHandler(
        ITelegramBotClient bot,
        long chatId,
        Dictionary<string, List<Quote>> quotesDict,
        string topic)
    {
        _bot = bot;
        _chatId = chatId;
        _quotesDict = quotesDict;
        _topic = topic;
    }

    public async Task Handle()
    {
        var randomQuote = GetRandomQuoteByTopic();
        await SendRandomQuote(randomQuote);
        await SendAuthorImage(randomQuote);

    }

    private Quote GetRandomQuoteByTopic()
    {
        var quotes = new List<Quote>();

        if (_quotesDict.ContainsKey(_topic))
        {
            quotes = _quotesDict[_topic];
        }

        var rnd = new Random();
        var randomIndex = rnd.Next(0, quotes.Count);
        var randomQuote = quotes[randomIndex];

        return randomQuote;
    }

    private async Task<Message> SendRandomQuote(Quote randomQuote)
    {
        return await _bot.SendTextMessageAsync(
            _chatId,
            $"{randomQuote.Text}\n\n<b>{randomQuote.Author}</b>",
            parseMode: ParseMode.Html
        );
    }

    private async Task<Message> SendAuthorImage(Quote randomQuote)
    {
        if (System.IO.File.Exists(randomQuote.ImagePath))
        {
            using var stream = new FileStream(randomQuote.ImagePath, FileMode.Open, FileAccess.Read);
            return await _bot.SendPhotoAsync(
                _chatId,
                stream
            );
        }

        return await _bot.SendTextMessageAsync(_chatId, "Выявы гэтага філосафа ў маёй базе пакуль няма 😢");
    }
}

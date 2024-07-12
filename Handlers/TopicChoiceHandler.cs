using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using PhilosopherBot.Models;
using PhilosopherBot.Contracts;

namespace PhilosopherBot.Handlers;

public class TopicChoiceHandler : IMessageHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly long _chatId;
    private readonly List<Quote> _quotes;

    public TopicChoiceHandler(
        ITelegramBotClient bot,
        long chatId,
        List<Quote> quotes
    )
    {
        _bot = bot;
        _chatId = chatId;
        _quotes = quotes;
    }

    public async Task Handle()
    {
        var randomQuote = GetRandomQuoteByTopic();
        await SendRandomQuote(randomQuote);
        await SendAuthorImage(randomQuote);
    }

    private Quote GetRandomQuoteByTopic()
    {
        var rnd = new Random();
        var randomIndex = rnd.Next(0, _quotes.Count);
        var randomQuote = _quotes[randomIndex];

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

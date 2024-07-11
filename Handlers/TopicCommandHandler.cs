using Telegram.Bot;
using Telegram.Bot.Types;
using PhilosopherBot.Utils;
using PhilosopherBot.Contracts;

namespace PhilosopherBot.Handlers;

public class TopicCommandHandler : ICommandHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly long _chatId;
    private readonly Dictionary<string, List<Quote>> _quotesDict;

    public TopicCommandHandler(
        ITelegramBotClient bot,
        long chatId,
        Dictionary<string, List<Quote>> quotesDict
    )
    {
        _bot = bot;
        _chatId = chatId;
        _quotesDict = quotesDict;
    }

    public async Task Handle()
    {
        await ReactToCommandWithKeyboard();
    }

    private async Task<Message> ReactToCommandWithKeyboard()
    {
        var keyboard = new KeyboardsManufactory().CreateKeyboard(_quotesDict);

        return await _bot.SendTextMessageAsync(
            _chatId,
            "На якую тэму ты хочаш атрымаць цытату?",
            replyMarkup: keyboard
        );
    }
}

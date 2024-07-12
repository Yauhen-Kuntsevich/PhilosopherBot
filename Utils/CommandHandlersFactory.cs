using Telegram.Bot;
using PhilosopherBot.Handlers;
using PhilosopherBot.Contracts;

namespace PhilosopherBot.Models;

public class CommandHandlersFactory
{
    private readonly string _command;
    private readonly ITelegramBotClient _bot;
    private readonly long _chatId;
    private readonly Dictionary<string, List<Quote>> _quotesDict;

    public CommandHandlersFactory(
        string command,
        ITelegramBotClient bot,
        long chatId,
        Dictionary<string, List<Quote>> quoteDict
    )
    {
        _command = command;
        _bot = bot;
        _chatId = chatId;
        _quotesDict = quoteDict;
    }

    public ICommandHandler CreateHandler()
    {
        switch (_command)
        {
            case "/start":
                return new StartCommandHandler(
                    _bot,
                    _chatId
                );
            case "/topic":
                return new TopicCommandHandler(
                    _bot,
                    _chatId,
                    _quotesDict
                );
            default:
                return new UnknownCommandHandler(
                    _bot,
                    _chatId
                );
        }
    }
}

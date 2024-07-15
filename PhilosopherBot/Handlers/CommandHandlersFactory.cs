using Telegram.Bot;
using PhilosopherBot.Contracts;

namespace PhilosopherBot.Handlers;

public class CommandHandlersFactory
{
    private readonly string _command;
    private readonly ITelegramBotClient _bot;
    private readonly long _chatId;
    private readonly string[] _topics;
    private readonly string[] _philosophers;

    public CommandHandlersFactory(
        string command,
        ITelegramBotClient bot,
        long chatId,
        string[] topics,
        string[] philosophers
    )
    {
        _command = command;
        _bot = bot;
        _chatId = chatId;
        _topics = topics;
        _philosophers = philosophers;
    }

    public IMessageHandler CreateHandler()
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
                    _topics
                );
            case "/philosopher":
                return new PhilosopherCommandHandler(
                    _bot,
                    _chatId,
                    _philosophers
                );
            default:
                return new UnknownCommandHandler(
                    _bot,
                    _chatId
                );
        }
    }
}

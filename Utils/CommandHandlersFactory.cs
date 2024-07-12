using Telegram.Bot;
using PhilosopherBot.Handlers;
using PhilosopherBot.Contracts;

namespace PhilosopherBot.Models;

public class CommandHandlersFactory
{
    private readonly string _command;
    private readonly ITelegramBotClient _bot;
    private readonly long _chatId;
    private readonly List<string> _topics;
    private readonly List<string> _philosophers;

    public CommandHandlersFactory(
        string command,
        ITelegramBotClient bot,
        long chatId,
        List<string> topics,
        List<string> philosophers
    )
    {
        _command = command;
        _bot = bot;
        _chatId = chatId;
        _topics = topics;
        _philosophers = philosophers;
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

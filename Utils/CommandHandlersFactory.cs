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

    public CommandHandlersFactory(
        string command,
        ITelegramBotClient bot,
        long chatId,
        List<string> topics
    )
    {
        _command = command;
        _bot = bot;
        _chatId = chatId;
        _topics = topics;
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
            default:
                return new UnknownCommandHandler(
                    _bot,
                    _chatId
                );
        }
    }
}

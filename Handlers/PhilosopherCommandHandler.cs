using Telegram.Bot;
using PhilosopherBot.Contracts;

namespace PhilosopherBot.Handlers;

public class PhilosopherCommandHandler : ICommandHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly long _chatId;

    public Task Handle()
    {
        throw new NotImplementedException();
    }
}

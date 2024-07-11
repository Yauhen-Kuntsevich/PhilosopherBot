using PhilosopherBot.Contracts;

namespace PhilosopherBot.Handlers;

public class UnknownCommandHandler : ICommandHandler
{
    public Task Handle()
    {
        throw new NotImplementedException();
    }
}

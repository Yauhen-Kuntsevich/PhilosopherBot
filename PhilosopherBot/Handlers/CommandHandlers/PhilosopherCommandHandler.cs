using Telegram.Bot;
using Telegram.Bot.Types;
using PhilosopherBot.Contracts;
using PhilosopherBot.Models;

namespace PhilosopherBot.Handlers;

public class PhilosopherCommandHandler : IMessageHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly long _chatId;
    private readonly string[] _philosophers;

    public PhilosopherCommandHandler(
        ITelegramBotClient bot,
        long chatId,
        string[] philosophers
    )
    {
        _bot = bot;
        _chatId = chatId;
        _philosophers = philosophers;
    }

    public async Task Handle()
    {
        await ReactOnCommandWithKeyboard();
    }

    private async Task<Message> ReactOnCommandWithKeyboard()
    {
        var keyboard = new KeyboardsManufactory().CreateKeyboard(_philosophers);

        return await _bot.SendTextMessageAsync(
            _chatId,
            "Цытату якога філосафа ты хочаш атрымаць?",
            replyMarkup: keyboard
        );
    }
}

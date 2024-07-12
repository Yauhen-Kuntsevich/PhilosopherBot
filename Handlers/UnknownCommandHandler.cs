using Telegram.Bot;
using Telegram.Bot.Types;
using PhilosopherBot.Contracts;

namespace PhilosopherBot.Handlers;

public class UnknownCommandHandler : ICommandHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly long _chatId;

    public UnknownCommandHandler(ITelegramBotClient bot, long chatId)
    {
        _bot = bot;
        _chatId = chatId;
    }

    public async Task Handle()
    {
        await SendHelperMessage();
    }

    private async Task<Message> SendHelperMessage()
    {
        return await _bot.SendTextMessageAsync(_chatId, "На жаль, я не ведаю такой каманды 😔 Каб знайсці спіс даступных камандаў, звярніся да кнопкі Menu злева ад поля ўводу або дашлі мне каманду /help 🙂");
    }

}

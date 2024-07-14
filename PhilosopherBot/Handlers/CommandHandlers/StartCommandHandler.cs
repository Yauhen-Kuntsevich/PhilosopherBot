using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using PhilosopherBot.Contracts;

namespace PhilosopherBot.Models;

public class StartCommandHandler : IMessageHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly long _chatId;

    public StartCommandHandler(ITelegramBotClient bot, long chatId)
    {
        _bot = bot;
        _chatId = chatId;
    }

    public Task Handle()
    {
        return SendGreetings();
    }

    private async Task<Message> SendGreetings()
    {
        return await _bot.SendTextMessageAsync(
            _chatId,
            "Вітанкі! Я бот, створаны Афінай, персанажкай <a href=\"https://yauhen-kuntsevich.github.io/luxation-of-thinking/\">аднаго падручніка па філасофіі</a>. Вядома, я не сапраўдны філосаф, але сёе-тое ўмею 🤖\n\n📝 Калі ты хочаш атрымаць выпадковую цытату на нейкую важную тэму, скарыстайся камандай /topic\n\n🧙‍♂ А калі хочаш атрымаць выпадковую думку нейкага філосафа, напішы мне /philosopher\n\n🤓 Каманда /bio дапаможа табе атрымаць кароткую біяграфію выпадковага філосафа ці філасафіні з цікавымі фактамі з іх жыцця.\n\n🕵‍♀ Калі ты ведаеш, пра каго хочаш пачытаць, дапоўні каманду /bio прозвішчам або іменем, напрыклад, /bio Кант.\n\n😿 У мяне няма ўсіх на свеце цытатаў, таму можа так здарыцца, што цытаты патрэбных табе мысляроў і мыслярак тут не знойдзецца.\n\n📚 Каманда /works дашле табе спіс кніжак, выкарыстаных пры стварэнні гэтага боту, і проста кніжкі, на якія можна звярнуць увагу.\n\n🦾 Усе гэтыя каманды ты знойдзеш, калі націснеш на кнопку Menu у левым ніжнім куце. А каманда /help дапаможа табе, калі ты нешта забудзеш.",
            parseMode: ParseMode.Html,
            replyMarkup: new InlineKeyboardMarkup(
                InlineKeyboardButton.WithUrl("Сайт падручніка", "https://yauhen-kuntsevich.github.io/luxation-of-thinking/")
            )
        );
    }
}

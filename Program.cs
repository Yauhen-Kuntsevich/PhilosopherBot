using PhilosopherBot.Models;
using dotenv.net;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

DotEnv.Load();
var envVars = DotEnv.Read();

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient(envVars["BOT_API_KEY"]);

var commands = new[] {
    new BotCommand { Command = "/topic", Description = "Цытата паводле тэмы" },
    new BotCommand { Command = "/philosopher", Description = "Цытата паводле імені філосафа" },
    new BotCommand {Command = "/bio", Description = "Біяграфія філосафа / філасафіні" },
    new BotCommand {Command = "/help", Description = "Як я працую?"},
};
await bot.SetMyCommandsAsync(commands);

bot.StartReceiving(
    updateHandler: HandleUpdate,
    pollingErrorHandler: (bot, ex, ct) =>
    {
        Console.WriteLine(ex);
        return Task.CompletedTask;
    },
    cancellationToken: cts.Token
);

var me = await bot.GetMeAsync();
Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel(); // stop the bot

// method that handle updates coming for the bot:
async Task HandleUpdate(ITelegramBotClient bot, Update update, CancellationToken ct)
{
    if (update.Message is null) return;
    if (update.Message.Text is null) return;
    var msg = update.Message;
    Console.WriteLine($"Received message '{msg.Text}' in {msg.Chat}");

    if (msg.Text.Trim().Equals("/start"))
    {
        await bot.SendTextMessageAsync(msg.Chat, "Вітанкі! Я бот, створаны Афінай, персанажкай аднаго падручніка па філасофіі. Вядома, я не сапраўдны філосаф, але сёе-тое ўмею🤖\n\n🤖Калі ты хочаш атрымаць выпадковую цытату на нейкую важную тэму, скарыстайся камандай /topic\n\n🤖А калі хочаш атрымаць выпадковую думку нейкага філосафа, напішы мне /philosopher\n\n🤖Каманда /bio дапаможа табе атрымаць кароткую біяграфію выпадковага філосафа ці філасафіні з цікавымі фактамі з іх жыцця.\n\n🤖Калі ты ведаеш, пра каго хочаш пачытаць, дапоўні каманду /bio прозвішчам або іменем, напрыклад, /bio Кант.\n\n🤖У мяне няма ўсіх на свеце цытатаў, таму можа так здарыцца, што цытаты патрэбных табе мысляроў і мыслярак тут не знойдзецца.\n\n🤖Усе гэтыя каманды ты знойдзеш, калі націснеш на кнопку Menu у левым ніжнім куце. А каманда /help дапаможа табе, калі ты нешта забудзеш.");
    }

    var quote = new RandomQuote();
    var quotesDict = quote.ParseQuotesJsonToDictionary("./Data/quotes.json");

    if (msg.Text.Equals("/topic"))
    {
        var buttons = new List<KeyboardButton[]>();
        var buttonsRow = new List<KeyboardButton>();

        foreach (var key in quotesDict.Keys)
        {
            buttonsRow.Add(new KeyboardButton(key));

            if (buttonsRow.Count == 2)
            {
                buttons.Add(buttonsRow.ToArray());
                buttonsRow = new List<KeyboardButton>();
            }
        }

        if (buttonsRow.Count > 0)
        {
            buttons.Add(buttonsRow.ToArray());
        }

        var replyKeyboard = new ReplyKeyboardMarkup(buttons)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true,
        };

        await bot.SendTextMessageAsync(msg.Chat, "На якую тэму ты хочаш атрымаць цытату?", replyMarkup: replyKeyboard);
    }

    foreach (var key in quotesDict.Keys)
    {
        if (msg.Text.ToLower() == key.ToLower())
        {
            var randomQuote = quote.GetRandomQuoteByTopic("./Data/quotes.json", key);
            await bot.SendTextMessageAsync(
                msg.Chat,
                $"{randomQuote.Text}\n\n<b>{randomQuote.Author}</b>",
                parseMode: ParseMode.Html
            );
        }
    }

    if (msg.Text == "/philosopher")
    {
        await bot.SendTextMessageAsync(msg.Chat, "Цытату якога філосафа ты хочаш атрымаць?");
    }
}

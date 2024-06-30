using PhilosopherBot.Models;
using dotenv.net;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

DotEnv.Load();
var envVars = DotEnv.Read();

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient(envVars["BOT_API_KEY"], cancellationToken: cts.Token);

var commands = new[] {
    new BotCommand { Command = "/topic", Description = "Цытата паводле тэмы" },
    new BotCommand { Command = "/philosopher", Description = "Цытата паводле імені філосафа" },
    new BotCommand { Command = "/bio", Description = "Біяграфія філосафа / філасафіні" },
    new BotCommand { Command = "/works", Description = "Працы, з якіх браліся цытаты" },
    new BotCommand { Command = "/help", Description = "Як я працую?" },
};
await bot.SetMyCommandsAsync(commands);

bot.StartReceiving(updateHandler: HandleUpdate, errorHandler: async (bot, ex, ct) => Console.WriteLine(ex));

var me = await bot.GetMeAsync();
Console.WriteLine($"@{me.Username} is running... Press Enter to terminate");
Console.ReadLine();
cts.Cancel(); // stop the bot


async Task HandleUpdate(ITelegramBotClient bot, Update update, CancellationToken ct)
{
    if (update.Message is null) return;
    if (update.Message.Text is null) return;
    var msg = update.Message;
    Console.WriteLine($"Received message '{msg.Text}' in {msg.Chat}");

    if (msg.Text.Equals("/start"))
    {
        await bot.SendTextMessageAsync(msg.Chat, "Вітанкі! Я бот, створаны Афінай, персанажкай аднаго падручніка па філасофіі. Вядома, я не сапраўдны філосаф, але сёе-тое ўмею🤖\n\n🤖Калі ты хочаш атрымаць выпадковую цытату на нейкую важную тэму, скарыстайся камандай /topic\n\n🤖А калі хочаш атрымаць выпадковую думку нейкага філосафа, напішы мне /philosopher\n\n🤖Каманда /bio дапаможа табе атрымаць кароткую біяграфію выпадковага філосафа ці філасафіні з цікавымі фактамі з іх жыцця.\n\n🤖Калі ты ведаеш, пра каго хочаш пачытаць, дапоўні каманду /bio прозвішчам або іменем, напрыклад, /bio Кант.\n\n🤖У мяне няма ўсіх на свеце цытатаў, таму можа так здарыцца, што цытаты патрэбных табе мысляроў і мыслярак тут не знойдзецца.\n\n🤖Усе гэтыя каманды ты знойдзеш, калі націснеш на кнопку Menu у левым ніжнім куце. А каманда /help дапаможа табе, калі ты нешта забудзеш.");
    }

    var quote = new RandomQuote();
    var quotesDict = quote.ParseQuotesJsonToDictionary("./Data/quotes.json");

    if (msg.Text.Equals("/topic"))
    {
        var keyboardsManufactory = new KeyboardsManufactory();
        var replyKeyboard = keyboardsManufactory.CreateKeyboard(quotesDict);

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

            if (System.IO.File.Exists(randomQuote.Image))
            {
                using (var stream = new FileStream(randomQuote.Image, FileMode.Open, FileAccess.Read, FileShare.Read))
                {
                    await bot.SendPhotoAsync(msg.Chat.Id, stream);
                }
            }
        }
    }

    if (msg.Text == "/philosopher")
    {
        await bot.SendTextMessageAsync(msg.Chat, "Цытату якога філосафа ты хочаш атрымаць?");
    }

    if (msg.Text == "/works")
    {
        await bot.SendTextMessageAsync(msg.Chat, "pong");
    }
}

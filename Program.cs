using dotenv.net;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using PhilosopherBot.Utils;
using PhilosopherBot.Handlers;

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
cts.Cancel();


async Task HandleUpdate(ITelegramBotClient bot, Update update, CancellationToken ct)
{
    if (update.Message is null) return;
    if (update.Message.Text is null) return;
    var msg = update.Message;
    Console.WriteLine($"Received message '{msg.Text}' in {msg.Chat}");

    if (msg.Text.Equals("/start"))
    {
        await new StartCommandHandler(bot, msg.Chat.Id).Handle();
    }

    var quotesDict = Quote.ParseQuotesJsonToDictionary("./Data/quotes.json");
    var quote = new Quote();

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
            var randomQuote = quote.GetRandomQuoteByTopic(quotesDict, key);
            await bot.SendTextMessageAsync(
                msg.Chat,
                $"{randomQuote.Text}\n\n<b>{randomQuote.Author}</b>",
                parseMode: ParseMode.Html
            );

            if (System.IO.File.Exists(randomQuote.ImagePath))
            {
                using (var stream = new FileStream(randomQuote.ImagePath, FileMode.Open, FileAccess.Read, FileShare.Read))
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
        await bot.SendTextMessageAsync(msg.Chat, "Гэтая функцыя пакуль распрацоўваецца.");
    }
}

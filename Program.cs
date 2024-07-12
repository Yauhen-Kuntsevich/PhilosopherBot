using dotenv.net;
using Telegram.Bot;
using Telegram.Bot.Types;
using PhilosopherBot.Models;
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

    var quotesRepository = new QuotesRepository();

    var quotesDict = quotesRepository.GetQuotesDictionary();
    var authors = quotesRepository.GetAllAuthors();

    foreach (var author in authors)
    {
        Console.WriteLine(author);
    }

    if (msg.Text.TrimStart().StartsWith('/'))
    {
        await new CommandHandlersFactory(msg.Text, bot, msg.Chat.Id, quotesDict).CreateHandler().Handle();
    }

    foreach (var key in quotesDict.Keys)
    {
        if (msg.Text.Equals(key))
        {
            await new TopicChoiceHandler(bot, msg.Chat.Id, quotesDict, key).Handle();
        }
    }
}

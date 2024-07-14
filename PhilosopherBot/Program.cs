using dotenv.net;
using Telegram.Bot;
using Telegram.Bot.Types;
using PhilosopherBot.Models;
using PhilosopherBot.Data;
using PhilosopherBot.Repositories;

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

    var quotesDict = new JsonInteraction("./Data/quotes.json").ParseJsonToQuotesDict();

    var quotesRepository = new QuotesRepository(quotesDict);

    var topics = quotesRepository.GetAllTopics();
    var philosophers = quotesRepository.GetAllPhilosophers();

    if (msg.Text.TrimStart().StartsWith('/'))
    {
        await new CommandHandlersFactory(msg.Text, bot, msg.Chat.Id, topics, philosophers).CreateHandler().Handle();
    }

    foreach (var topic in topics)
    {
        if (msg.Text.Equals(topic))
        {
            var quotes = quotesRepository.GetQuotesByTopic(topic);
            await new TopicChoiceHandler(bot, msg.Chat.Id, quotes).Handle();
        }
    }

    foreach (var philosopher in philosophers)
    {
        if (msg.Text.Equals(philosopher))
        {
            var quotes = quotesRepository.GetQuotesByPhilosopher(philosopher);
            await new PhilosopherChoiceHandler(bot, msg.Chat.Id, quotes).Handle();
        }
    }
}

using PhilosopherBot.Models;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

using var cts = new CancellationTokenSource();
var bot = new TelegramBotClient("6536318939:AAFJnvfNvM0Nr4pr3mGSwomPMjgH3sxqFds");
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
    if (update.Message is null) return;			// we want only updates about new Message
    if (update.Message.Text is null) return;	// we want only updates about new Text Message
    var msg = update.Message;
    Console.WriteLine($"Received message '{msg.Text}' in {msg.Chat}");
    // let's echo back received text in the chat
    // await bot.SendTextMessageAsync(msg.Chat, $"{msg.From} said: {msg.Text}");
    if (msg.Text.Equals("/ping"))
    {
        var quote = new Quote();
        var randomQuote = quote.GetRandomQuoteByTopic("./Data/quotes.json", "этыка").Text;
        await bot.SendTextMessageAsync(msg.Chat, randomQuote);
    }
}

﻿using Telegram.Bot;
using Telegram.Bot.Types;
using PhilosopherBot.Models;
using PhilosopherBot.Contracts;

namespace PhilosopherBot.Handlers;

public class TopicCommandHandler : ICommandHandler
{
    private readonly ITelegramBotClient _bot;
    private readonly long _chatId;
    private readonly List<string> _topics;

    public TopicCommandHandler(
        ITelegramBotClient bot,
        long chatId,
        List<string> topics
    )
    {
        _bot = bot;
        _chatId = chatId;
        _topics = topics;
    }

    public async Task Handle()
    {
        await ReactOnCommandWithKeyboard();
    }

    private async Task<Message> ReactOnCommandWithKeyboard()
    {
        var keyboard = new KeyboardsManufactory().CreateKeyboard(_topics);

        return await _bot.SendTextMessageAsync(
            _chatId,
            "На якую тэму ты хочаш атрымаць цытату?",
            replyMarkup: keyboard
        );
    }
}

using Telegram.Bot.Types.ReplyMarkups;

namespace PhilosopherBot.Models;

class KeyboardsManufactory
{
    public List<KeyboardButton[]> Buttons { get; set; } = new List<KeyboardButton[]>();
    public List<KeyboardButton> ButtonsRow { get; set; } = new List<KeyboardButton>();

    public ReplyKeyboardMarkup CreateKeyboard(Dictionary<string, List<RandomQuote>> quotesDict)
    {
        foreach (var key in quotesDict.Keys)
        {
            ButtonsRow.Add(new KeyboardButton(key));

            if (ButtonsRow.Count == 2)
            {
                Buttons.Add(ButtonsRow.ToArray());
                ButtonsRow = new List<KeyboardButton>();
            }
        }

        if (ButtonsRow.Count > 0)
        {
            Buttons.Add(ButtonsRow.ToArray());
        }

        var replyKeyboard = new ReplyKeyboardMarkup(Buttons)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true,
        };

        return replyKeyboard;
    }
}
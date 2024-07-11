using Telegram.Bot.Types.ReplyMarkups;

namespace PhilosopherBot.Utils;

class KeyboardsManufactory
{
    public List<KeyboardButton[]> Buttons { get; set; } = new List<KeyboardButton[]>();
    public List<KeyboardButton> ButtonsRow { get; set; } = new List<KeyboardButton>();

    public ReplyKeyboardMarkup CreateKeyboard(Dictionary<string, List<Quote>> quotesDict)
    {
        const int buttonsInRowCount = 2;

        foreach (var key in quotesDict.Keys)
        {
            ButtonsRow.Add(new KeyboardButton(key));

            if (ButtonsRow.Count == buttonsInRowCount)
            {
                Buttons.Add(ButtonsRow.ToArray());
                ButtonsRow = new List<KeyboardButton>();
            }
        }

        if (ButtonsRow.Count > 0)
        {
            Buttons.Add(ButtonsRow.ToArray());
        }

        return new ReplyKeyboardMarkup(Buttons)
        {
            ResizeKeyboard = true,
            OneTimeKeyboard = true,
        };
    }
}
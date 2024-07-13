using Telegram.Bot.Types.ReplyMarkups;

namespace PhilosopherBot.Models;

class KeyboardsManufactory
{
    public List<KeyboardButton[]> Buttons { get; set; } = new List<KeyboardButton[]>();
    public List<KeyboardButton> ButtonsRow { get; set; } = new List<KeyboardButton>();

    public ReplyKeyboardMarkup CreateKeyboard(string[] options)
    {
        const int buttonsInRowCount = 2;

        foreach (var option in options)
        {
            ButtonsRow.Add(new KeyboardButton(option));

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
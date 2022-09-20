using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Handlers.Message_Handlers
{
    internal class DateScheduleHandler : Handler
    {
        public DateScheduleHandler(Dictionary<string, State> dialogs) : base(dialogs)
        {
        }

        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            var dialogId = $"{update.Message.Chat.Id}_{update.Message.From.Id}";

            if (message.Text == "/расписание")
            {
                if (!DBManager.UserExists(message, out var user))
                    await botClient.SendTextMessageAsync(message.Chat, "Вы не зарегистрированы");
                else
                {
                    dialogs[dialogId] = State.DateRequested;
                    await botClient.SendTextMessageAsync(message.Chat, "Введите дату промежуток в формате\n \"dd.mm.yyyy-dd.mm.yyyy\"");
                }                    
            }

            else if (Successor != null)
                Successor.HandleRequestAsync(update, botClient);
        }
    }
}

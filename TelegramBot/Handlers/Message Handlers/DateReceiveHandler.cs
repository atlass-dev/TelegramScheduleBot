using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Handlers.Message_Handlers
{
    internal class DateReceiveHandler : Handler
    {
        public DateReceiveHandler(Dictionary<string, State> dialogs) : base(dialogs)
        {
        }

        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            var dialogId = $"{update.Message.Chat.Id}_{update.Message.From.Id}";

            if (dialogs[dialogId] == State.DateRequested)
            {
                string[] dates = message.Text.Split('-');

                SchedulePrinter.ShowSchedule(DBManager.GetUser(message).Group, dates[0], dates[1], update, botClient);

                return;
            }

            else if (Successor != null)
                Successor.HandleRequestAsync(update, botClient);
        }
    }
}

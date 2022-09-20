using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Handlers.Message_Handlers
{
    internal class ShowScheduleHandler : Handler
    {
        public ShowScheduleHandler(Dictionary<string, State> dialogs) : base(dialogs)
        {
        }

        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            var dialogId = $"{update.Message.Chat.Id}_{update.Message.From.Id}";

            Models.User user;
            if (message.Text == "/завтра")
            {
                if (!DBManager.UserExists(message, out user))
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Вы не зарегистрированы!");
                    return;
                }

                else
                {
                    string tomorrowDate = GetTomorrowDate();
                    SchedulePrinter.ShowSchedule(user.Group, tomorrowDate, tomorrowDate, update, botClient);
                    dialogs[dialogId] = State.TomorrowSchedule;
                }
            }

            else if (Successor != null)
                Successor.HandleRequestAsync(update, botClient);
            
        }

        private string GetTomorrowDate()
        {
            DateTime tomorrowDate = DateTime.Now.AddDays(1);
            string tomorrowMonth = tomorrowDate.Month.ToString().Length > 1 ? tomorrowDate.Month.ToString() : "0" + tomorrowDate.Month;
            return $"{tomorrowDate.Day}.{tomorrowMonth}.{tomorrowDate.Year}";
        }
    }
}

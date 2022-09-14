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
        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;
            Models.User user;
            if (message.Text == "/завтра")
            {
                if (!DBChecker.UserExists(message, out user))
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Вы не зарегистрированы!");
                    return;
                }

                else
                {
                    string tomorrowDate = GetTomorrowDate();
                    SchedulePrinter.ShowSchedule(user.Group, tomorrowDate, tomorrowDate, update, botClient);
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

using ScheduleParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using TelegramBot.Models;

namespace TelegramBot.Handlers.Message_Handlers
{
    internal class GroupSetterHandler : Handler
    {
        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;         
            if (message.Text == "ИВТ-20-1")
            {
                using (UserContext context = new UserContext())
                {
                    context.Add(new Models.User() { Username = message.From.Username, Group = message.Text });
                    context.SaveChangesAsync();
                    await botClient.SendTextMessageAsync(message.Chat, "Вы успешно зарегистрировались!");
                }
            }

            else if (Successor != null)
                Successor.HandleRequestAsync(update, botClient);
        }

        private bool GroupExists(string name)
        {
            return true;
        }     
    }
}

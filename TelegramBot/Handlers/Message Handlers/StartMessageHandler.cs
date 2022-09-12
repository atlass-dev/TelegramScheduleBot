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
    internal class StartMessageHandler : Handler
    {
        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;
            if (message != null && message.Text == "/start")
            {
                Models.User user;

                if (UserExists(message, out user))
                    await botClient.SendTextMessageAsync(message.Chat, "Вы уже зарегистрированы!");

                else
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Введите имя группы");
                }

            }
            else if (Successor != null)
            {
                Successor.HandleRequestAsync(update, botClient);
            }
                
        }

        public bool UserExists(Message message, out Models.User user)
        {
            using (UserContext userContext = new UserContext())
            {
                user = userContext.Users
                                  .Where(u => u.Username == message.From.Username)
                                  .FirstOrDefault();
            }

            return user != null;
        }
    }
}

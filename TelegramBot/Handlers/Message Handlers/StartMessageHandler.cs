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
                using(UserContext userContext = new UserContext())
                {
                    var user = userContext.Users
                                          .Where(u => u.Username == message.From.Username)
                                          .FirstOrDefault();

                    if (user != null)
                        await botClient.SendTextMessageAsync(message.Chat, "Вы уже зарегистрированы!");

                    else
                    {
                        await botClient.SendTextMessageAsync(message.Chat, "Введите имя группы");
                        return;                 
                    }
                        
                }
                
            }
            else if (Successor != null)
            {
                Successor.HandleRequestAsync(update, botClient);
            }
                
        }
    }
}

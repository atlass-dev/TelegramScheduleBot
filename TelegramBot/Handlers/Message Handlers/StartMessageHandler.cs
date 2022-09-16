using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;
using TelegramBot.Models;

namespace TelegramBot.Handlers.Message_Handlers
{
    internal class StartMessageHandler : Handler
    {
        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;
            if (message.Text == "/start")
            {
                Models.User user;

                if (DBManager.UserExists(message, out user))
                    await botClient.SendTextMessageAsync(message.Chat, "Вы уже зарегистрированы!");

                else
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Для регистрации введите название группы"
                        , null, null, null, null, null, null, null, replyMarkup: new ForceReplyMarkup { Selective = true });
                }

            }
            else if (Successor != null)
            {
                Successor.HandleRequestAsync(update, botClient);
            }
                
        }    
    }
}

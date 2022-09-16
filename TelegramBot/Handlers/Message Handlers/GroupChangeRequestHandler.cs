using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot.Handlers.Message_Handlers
{
    internal class GroupChangeRequestHandler : Handler
    {
        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (message.Text == "/сменитьгруппу") 
            {
                await botClient.SendTextMessageAsync(message.Chat, "Введите новое название группы", 
                    null, null, null, null, null, null, null, replyMarkup: new ForceReplyMarkup { Selective = true });
            }

            else if(Successor != null)
                Successor.HandleRequestAsync(update, botClient);
        }
    }
}

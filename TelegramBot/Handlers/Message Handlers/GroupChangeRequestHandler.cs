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
        public GroupChangeRequestHandler(Dictionary<string, State> dialogs) : base(dialogs)
        {
        }

        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            var dialogId = $"{update.Message.Chat.Id}_{update.Message.From.Id}";

            if (message.Text == "/сменитьгруппу") 
            {
                if (DBManager.UserExists(message, out var user))
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Введите новое название группы");
                    dialogs[dialogId] = State.GroupChange;
                }

                else
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Вы не зарегистрированы!");
                    dialogs[dialogId] = State.None;
                }
                    
                
            }

            else if(Successor != null)
                Successor.HandleRequestAsync(update, botClient);
        }
    }
}

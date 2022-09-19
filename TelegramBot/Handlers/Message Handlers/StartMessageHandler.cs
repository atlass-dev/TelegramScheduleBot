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
        public StartMessageHandler(Dictionary<string, State> dialogs) : base(dialogs)
        {
        }

        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            var dialogId = $"{update.Message.Chat.Id}_{update.Message.From.Id}";

            if (message.Text == "/start")
            {
                if (!dialogs.TryGetValue(dialogId, out var dialog))
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Для регистрации введите название группы");

                    dialogs[dialogId] = State.Start;
                }

            }
            else if (Successor != null)
            {
                Successor.HandleRequestAsync(update, botClient);
            }
                
        }    
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Handlers.Message_Handlers
{
    internal class DefaultMessageHandler : Handler
    {
        public DefaultMessageHandler(Dictionary<string, State> dialogs) : base(dialogs)
        {
        }

        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            var dialogId = $"{update.Message.Chat.Id}_{update.Message.From.Id}";

            if (message != null && dialogs[dialogId] == State.None)
            {
                await botClient.SendTextMessageAsync(message.Chat, "Я твоя не понимать!");
                return;
            }
                
            else if (Successor != null)
                Successor.HandleRequestAsync(update, botClient);
        }
    }
}

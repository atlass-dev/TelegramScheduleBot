using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Handlers.Message_Handlers
{
    abstract class Handler
    {
        public Handler Successor { get; set; }
        protected Dictionary<string, State> dialogs;
        public abstract Task HandleRequestAsync(Update update, ITelegramBotClient botClient);

        public Handler(Dictionary<string, State> dialogs)
        {
            this.dialogs = dialogs;
        }
    }
}

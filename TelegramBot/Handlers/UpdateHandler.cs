using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using TelegramBot.Handlers.Message_Handlers;

namespace TelegramBot
{
    internal class UpdateHandler : IUpdateHandler
    {
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonSerializer.Serialize(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                
                Handler startMessageHandler = new StartMessageHandler();
                Handler defaultMessageHandler = new DefaultMessageHandler();
                Handler GroupSetterHandler = new GroupSetterHandler();

                startMessageHandler.Successor = GroupSetterHandler;
                GroupSetterHandler.Successor = defaultMessageHandler;
                startMessageHandler.HandleRequestAsync(update, botClient);
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonSerializer.Serialize(exception));
        }
    }
}

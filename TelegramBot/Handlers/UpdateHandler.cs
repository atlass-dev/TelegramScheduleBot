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
        private Dictionary<string, State> dialogs = new();
        public async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
        {
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                
                Handler startMessageHandler = new StartMessageHandler(dialogs);
                Handler defaultMessageHandler = new DefaultMessageHandler(dialogs);
                Handler showScheduleHandler = new ShowScheduleHandler(dialogs);
                Handler groupChangerRequestHandler = new GroupChangeRequestHandler(dialogs);
                Handler groupChangeHandler = new GroupChangeHandler(dialogs);

                startMessageHandler.Successor = showScheduleHandler;
                showScheduleHandler.Successor = groupChangerRequestHandler;
                groupChangerRequestHandler.Successor = groupChangeHandler;
                groupChangeHandler.Successor = defaultMessageHandler;
                startMessageHandler.HandleRequestAsync(update, botClient);
            }
        }

        public async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            Console.WriteLine(JsonSerializer.Serialize(exception));
        }
    }

    public enum State
    {
        Start,
        Registered,
        GroupChange,
        TomorrowSchedule,
        DateSchedule
    }
}

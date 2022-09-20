using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Handlers.Message_Handlers
{
    internal class GroupChangerHandler : Handler
    {
        public GroupChangerHandler(Dictionary<string, State> dialogs) : base(dialogs)
        {
        }

        public async override Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            var dialogId = $"{update.Message.Chat.Id}_{update.Message.From.Id}";

            if (dialogs.ContainsKey(dialogId) && dialogs[dialogId] == State.Start)
            {
                DBManager.AddUser(message);
                await botClient.SendTextMessageAsync(message.Chat, "Вы успешно зарегистрировались!");
            }

             if (dialogs.ContainsKey(dialogId) && dialogs[dialogId] == State.GroupChange)
             {
                 try
                 {
                     DBManager.ChangeGroup(message);
                     await botClient.SendTextMessageAsync(message.Chat, "Вы успешно изменили учебную группу!");
                 }
                 catch (ArgumentException ex)
                 {
                     await botClient.SendTextMessageAsync(message.Chat, "Такой группы не существует");
                 }
             }

            else if (Successor != null)
                Successor.HandleRequestAsync(update, botClient);
        }
    }
}

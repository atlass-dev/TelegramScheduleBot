using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot.Handlers.Message_Handlers
{
    internal class GroupChangeHandler : Handler
    {
        public GroupChangeHandler(Dictionary<string, State> dialogs) : base(dialogs)
        {
        }

        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            var dialogId = $"{update.Message.Chat.Id}_{update.Message.From.Id}";

            if (dialogs[dialogId] == State.Start)
            {
                DBManager.AddUser(message);
                await botClient.SendTextMessageAsync(message.Chat, "Вы успешно зарегистрировались!");
            }

            if (dialogs[dialogId] == State.GroupChange)
            {
                try
                {
                    DBManager.ChangeGroup(message);
                    await botClient.SendTextMessageAsync(message.Chat, "Вы успешно изменили учебную группу!");
                }
                catch(ArgumentException ex)
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Такой группы не существует");
                }
            }
        }
    }
}

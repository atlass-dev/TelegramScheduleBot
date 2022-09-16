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
        public override async Task HandleRequestAsync(Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;

            if (message.ReplyToMessage != null && message.ReplyToMessage.Text == "Для регистрации введите название группы")
            {
                try
                {
                    DBManager.AddUser(message);
                    await botClient.SendTextMessageAsync(message.Chat, "Вы успешно зарегистрировались!");
                }
                catch (Exception ex)
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Вы уже зарегистрированы!");
                }
            }

            if (message.ReplyToMessage != null && message.ReplyToMessage.Text == "Введите новое название группы")
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
                catch(Exception ex)
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Вы не зарегистрированы!");
                }
            }
        }
    }
}

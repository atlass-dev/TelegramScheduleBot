using ScheduleParsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramBot
{
    internal static class SchedulePrinter
    {
        public static async void ShowSchedule(string group,string startDate,string endDate,Update update, ITelegramBotClient botClient)
        {
            var message = update.Message;
            Weekday[] schedule = await ScheduleParser.GetSchedule(group, startDate, endDate);
            string response = "";

            foreach (Weekday item in schedule)
            {
                response += new string('-', 30) + "\n" + $"Дата: {item.date}, День недели: {item.weekDay}"
                    + "\n" + "Список занятий: \n\n";

                if (item.pairs.Count == 0)
                {
                    await botClient.SendTextMessageAsync(message.Chat, "В этот день нет занятий \n\n");
                    continue;
                }

                foreach (var pair in item.pairs)
                {
                    if (pair.schedulePairs.Count == 0)
                    {
                        continue;
                    }
                        
                    response += $"Номер занятия: {pair.N}, время: {pair.time} \n\n";
                    foreach (var schedulePair in pair.schedulePairs)
                        response += $"{schedulePair.subject}  -  {schedulePair.teacher}  -  {schedulePair.aud} \n\n";
                }
            }

            await botClient.SendTextMessageAsync(message.Chat, response);
        }
    }
}

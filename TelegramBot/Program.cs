using System;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using System.Text.Json;

namespace TelegramBot
{
    internal class Program
    {
        static ITelegramBotClient bot = new TelegramBotClient("5711601462:AAGX9SEJmAkdeBrqPXJsZ-MPOdFlsIzoDKQ");
        public static void Main(string[] args)
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var updateHandler = new UpdateHandler();

            var cts = new CancellationTokenSource();
            var cancellationToken = cts.Token;
            var receiverOptions = new ReceiverOptions
            {
                AllowedUpdates = { }, // receive all update types
            };
            bot.StartReceiving(
                updateHandler.HandleUpdateAsync,
                updateHandler.HandleErrorAsync,
                receiverOptions,
                cancellationToken
            );
            Console.ReadLine();
        }
    } 
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot
{
    internal static class DBChecker
    {
        public static bool UserExists(Message message, out Models.User user)
        {
            using (UserContext userContext = new UserContext())
            {
                user = userContext.Users
                                  .Where(u => u.Username == message.From.Username)
                                  .FirstOrDefault();
            }

            return user != null;
        }
    }
}

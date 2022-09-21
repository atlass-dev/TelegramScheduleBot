using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types;

namespace TelegramBot
{
    internal static class DBManager
    {
        private static List<string> groups = new List<string>()
        {
            "ИВТ-20-1",
            "ИВТ-20-2",
            "ПИЭ-20-1",
            "ПИЭ-20-2",
            "ПИЭ-20-3",
            "АИС-20-1",
        };
        public static void AddUser(Message message)
        {
            Models.User user;

            if (!UserExists(message, out user))
            {
                user = new Models.User() { Username = message.From.Username, Group = message.Text};

                using (UserContext userContext = new UserContext())
                {
                    userContext.Add(user);
                    userContext.SaveChanges();
                }
            }

            else
            {
                throw new Exception("User already exists");
            }
        }

        public static void ChangeGroup(Message message)
        {
            using(UserContext userContext = new UserContext())
            {
                Models.User user;

                if (UserExists(message, out user))
                {
                    if (GroupExists(message.Text))
                    {
                        user.Group = message.Text;
                        userContext.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                        userContext.SaveChanges();
                    }

                    else
                        throw new ArgumentException("Group doesn`t exist");
                    
                }

                else
                    throw new Exception("User`s not registered");
            }
        }

        private static bool GroupExists(string text) => groups.Contains(text);

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

        public static Models.User GetUser(Message message)
        {
            if (message != null && UserExists(message, out var user))
                return user;
            return null;
        }
    }
}

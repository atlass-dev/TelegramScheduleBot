using System.Net;
using System.Text;
using System.Text.Json;

namespace ScheduleParsing
{
    public static class ScheduleParser
    {
        public static async Task<Weekday[]> GetSchedule(string group, string start, string end)
        {
            string serverResponse = await GetServerResponse(group, start, end);

            Weekday[] schedule = DeserializeSchedule(serverResponse);

            return schedule;
        }

        private static async Task<string> GetServerResponse(string group, string start, string end)
        {
            var client = new HttpClient();

            var responseMessage = await client.GetAsync(new Uri($"https://www.usue.ru/schedule/?t=0.4910114439173696&action=show&startDate={start}&endDate={end}&group={group}"));

            var response = await responseMessage.Content.ReadAsStringAsync();

            return response;
        }

        private static Weekday[] DeserializeSchedule(string serverResponse)
        {
            return JsonSerializer.Deserialize<Weekday[]>(serverResponse);
        }
    }
}
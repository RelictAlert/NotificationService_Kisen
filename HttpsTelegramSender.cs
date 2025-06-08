using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using NotificationService_Kisen.Senders;

namespace NotificationService_Kisen
{
    public class HttpsTelegramSender : ITelegramSender
    {
        private readonly HttpClient _https;

        public HttpsTelegramSender(HttpClient https)
        {
            _https = https;
        }
        public async Task SendAsync(string chatId, int alertId, string text)
        {
            var payload = new
            {
                ChatId = long.Parse(chatId),
                AlertId = alertId,
                Text = text
            };

            var response = await _https.PostAsJsonAsync("/api/bot/send", payload);
            response.EnsureSuccessStatusCode();
        }
    }
}

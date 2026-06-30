using System;
using System.Configuration;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace DataIntegration.Services
{
    public static class WhatsAppSender
    {
        private static readonly HttpClient _httpClient = new HttpClient();

        public static async Task SendErrorNotificationAsync(string errorMessage)
        {
            try
            {
                // Read configuration
                string enabledStr = ConfigurationManager.AppSettings["WhatsAppEnabled"];
                if (string.IsNullOrWhiteSpace(enabledStr) || !bool.TryParse(enabledStr, out bool enabled) || !enabled)
                {
                    return; // WhatsApp notifications are disabled
                }

                string storeName = ConfigurationManager.AppSettings["StoreName"] ?? "Unknown Store";
                string apiUrl = ConfigurationManager.AppSettings["WhatsAppApiUrl"];
                string token = ConfigurationManager.AppSettings["WhatsAppToken"];
                string toPhone = ConfigurationManager.AppSettings["WhatsAppToPhone"];

                if (string.IsNullOrWhiteSpace(apiUrl) || string.IsNullOrWhiteSpace(toPhone))
                {
                    Log.Warning("WhatsApp notification is enabled but WhatsAppApiUrl or WhatsAppToPhone is not configured.");
                    return;
                }

                // Format the message with WhatsApp markdown formatting
                string formattedMessage = $"⚠️ *Pixel Integration Error Alert* ⚠️\n" +
                                           $"*Store:* {storeName}\n" +
                                           $"*Time:* {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n\n" +
                                           $"*Details:*\n{errorMessage}";

                Log.Information("Sending WhatsApp notification to {Phone}...", toPhone);

                // Send using the configured API
                // We support both GET (if URL contains placeholders) and POST (standard JSON/FormUrlEncoded)
                if (apiUrl.Contains("{text}") || apiUrl.Contains("{message}"))
                {
                    // GET Request Fallback (e.g. CallMeBot style)
                    string requestUrl = apiUrl
                        .Replace("{phone}", Uri.EscapeDataString(toPhone))
                        .Replace("{apikey}", Uri.EscapeDataString(token ?? ""))
                        .Replace("{token}", Uri.EscapeDataString(token ?? ""))
                        .Replace("{text}", Uri.EscapeDataString(formattedMessage))
                        .Replace("{message}", Uri.EscapeDataString(formattedMessage));

                    var response = await _httpClient.GetAsync(requestUrl).ConfigureAwait(false);
                    if (response.IsSuccessStatusCode)
                    {
                        Log.Information("WhatsApp notification sent successfully via GET.");
                    }
                    else
                    {
                        Log.Error("Failed to send WhatsApp notification. Status: {StatusCode}", response.StatusCode);
                    }
                }
                else
                {
                    // POST Request (e.g. UltraMsg / GreenAPI / Custom Webhook style)
                    // We send as application/x-www-form-urlencoded which is compatible with UltraMsg out-of-the-box
                    var values = new System.Collections.Generic.Dictionary<string, string>
                    {
                        { "token", token ?? "" },
                        { "to", toPhone },
                        { "body", formattedMessage },
                        { "phone", toPhone },       // Some APIs use 'phone' instead of 'to'
                        { "message", formattedMessage } // Some APIs use 'message' instead of 'body'
                    };

                    var content = new FormUrlEncodedContent(values);
                    var response = await _httpClient.PostAsync(apiUrl, content).ConfigureAwait(false);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        Log.Information("WhatsApp notification sent successfully via POST.");
                    }
                    else
                    {
                        string responseBody = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                        Log.Error("Failed to send WhatsApp notification. Status: {StatusCode}, Response: {Response}", response.StatusCode, responseBody);
                    }
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, "Exception occurred while sending WhatsApp notification.");
            }
        }
    }
}

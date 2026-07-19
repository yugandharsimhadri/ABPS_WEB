using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using ABPS_WEB.Domain.Notifications;
using Microsoft.Extensions.Configuration;

namespace ABPS_WEB.Infrastructure.Notifications;

/// <summary>Ported from ABPS_WPF's Helper/WhatsAppHelper.cs. Registered as a typed client
/// (services.AddHttpClient&lt;IWhatsAppClient, WhatsAppClient&gt;()) so HttpClient pooling is
/// handled by the framework instead of a hand-rolled static field.</summary>
public class WhatsAppClient : IWhatsAppClient
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public WhatsAppClient(HttpClient httpClient, IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;
    }

    private async Task<string> PostAsync(object payload)
    {
        var phoneNumberId = _config["WhatsApp:FacebookPhoneNumberId"];
        var accessToken = _config["WhatsApp:FacebookAccessToken"];
        string url = $"https://graph.facebook.com/v25.0/{phoneNumberId}/messages";

        string jsonPayload = JsonSerializer.Serialize(payload);
        using var request = new HttpRequestMessage(HttpMethod.Post, url)
        {
            Content = new StringContent(jsonPayload, Encoding.UTF8, "application/json")
        };
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

        using HttpResponseMessage response = await _httpClient.SendAsync(request);
        return await response.Content.ReadAsStringAsync();
    }

    public Task<string> SendTemplateMessageAsync(string toPhoneNumber, string templateName, string languageCode = "en_US") =>
        PostAsync(new
        {
            messaging_product = "whatsapp",
            to = toPhoneNumber,
            type = "template",
            template = new { name = templateName, language = new { code = languageCode } }
        });

    public Task<string> SendTextMessageAsync(string toPhoneNumber, string messageText) =>
        PostAsync(new
        {
            messaging_product = "whatsapp",
            to = toPhoneNumber,
            type = "text",
            text = new { body = messageText }
        });
}

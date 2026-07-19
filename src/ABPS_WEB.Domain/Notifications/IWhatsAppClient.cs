namespace ABPS_WEB.Domain.Notifications;

/// <summary>Ported from ABPS_WPF's Helper/WhatsAppHelper.cs — thin wrapper over Meta's
/// WhatsApp Graph API. The shared-HttpClient concern that comment documents is solved here via
/// IHttpClientFactory's typed-client pooling instead of a static field — same fix, ASP.NET
/// Core-native mechanism.</summary>
public interface IWhatsAppClient
{
    Task<string> SendTemplateMessageAsync(string toPhoneNumber, string templateName, string languageCode = "en_US");
    Task<string> SendTextMessageAsync(string toPhoneNumber, string messageText);
}

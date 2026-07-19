using System.Text.Json;
using ABPS_WEB.Domain.Notifications;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ABPS_WEB.Infrastructure.Notifications;

/// <summary>Ported from ABPS_WPF's Helper/WhatsAppNotifier.cs, in full — same flag gate
/// (WhatsApp:NotificationsEnabled), same "hello_world" template + text-message pattern, same
/// attempt/success/failure logging, same fail-closed behavior on missing config. ILogger<T>
/// replaces LogHelper/NLog — see the note in RoomService.cs on that swap.</summary>
public class WhatsAppNotifier : IWhatsAppNotifier
{
    private readonly IWhatsAppClient _client;
    private readonly IConfiguration _config;
    private readonly ILogger<WhatsAppNotifier> _logger;

    public WhatsAppNotifier(IWhatsAppClient client, IConfiguration config, ILogger<WhatsAppNotifier> logger)
    {
        _client = client;
        _config = config;
        _logger = logger;
    }

    public async Task NotifyAsync(string? phoneNo, string message)
    {
        if (!bool.TryParse(_config["WhatsApp:NotificationsEnabled"], out var enabled) || !enabled)
        {
            _logger.LogInformation("WhatsApp send skipped — WhatsApp:NotificationsEnabled is not true.");
            return;
        }

        if (string.IsNullOrWhiteSpace(phoneNo))
        {
            _logger.LogWarning("WhatsApp send skipped — no phone number on record.");
            return;
        }

        var toNumber = "91" + phoneNo.Trim();
        _logger.LogInformation("WhatsApp send attempted to {ToNumber}.", toNumber);

        try
        {
            var accessToken = _config["WhatsApp:FacebookAccessToken"];
            var phoneNumberId = _config["WhatsApp:FacebookPhoneNumberId"];
            if (string.IsNullOrWhiteSpace(accessToken) || string.IsNullOrWhiteSpace(phoneNumberId))
            {
                _logger.LogWarning("WhatsApp send failed — not configured (missing WhatsApp:FacebookAccessToken/FacebookPhoneNumberId).");
                return;
            }

            var templateResp = await _client.SendTemplateMessageAsync(toNumber, "hello_world");
            var textResp = await _client.SendTextMessageAsync(toNumber, message);

            if (IsApiError(templateResp) || IsApiError(textResp))
                _logger.LogWarning("WhatsApp send failed (API error) to {ToNumber}: {Response}", toNumber, Truncate(textResp));
            else
                _logger.LogInformation("WhatsApp send succeeded to {ToNumber}.", toNumber);
        }
        catch (Exception ex)
        {
            // Best-effort notification only — swallow so a WhatsApp/network failure never
            // surfaces to the caller or unwinds into the room-op's success path.
            _logger.LogError(ex, "WhatsApp send failed (exception) to {ToNumber}", toNumber);
        }
    }

    private static bool IsApiError(string? response)
    {
        if (string.IsNullOrWhiteSpace(response)) return true;
        try
        {
            using var doc = JsonDocument.Parse(response);
            return doc.RootElement.TryGetProperty("error", out _);
        }
        catch
        {
            return true;
        }
    }

    private static string Truncate(string? s) => string.IsNullOrEmpty(s) ? "" : s.Length > 500 ? s.Substring(0, 500) : s;
}

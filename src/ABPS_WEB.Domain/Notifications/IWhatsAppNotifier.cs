namespace ABPS_WEB.Domain.Notifications;

/// <summary>Ported from ABPS_WPF's Helper/WhatsAppNotifier.cs — best-effort send for
/// room-operation and donation-greeting messages. Never throws; a WhatsApp/network failure
/// must never affect an operation that has already been committed to the database. Call as a
/// discarded, unawaited Task (e.g. `_ = _whatsApp.NotifyAsync(phone, message)`), same
/// fire-and-forget contract as the WPF version.</summary>
public interface IWhatsAppNotifier
{
    Task NotifyAsync(string? phoneNo, string message);
}

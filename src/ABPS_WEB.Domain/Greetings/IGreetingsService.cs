namespace ABPS_WEB.Domain.Greetings;

/// <summary>Ported from ABPS_WPF's SendSmsWindow.xaml.cs (screen code-behind, no dedicated
/// controller in the source app — the donor-match query and send loop lived directly in the
/// window). WhatsApp send + tbl_SmsTransaction persistence per recipient is done here
/// synchronously (unlike IWhatsAppNotifier's fire-and-forget rooms notifications) because
/// Send Greetings is itself the user-initiated action being performed, not a side-effect of
/// some other operation — the user needs the real sent/attempted count back.</summary>
public interface IGreetingsService
{
    Task<List<GreetingRecipient>> FindByEnglishDateAsync(DateTime date);
    Task<List<GreetingRecipient>> FindByTeluguCalendarAsync(int tMasamId, int tPakshamId, int tThiduluId);
    Task<int> GetSentTodayCountAsync();
    Task<SendGreetingsResult> SendGreetingsAsync(SendGreetingsRequest request);
}

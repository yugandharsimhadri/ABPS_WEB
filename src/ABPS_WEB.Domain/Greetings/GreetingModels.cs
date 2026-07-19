namespace ABPS_WEB.Domain.Greetings;

public record GreetingRecipient(string? Name, string? PhoneNo, string? BondNo, string? IntheNameof, string? OcassionName, int OcassionId);

public record SendGreetingsRecipient(string? Name, string? PhoneNo, string? BondNo, string? IntheNameof, string? OcassionName, int OcassionId);

public record SendGreetingsRequest(
    string SmsSendType, DateTime SmsDate, int TMasamId, int TPakshamId, int TThiduluId,
    List<SendGreetingsRecipient> Recipients);

public record SendGreetingsResult(int Sent, int Attempted);

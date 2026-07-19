namespace ABPS_WEB.Domain.Reports;

public record UserOption(int UserId, string? UserName);

/// <summary>Ported from ABPS_WPF's Controllers/ReportController.cs. Two methods were
/// confirmed dead code (no call site anywhere in ABPS_WPF) and are NOT ported:
/// GetBookingReport_old (both overloads — superseded by GetBookingReport) and sendSMS (a
/// literal no-op stub returning "Success"). Everything else is a 1:1 port, including both
/// overloads each of getTransactionDaySummary/getTransactionMonthSummary — both really are
/// called, from TransactionSummaryReportWindow, selected by whether a group-by dimension is
/// chosen.</summary>
public interface IReportService
{
    Task<List<DonationReportVM>> GetDonationReportAsync(DateTime fromDate, DateTime toDate);
    Task<List<AdvanceBookingReportVM>> GetAdvanceBookingReportAsync(DateTime fromDate, DateTime toDate, int statusId = 0);
    Task<List<BookingReportVM>> GetBookingReportAsync(DateTime fromDate, DateTime toDate, int statusId);
    Task<ReceiptData?> GetPrecautionReceiptAsync(int bookingId);
    Task<List<RoomReportVM>> GetRoomReportAsync(int rtId, int floorId, int statusId, int beds);
    Task<string> StoreSmsTransactionAsync(int donarId, string bondNo, int occasionId, string smsSendType,
        string? phoneNo, string content, DateTime smsDate, int tMasamId, int tPaksham, int tThidulu, string? createdBy);
    Task<List<TransactionReportVM>> GetTransactionReportAsync(DateTime fromDate, DateTime toDate, int categoryId, int typeId);
    Task<List<TransactionSummaryDayWise>> GetTransactionDaySummaryAsync(DateTime fromDate, DateTime toDate, int categoryId, int typeId);
    Task<List<TransactionSummaryDayCategory>> GetTransactionDaySummaryByGroupAsync(DateTime fromDate, DateTime toDate, int categoryId, int typeId, string groupBy);
    Task<List<TransactionSummaryMonthWise>> GetTransactionMonthSummaryAsync(DateTime fromDate, DateTime toDate, int categoryId, int typeId);
    Task<List<TransactionSummaryMonthCategory>> GetTransactionMonthSummaryByGroupAsync(DateTime fromDate, DateTime toDate, int categoryId, int typeId, string? groupBy);
    Task<List<ExpiringBookingReportVM>> GetExpiringBookingReportAsync();
    Task<List<UserOption>> GetAllUsersAsync();
    Task<List<UserTransactionDetails>> GetTransactionReportByUserAsync(DateTime fromDate, DateTime toDate, string userName, int selectedIndex, int paymentTypeId);
    Task<List<UserTransactionSummaryVM>> GetTransactionUserSummaryReportAsync(DateTime fromDate, DateTime toDate, string userName, int selectedIndex, int paymentTypeId);
    Task<int> GetBookingStatusIdAsync(int bookingId);
    Task<List<OccupancyReportVM>> GetOccupancyReportAsync();
    Task<List<AuditTransactionReport>> GetAuditTransactionReportAsync(DateTime fromDate, DateTime toDate);
}

using ABPS_WEB.Domain.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABPS_WEB.Api.Controllers;

/// <summary>Ported from ABPS_WPF's ReportController — one endpoint per report screen.</summary>
[ApiController]
[Authorize]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _reports;

    public ReportsController(IReportService reports) => _reports = reports;

    [HttpGet("donations")]
    public async Task<ActionResult<List<DonationReportVM>>> GetDonationReport([FromQuery] DateTime from, [FromQuery] DateTime to) =>
        Ok(await _reports.GetDonationReportAsync(from, to));

    [HttpGet("advance-bookings")]
    public async Task<ActionResult<List<AdvanceBookingReportVM>>> GetAdvanceBookingReport([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int statusId = 0) =>
        Ok(await _reports.GetAdvanceBookingReportAsync(from, to, statusId));

    [HttpGet("bookings")]
    public async Task<ActionResult<List<BookingReportVM>>> GetBookingReport([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int statusId) =>
        Ok(await _reports.GetBookingReportAsync(from, to, statusId));

    [HttpGet("bookings/{bookingId}/precaution-receipt")]
    public async Task<ActionResult<ReceiptData>> GetPrecautionReceipt(int bookingId)
    {
        var data = await _reports.GetPrecautionReceiptAsync(bookingId);
        return data == null ? NotFound() : Ok(data);
    }

    [HttpGet("rooms")]
    public async Task<ActionResult<List<RoomReportVM>>> GetRoomReport([FromQuery] int rtId, [FromQuery] int floorId, [FromQuery] int statusId, [FromQuery] int beds) =>
        Ok(await _reports.GetRoomReportAsync(rtId, floorId, statusId, beds));

    [HttpGet("transactions")]
    public async Task<ActionResult<List<TransactionReportVM>>> GetTransactionReport([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int categoryId, [FromQuery] int typeId) =>
        Ok(await _reports.GetTransactionReportAsync(from, to, categoryId, typeId));

    [HttpGet("transactions/summary/day")]
    public async Task<ActionResult<List<TransactionSummaryDayWise>>> GetTransactionDaySummary([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int categoryId, [FromQuery] int typeId) =>
        Ok(await _reports.GetTransactionDaySummaryAsync(from, to, categoryId, typeId));

    [HttpGet("transactions/summary/day-by-group")]
    public async Task<ActionResult<List<TransactionSummaryDayCategory>>> GetTransactionDaySummaryByGroup([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int categoryId, [FromQuery] int typeId, [FromQuery] string groupBy) =>
        Ok(await _reports.GetTransactionDaySummaryByGroupAsync(from, to, categoryId, typeId, groupBy));

    [HttpGet("transactions/summary/month")]
    public async Task<ActionResult<List<TransactionSummaryMonthWise>>> GetTransactionMonthSummary([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int categoryId, [FromQuery] int typeId) =>
        Ok(await _reports.GetTransactionMonthSummaryAsync(from, to, categoryId, typeId));

    [HttpGet("transactions/summary/month-by-group")]
    public async Task<ActionResult<List<TransactionSummaryMonthCategory>>> GetTransactionMonthSummaryByGroup([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] int categoryId, [FromQuery] int typeId, [FromQuery] string? groupBy) =>
        Ok(await _reports.GetTransactionMonthSummaryByGroupAsync(from, to, categoryId, typeId, groupBy));

    [HttpGet("expiring-bookings")]
    public async Task<ActionResult<List<ExpiringBookingReportVM>>> GetExpiringBookingReport() =>
        Ok(await _reports.GetExpiringBookingReportAsync());

    [HttpGet("users")]
    public async Task<ActionResult<List<UserOption>>> GetAllUsers() => Ok(await _reports.GetAllUsersAsync());

    [HttpGet("transactions/by-user")]
    public async Task<ActionResult<List<UserTransactionDetails>>> GetTransactionReportByUser([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] string userName, [FromQuery] int selectedIndex, [FromQuery] int paymentTypeId) =>
        Ok(await _reports.GetTransactionReportByUserAsync(from, to, userName, selectedIndex, paymentTypeId));

    [HttpGet("transactions/user-summary")]
    public async Task<ActionResult<List<UserTransactionSummaryVM>>> GetTransactionUserSummaryReport([FromQuery] DateTime from, [FromQuery] DateTime to, [FromQuery] string userName, [FromQuery] int selectedIndex, [FromQuery] int paymentTypeId) =>
        Ok(await _reports.GetTransactionUserSummaryReportAsync(from, to, userName, selectedIndex, paymentTypeId));

    [HttpGet("bookings/{bookingId}/status-id")]
    public async Task<ActionResult<int>> GetBookingStatusId(int bookingId) => Ok(await _reports.GetBookingStatusIdAsync(bookingId));

    [HttpGet("occupancy")]
    public async Task<ActionResult<List<OccupancyReportVM>>> GetOccupancyReport() => Ok(await _reports.GetOccupancyReportAsync());

    [HttpGet("audit")]
    public async Task<ActionResult<List<AuditTransactionReport>>> GetAuditTransactionReport([FromQuery] DateTime from, [FromQuery] DateTime to) =>
        Ok(await _reports.GetAuditTransactionReportAsync(from, to));
}

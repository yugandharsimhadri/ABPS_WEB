using ABPS_WEB.Domain.AdvanceBooking;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABPS_WEB.Api.Controllers;

/// <summary>Ported from ABPS_WPF's AdvanceBookingController — New/Manage Advance Booking screens.</summary>
[ApiController]
[Authorize]
[Route("api/advance-bookings")]
public class AdvanceBookingsController : ControllerBase
{
    private readonly IAdvanceBookingService _advanceBookings;

    public AdvanceBookingsController(IAdvanceBookingService advanceBookings) => _advanceBookings = advanceBookings;

    [HttpPost]
    public async Task<IActionResult> Create(AdvanceBookingRequest request)
    {
        var result = await _advanceBookings.CreateAsync(request);
        if (!result.Success) return BadRequest(new { message = result.Message });
        return Ok(new { advanceBookingId = result.AdvanceBookingId, message = result.Message });
    }

    [HttpGet]
    public async Task<ActionResult<List<AdvanceBookingDto>>> Search(
        [FromQuery] string? phone, [FromQuery] string? name, [FromQuery] DateTime? date,
        [FromQuery] int? statusId, [FromQuery] string? aadhaar) =>
        Ok(await _advanceBookings.SearchAsync(phone, name, date, statusId, aadhaar));

    [HttpGet("due")]
    public async Task<ActionResult<List<AdvanceBookingDto>>> GetDue([FromQuery] DateTime asOf) =>
        Ok(await _advanceBookings.GetDueAsync(asOf));

    [HttpPost("{advanceBookingId}/assign/{resultingBookingId}")]
    public async Task<IActionResult> Assign(int advanceBookingId, int resultingBookingId) =>
        FromResult(await _advanceBookings.AssignAsync(advanceBookingId, resultingBookingId));

    [HttpPost("{advanceBookingId}/cancel")]
    public async Task<IActionResult> Cancel(int advanceBookingId, [FromBody] string reason) =>
        FromResult(await _advanceBookings.CancelAsync(advanceBookingId, reason));

    [HttpPost("expire-overdue")]
    public async Task<ActionResult<int>> ExpireOverdue() => Ok(await _advanceBookings.ExpireOverdueAsync());

    private IActionResult FromResult(AdvanceBookingOperationResult result) =>
        result.Success ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
}

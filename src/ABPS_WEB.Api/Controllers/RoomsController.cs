using ABPS_WEB.Domain.RoomOps;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABPS_WEB.Api.Controllers;

/// <summary>Ported from ABPS_WPF's RoomController, in full — Book/Vacate/Extend/Shift/
/// Cancel/PartialCheckout/AddOn, matching each WPF Room Ops screen 1:1.</summary>
[ApiController]
[Authorize]
[Route("api/rooms")]
public class RoomsController : ControllerBase
{
    private readonly IRoomService _rooms;

    public RoomsController(IRoomService rooms) => _rooms = rooms;

    [HttpGet("available")]
    public async Task<ActionResult<List<RoomVM>>> GetAvailableRooms() => Ok(await _rooms.GetAllRoomsAsync());

    [HttpGet("available/dorm-locker")]
    public async Task<ActionResult<List<RoomVM>>> GetAvailableDormLockerRooms() => Ok(await _rooms.GetAllDormLockerRoomsAsync());

    [HttpGet("counts")]
    public async Task<ActionResult<RoomCounts>> GetRoomCounts() => Ok(await _rooms.GetRoomCountsAsync());

    [HttpGet("counts/dorm-locker")]
    public async Task<ActionResult<RoomCounts>> GetDormLockerRoomCounts() => Ok(await _rooms.GetDormLockerRoomCountsAsync());

    [HttpGet("donors/by-bond-no/{bondNo}")]
    public async Task<ActionResult<DonarBondVM>> GetDonarByBondNo(string bondNo)
    {
        var result = await _rooms.GetDonarByBondNoAsync(bondNo);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("guests/by-aadhaar/{aadhaar}")]
    public async Task<ActionResult<CustomerLookupResult>> GetCustomerByAadhar(string aadhaar)
    {
        var result = await _rooms.GetCustomerByAadharAsync(aadhaar);
        return result == null ? NotFound() : Ok(result);
    }

    [HttpGet("categories")]
    public async Task<ActionResult<List<BookingCategory>>> GetCategories() => Ok(await _rooms.GetCategoriesAsync());

    [HttpGet("by-room-no/{roomNo}")]
    public async Task<ActionResult<int>> GetBookingIdByRoomNo(string roomNo) => Ok(await _rooms.GetBookingIdByRoomNoAsync(roomNo));

    [HttpGet("bookings/{bookingId}/is-active")]
    public async Task<ActionResult<bool>> ValidateActiveBooking(int bookingId) => Ok(await _rooms.ValidateActiveBookingAsync(bookingId));

    [HttpGet("bookings/{bookingId}")]
    public async Task<ActionResult<BookingSummary>> GetBookingDetails(int bookingId)
    {
        var summary = await _rooms.GetBookingDetailsAsync(bookingId);
        return summary == null ? NotFound() : Ok(summary);
    }

    [HttpGet("bookings/{bookingId}/rooms")]
    public async Task<ActionResult<List<BookingRoomLine>>> GetBookingRooms(int bookingId) => Ok(await _rooms.GetBookingRoomsAsync(bookingId));

    [HttpPost("master")]
    public async Task<IActionResult> SaveRoomDetails(SaveRoomDetailsRequest request) => FromResult(await _rooms.SaveRoomDetailsAsync(request));

    [HttpPut("master/{roomId}")]
    public async Task<IActionResult> ModifyRoomDetails(int roomId, ModifyRoomDetailsRequest request) =>
        FromResult(await _rooms.ModifyRoomDetailsAsync(request with { RoomId = roomId }));

    [HttpPost("bookings")]
    public async Task<IActionResult> Book(BookingRequest request)
    {
        var result = await _rooms.SaveBookingAsync(request);
        if (!result.Success) return BadRequest(new { message = result.Message });
        return Ok(new { bookingId = result.BookingId, message = result.Message });
    }

    [HttpPost("bookings/{bookingId}/vacate")]
    public async Task<IActionResult> Vacate(int bookingId, VacateRoomRequest request) => FromResult(await _rooms.VacateRoomAsync(request with { BookingId = bookingId }));

    [HttpPost("bookings/{bookingId}/extend")]
    public async Task<IActionResult> Extend(int bookingId, ExtendRoomRequest request) => FromResult(await _rooms.ExtendRoomAsync(request with { BookingId = bookingId }));

    [HttpPost("bookings/{bookingId}/shift")]
    public async Task<IActionResult> Shift(int bookingId, ShiftRoomRequest request) => FromResult(await _rooms.ShiftRoomAsync(request with { BookingId = bookingId }));

    [HttpPost("bookings/{bookingId}/cancel")]
    public async Task<IActionResult> Cancel(int bookingId, CancelRoomRequest request) => FromResult(await _rooms.CancelRoomAsync(request with { BookingId = bookingId }));

    [HttpPost("bookings/{bookingId}/cancel-complete")]
    public async Task<IActionResult> CancelComplete(int bookingId, CancelCompleteRoomRequest request) => FromResult(await _rooms.CancelCompleteRoomAsync(request with { BookingId = bookingId }));

    [HttpPost("bookings/{bookingId}/partial-checkout")]
    public async Task<IActionResult> PartialCheckout(int bookingId, PartialCheckoutRequest request) => FromResult(await _rooms.PartialCheckoutAsync(request with { BookingId = bookingId }));

    [HttpPost("bookings/{bookingId}/add-room")]
    public async Task<IActionResult> AddRoom(int bookingId, AddAdditionalRoomRequest request) => FromResult(await _rooms.AddAdditionalRoomAsync(request with { BookingId = bookingId }));

    [HttpPost("bookings/{bookingId}/documents")]
    [RequestSizeLimit(20_000_000)]
    public async Task<IActionResult> UploadDocument(int bookingId, IFormFile file, [FromForm] string documentType)
    {
        using var ms = new MemoryStream();
        await file.CopyToAsync(ms);
        await _rooms.SaveBookingDocumentAsync(bookingId, documentType, file.FileName, file.ContentType, ms.ToArray());
        return Ok();
    }

    private IActionResult FromResult(OperationResult result) =>
        result.Success ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
}

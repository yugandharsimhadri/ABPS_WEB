namespace ABPS_WEB.Domain.AdvanceBooking;

// Ported verbatim from ABPS_WPF/Enums/AdvanceBookingStatus.cs.
public enum AdvanceBookingStatus
{
    Pending = 1,
    Assigned = 2,
    Cancelled = 3,
    Expired = 4,
    NoShow = 5
}

/// <summary>Ported from ABPS_WPF/Models/ViewModel/AdvanceBookingRequest.cs.</summary>
public record AdvanceBookingRequest(
    string GuestName, string PhoneNo, string? AadharNo, string? Address,
    int NoPeople, int NoRoomsRequested, int? RoomCategoryRtId, int? CatId, string? BondNo,
    DateTime RequestedCheckInDate, int RequestedNights, decimal? AdvanceDepositAmount,
    int? PaymentTypeId, int? PaymentSubTypeId, string? ReferenceNumber, string? Notes);

public record AdvanceBookingDto(
    int AdvanceBookingId, string GuestName, string PhoneNo, string? AadharNo, string? Address,
    int NoPeople, int NoRoomsRequested, int? RoomCategoryRtId, int? CatId, string? BondNo,
    DateTime RequestedCheckInDate, int RequestedNights, decimal? AdvanceDepositAmount,
    int? PaymentTypeId, int? PaymentSubTypeId, string? ReferenceNumber, string? Notes,
    int StatusId, int? AssignedBookingId, string? CancelledReason);

public record CreateAdvanceBookingResult(bool Success, string Message, int AdvanceBookingId);
public record AdvanceBookingOperationResult(bool Success, string Message);

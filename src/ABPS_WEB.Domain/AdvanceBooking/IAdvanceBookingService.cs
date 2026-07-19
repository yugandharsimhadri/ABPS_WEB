namespace ABPS_WEB.Domain.AdvanceBooking;

/// <summary>Ported from ABPS_WPF's Controllers/AdvanceBookingController.cs, in full.</summary>
public interface IAdvanceBookingService
{
    Task<CreateAdvanceBookingResult> CreateAsync(AdvanceBookingRequest request);
    Task<List<AdvanceBookingDto>> SearchAsync(string? phone, string? name, DateTime? date, int? statusId, string? aadhaar = null);
    Task<List<AdvanceBookingDto>> GetDueAsync(DateTime asOf);
    Task<AdvanceBookingOperationResult> AssignAsync(int advanceBookingId, int resultingBookingId);
    Task<AdvanceBookingOperationResult> CancelAsync(int advanceBookingId, string reason);
    Task<int> ExpireOverdueAsync();
}

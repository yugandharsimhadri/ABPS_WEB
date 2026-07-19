using ABPS_WEB.Domain.Auth;
using ABPS_WEB.Domain.AdvanceBooking;
using ABPS_WEB.Infrastructure.Data;
using ABPS_WEB.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ABPS_WEB.Infrastructure.AdvanceBooking;

/// <summary>Ported from ABPS_WPF's Controllers/AdvanceBookingController.cs, in full. Records a
/// guest's intent to book on a future date without touching tbl_Room — see
/// docs/ADVANCE_BOOKING_DESIGN.md (ABPS_WPF) for the full design rationale, unchanged here.</summary>
public class AdvanceBookingService : IAdvanceBookingService
{
    private readonly AbpsContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<AdvanceBookingService> _logger;

    public AdvanceBookingService(AbpsContext context, ICurrentUserService currentUser, ILogger<AdvanceBookingService> logger)
    {
        _context = context;
        _currentUser = currentUser;
        _logger = logger;
    }

    private static AdvanceBookingDto ToDto(TblAdvanceBooking a) => new(
        a.AdvanceBookingId, a.GuestName, a.PhoneNo, a.AadharNo, a.Address, a.NoPeople, a.NoRoomsRequested,
        a.RoomCategoryRtId, a.CatId, a.BondNo, a.RequestedCheckInDate, a.RequestedNights, a.AdvanceDepositAmount,
        a.PaymentTypeId, a.PaymentSubTypeId, a.ReferenceNumber, a.Notes, a.StatusId, a.AssignedBookingId, a.CancelledReason);

    public async Task<CreateAdvanceBookingResult> CreateAsync(AdvanceBookingRequest req)
    {
        _logger.LogInformation("CreateAdvanceBooking → Guest={Guest} Date={Date:yyyy-MM-dd}", req.GuestName, req.RequestedCheckInDate);
        try
        {
            var now = DateTime.Now;
            var advanceBooking = new TblAdvanceBooking
            {
                GuestName = req.GuestName,
                PhoneNo = req.PhoneNo,
                AadharNo = req.AadharNo,
                Address = req.Address,
                NoPeople = req.NoPeople,
                NoRoomsRequested = req.NoRoomsRequested,
                RoomCategoryRtId = req.RoomCategoryRtId,
                CatId = req.CatId,
                BondNo = req.BondNo,
                RequestedCheckInDate = req.RequestedCheckInDate,
                RequestedNights = req.RequestedNights,
                AdvanceDepositAmount = req.AdvanceDepositAmount,
                PaymentTypeId = req.PaymentTypeId,
                PaymentSubTypeId = req.PaymentSubTypeId,
                ReferenceNumber = req.ReferenceNumber,
                Notes = req.Notes,
                StatusId = (int)AdvanceBookingStatus.Pending,
                CreatedBy = _currentUser.UserName,
                CreatedTimestamp = now
            };

            _context.TblAdvanceBookings.Add(advanceBooking);
            await _context.SaveChangesAsync();

            _logger.LogInformation("CreateAdvanceBooking ← AdvanceBookingId={AdvanceBookingId}", advanceBooking.AdvanceBookingId);
            return new CreateAdvanceBookingResult(true, "Advance booking recorded successfully", advanceBooking.AdvanceBookingId);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating advance booking");
            return new CreateAdvanceBookingResult(false, "Error creating advance booking: " + ex.Message, 0);
        }
    }

    public async Task<List<AdvanceBookingDto>> SearchAsync(string? phone, string? name, DateTime? date, int? statusId, string? aadhaar = null)
    {
        try
        {
            var query = _context.TblAdvanceBookings.AsQueryable();

            if (!string.IsNullOrWhiteSpace(phone))
                query = query.Where(a => a.PhoneNo.Contains(phone));
            if (!string.IsNullOrWhiteSpace(name))
                query = query.Where(a => a.GuestName.Contains(name));
            if (!string.IsNullOrWhiteSpace(aadhaar))
                query = query.Where(a => a.AadharNo != null && a.AadharNo.Contains(aadhaar));
            if (date.HasValue)
                query = query.Where(a => a.RequestedCheckInDate.Date == date.Value.Date);
            if (statusId.HasValue && statusId.Value != 0)
                query = query.Where(a => a.StatusId == statusId.Value);

            var result = await query.OrderBy(a => a.RequestedCheckInDate).ToListAsync();
            return result.Select(ToDto).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error searching advance bookings");
            return new List<AdvanceBookingDto>();
        }
    }

    public async Task<List<AdvanceBookingDto>> GetDueAsync(DateTime asOf)
    {
        try
        {
            var result = await _context.TblAdvanceBookings
                .Where(a => a.StatusId == (int)AdvanceBookingStatus.Pending && a.RequestedCheckInDate.Date == asOf.Date)
                .OrderBy(a => a.RequestedCheckInDate)
                .ToListAsync();
            return result.Select(ToDto).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching due advance bookings");
            return new List<AdvanceBookingDto>();
        }
    }

    public async Task<AdvanceBookingOperationResult> AssignAsync(int advanceBookingId, int resultingBookingId)
    {
        _logger.LogInformation("AssignAdvanceBooking → AdvanceBookingId={AdvanceBookingId} BookingId={BookingId}", advanceBookingId, resultingBookingId);
        try
        {
            var advanceBooking = await _context.TblAdvanceBookings.FirstOrDefaultAsync(a => a.AdvanceBookingId == advanceBookingId);
            if (advanceBooking == null)
                return new AdvanceBookingOperationResult(false, "Advance booking not found");

            advanceBooking.StatusId = (int)AdvanceBookingStatus.Assigned;
            advanceBooking.AssignedBookingId = resultingBookingId;
            advanceBooking.AssignedBy = _currentUser.UserName;
            advanceBooking.AssignedTimestamp = DateTime.Now;
            await _context.SaveChangesAsync();

            return new AdvanceBookingOperationResult(true, "Advance booking assigned successfully");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error assigning advance booking");
            return new AdvanceBookingOperationResult(false, "Error assigning advance booking: " + ex.Message);
        }
    }

    public async Task<AdvanceBookingOperationResult> CancelAsync(int advanceBookingId, string reason)
    {
        _logger.LogInformation("CancelAdvanceBooking → AdvanceBookingId={AdvanceBookingId}", advanceBookingId);
        try
        {
            var advanceBooking = await _context.TblAdvanceBookings.FirstOrDefaultAsync(a => a.AdvanceBookingId == advanceBookingId);
            if (advanceBooking == null)
                return new AdvanceBookingOperationResult(false, "Advance booking not found");

            advanceBooking.StatusId = (int)AdvanceBookingStatus.Cancelled;
            advanceBooking.CancelledReason = reason;
            advanceBooking.ModifiedBy = _currentUser.UserName;
            advanceBooking.ModifiedTimestamp = DateTime.Now;
            await _context.SaveChangesAsync();

            return new AdvanceBookingOperationResult(true, "Advance booking cancelled");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error cancelling advance booking");
            return new AdvanceBookingOperationResult(false, "Error cancelling advance booking: " + ex.Message);
        }
    }

    public async Task<int> ExpireOverdueAsync()
    {
        try
        {
            var overdue = await _context.TblAdvanceBookings
                .Where(a => a.StatusId == (int)AdvanceBookingStatus.Pending && a.RequestedCheckInDate.Date < DateTime.Today)
                .ToListAsync();

            foreach (var a in overdue)
            {
                a.StatusId = (int)AdvanceBookingStatus.Expired;
                a.ModifiedTimestamp = DateTime.Now;
            }
            await _context.SaveChangesAsync();

            if (overdue.Count > 0)
                _logger.LogInformation("Expired {Count} overdue advance bookings", overdue.Count);
            return overdue.Count;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error expiring overdue advance bookings");
            return 0;
        }
    }
}

using System;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblAdvanceBooking
{
    public int AdvanceBookingId { get; set; }

    public string GuestName { get; set; } = null!;

    public string PhoneNo { get; set; } = null!;

    public string? AadharNo { get; set; }

    public string? Address { get; set; }

    public int NoPeople { get; set; }

    public int NoRoomsRequested { get; set; }

    public int? RoomCategoryRtId { get; set; }

    /// <summary>Booking category (e.g. Donar vs. regular) — same tbl_BookingCategory FK used
    /// by Book Room's cmbCategory. Donar-category advance bookings also carry <see cref="BondNo"/>.</summary>
    public int? CatId { get; set; }

    public string? BondNo { get; set; }

    public DateTime RequestedCheckInDate { get; set; }

    public int RequestedNights { get; set; }

    public decimal? AdvanceDepositAmount { get; set; }

    public int? PaymentTypeId { get; set; }

    public int? PaymentSubTypeId { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? Notes { get; set; }

    public int StatusId { get; set; }

    public int? AssignedBookingId { get; set; }

    public string? AssignedBy { get; set; }

    public DateTime? AssignedTimestamp { get; set; }

    public string? CancelledReason { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedTimestamp { get; set; }
}

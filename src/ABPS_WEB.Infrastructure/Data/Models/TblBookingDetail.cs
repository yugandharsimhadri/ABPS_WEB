using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblBookingDetail
{
    public int BookingId { get; set; }

    public DateTime? BookingDate { get; set; }

    public string? Name { get; set; }

    public string? AadharNo { get; set; }

    public string? PhoneNo { get; set; }

    public int? NoPeople { get; set; }

    public int CatId { get; set; }

    public string? Details { get; set; }

    public int Dtid { get; set; }

    public int DonarId { get; set; }

    public string? BondNo { get; set; }

    public int? NoOfRooms { get; set; }

    public decimal? Advance { get; set; }

    public decimal? TotalAmount { get; set; }

    public string? GroupBooking { get; set; }

    public string? AdvanceBooking { get; set; }

    public int BookingStatusId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }

    public string AddonRoom { get; set; } = null!;

    public string? Address { get; set; }
}

using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblBookingRoom
{
    public int BookingRoomId { get; set; }

    public int? BookingId { get; set; }

    public DateTime? BookingDate { get; set; }

    public int RoomId { get; set; }

    public string? GroupBooking { get; set; }

    public decimal? RoomAmount { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime? CreatedTimestamp { get; set; }

    public DateTime? CheckInDate { get; set; }

    public DateTime? CheckOutDate { get; set; }

    public int? BookingStatusId { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedTimestamp { get; set; }

    public string CancellationType { get; set; } = null!;
}

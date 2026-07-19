using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblTransactionRoomDonation
{
    public int TransactionId { get; set; }

    public int? BookingId { get; set; }

    public DateTime? CheckoutDate { get; set; }

    public int? NoofDays { get; set; }

    public decimal? TotalAmount { get; set; }

    public decimal? RoomAmount { get; set; }

    public decimal? DonationAmount { get; set; }

    public string? Notes { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

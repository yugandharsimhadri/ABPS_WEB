using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblBookingLedger
{
    public int BookingLedgerId { get; set; }

    public int? BookingId { get; set; }

    public int? NoOfDays { get; set; }

    public DateTime? CheckinDateTime { get; set; }

    public DateTime? CheckoutDateTime { get; set; }

    public decimal? Advance { get; set; }

    public decimal? IntendedTotalAmount { get; set; }

    public decimal? IntendedRefundAmount { get; set; }

    public decimal? ActualTotalAmount { get; set; }

    public decimal? ActualRefundAmount { get; set; }

    public string? CheckinByuser { get; set; }

    public DateTime? CheckinInsertDatetime { get; set; }

    public string? CheckoutByuser { get; set; }

    public DateTime? CheckoutInsertDatetime { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }

    public string? ModifiedBy { get; set; }

    public DateTime? ModifiedTimestamp { get; set; }
}

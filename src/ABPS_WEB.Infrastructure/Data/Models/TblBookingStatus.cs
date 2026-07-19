using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblBookingStatus
{
    public int BookingStatusId { get; set; }

    public string StatusCode { get; set; } = null!;

    public string? BookingStatusDesc { get; set; }

    public string? Notes { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

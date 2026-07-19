using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblAddonRoom
{
    public int AddonRoomId { get; set; }

    public DateTime AddonBookingDate { get; set; }

    public int BookingId { get; set; }

    public decimal AddAdvance { get; set; }

    public decimal TotalAdvance { get; set; }

    public decimal TotalAmount { get; set; }

    public string? Createdby { get; set; }

    public DateTime CreateTimestmap { get; set; }
}

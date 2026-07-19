using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblVacateRoom
{
    public int VacateRoomId { get; set; }

    public int? BookingId { get; set; }

    public int RoomId { get; set; }

    public DateTime? CheckoutDateTime { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

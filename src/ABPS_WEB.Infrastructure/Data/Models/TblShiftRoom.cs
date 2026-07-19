using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblShiftRoom
{
    public int ShiftRoomId { get; set; }

    public int? BookingId { get; set; }

    public int OldRoomId { get; set; }

    public int NewRoomId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

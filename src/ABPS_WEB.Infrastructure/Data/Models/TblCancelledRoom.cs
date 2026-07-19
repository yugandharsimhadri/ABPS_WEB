using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblCancelledRoom
{
    public int CancelledRoomId { get; set; }

    public int? BookingId { get; set; }

    public int RoomId { get; set; }

    public DateTime? CancelationDate { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

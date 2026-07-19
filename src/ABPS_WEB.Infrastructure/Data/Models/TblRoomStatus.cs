using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblRoomStatus
{
    public int RoomStatusId { get; set; }

    public string StatusCode { get; set; } = null!;

    public string? RoomStatusDesc { get; set; }

    public string? Notes { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

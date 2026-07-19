using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblRoomType
{
    public int RtId { get; set; }

    public string? RtDesc { get; set; }

    public string RoomCategory { get; set; } = null!;

    public string? RtCreatedBy { get; set; }

    public DateTime RtCreatedTimestamp { get; set; }

    public string IsActive { get; set; } = null!;
}

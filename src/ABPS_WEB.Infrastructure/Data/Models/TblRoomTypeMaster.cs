using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblRoomTypeMaster
{
    public int RtId { get; set; }

    public string? RtDesc { get; set; }

    public int? RtPrice { get; set; }

    public int? RtBed { get; set; }

    public string? RtAc { get; set; }

    public string? RtCreatedBy { get; set; }

    public DateTime RtCreatedTimestamp { get; set; }

    public string RoomCategory { get; set; } = null!;
}

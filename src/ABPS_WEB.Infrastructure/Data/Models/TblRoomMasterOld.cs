using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblRoomMasterOld
{
    public int RoomId { get; set; }

    public string? RoomNo { get; set; }

    public int? RtId { get; set; }

    public int? FloorId { get; set; }

    public int RoomStatusId { get; set; }

    public int BookingId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblRoom
{
    public int RoomId { get; set; }

    public int? RtId { get; set; }

    public int? FloorId { get; set; }

    public string? RoomNo { get; set; }

    public decimal Cost { get; set; }

    public int Beds { get; set; }

    public int Allowed { get; set; }

    public int RoomStatusId { get; set; }

    public int BookingId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }

    public decimal AdvanceAmount { get; set; }
}

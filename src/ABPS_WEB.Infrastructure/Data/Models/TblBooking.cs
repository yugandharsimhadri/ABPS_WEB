using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblBooking
{
    public int Id { get; set; }

    public int RoomId { get; set; }

    public string? DevoteeName { get; set; }

    public string? Phone { get; set; }

    public DateOnly FromDate { get; set; }

    public DateOnly ToDate { get; set; }

    public DateTime? CreatedAt { get; set; }
}

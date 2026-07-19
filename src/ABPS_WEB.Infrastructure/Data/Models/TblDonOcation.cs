using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblDonOcation
{
    public int DonationId { get; set; }

    public int DonarId { get; set; }

    public int OcassionId { get; set; }

    public string OcassionName { get; set; } = null!;

    public string? IntheNameof { get; set; }

    public string? AnandaanamDate { get; set; }

    public string? AnandaanamMonth { get; set; }

    public string? TMasam { get; set; }

    public string? TPasksham { get; set; }

    public string? TThidulu { get; set; }

    public int BondNo { get; set; }

    public int RoomDays { get; set; }

    public string? CreatedBy { get; set; }

    public byte[]? CreatedTimestamp { get; set; }
}

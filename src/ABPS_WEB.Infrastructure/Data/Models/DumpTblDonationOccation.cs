using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class DumpTblDonationOccation
{
    public int DonationOccationId { get; set; }

    public int DonarId { get; set; }

    public int DonationId { get; set; }

    public int? OcassionId { get; set; }

    public string OcassionName { get; set; } = null!;

    public string? IntheNameof { get; set; }

    public string? TeluguIntheNameof { get; set; }

    public string AnandaanamDateType { get; set; } = null!;

    public DateOnly? AnandaanamDate { get; set; }

    public int? TMasamId { get; set; }

    public int? TPaskshamId { get; set; }

    public int? TThiduluId { get; set; }

    public string? BondNo { get; set; }

    public int? RoomDays { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }

    public string? DateEnglishTelugu { get; set; }
}

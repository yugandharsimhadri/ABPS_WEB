using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblSmstransaction
{
    public int Smsid { get; set; }

    public int? DonarId { get; set; }

    public string? BondNo { get; set; }

    public int? OccasionId { get; set; }

    public string? Content { get; set; }

    public string PhoneNo { get; set; } = null!;

    public string DateType { get; set; } = null!;

    public DateOnly Smsdate { get; set; }

    public int? TMasamId { get; set; }

    public int? TPaskshamId { get; set; }

    public int? TThiduluId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class DumpDonlist
{
    public string? Doname { get; set; }

    public string? Dtype { get; set; }

    public int? Amt { get; set; }

    public string? Remarks { get; set; }

    public int? Id { get; set; }

    public double? Days { get; set; }

    public byte[] SsmaTimeStamp { get; set; } = null!;
}

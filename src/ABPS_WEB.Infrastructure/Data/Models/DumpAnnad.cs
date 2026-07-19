using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class DumpAnnad
{
    public double? Id { get; set; }

    public string? Name { get; set; }

    public double? Amt { get; set; }

    public double? Days { get; set; }

    public byte[] SsmaTimeStamp { get; set; } = null!;

    public int? OccassionId { get; set; }

    public string? NewBondNo { get; set; }

    public string? DateType { get; set; }

    public string? TeluguMasam { get; set; }

    public string? TeluguPaksham { get; set; }

    public string? TeluguThidhi { get; set; }

    public int? TeluguMasaId { get; set; }

    public int? TeluguPakshamid { get; set; }

    public int? TeluguThidhiid { get; set; }

    public int? EnglishDay { get; set; }

    public int? EnglishMonth { get; set; }

    public int? EnglishYear { get; set; }

    public string? ParseStatus { get; set; }

    public string? Rawdata { get; set; }
}

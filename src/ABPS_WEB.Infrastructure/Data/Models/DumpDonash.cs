using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class DumpDonash
{
    public double? Id { get; set; }

    public string? Namedon { get; set; }

    public double? Amtdon { get; set; }

    public string? Dateofann { get; set; }

    public string? Addressdon { get; set; }

    public string? Phoneno { get; set; }

    public string? Sno { get; set; }

    public int Id1 { get; set; }

    public string? T1 { get; set; }

    public string? T2 { get; set; }

    public string? Sda { get; set; }

    public byte[] SsmaTimeStamp { get; set; } = null!;
}

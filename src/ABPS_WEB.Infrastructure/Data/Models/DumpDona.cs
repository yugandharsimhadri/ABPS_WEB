using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class DumpDona
{
    public int Id { get; set; }

    public string? Namedon { get; set; }

    public string? Wsname { get; set; }

    public string? Donty { get; set; }

    public string? Gothram { get; set; }

    public string? Addressdon { get; set; }

    /// <summary>
    /// RECEIVED
    /// </summary>
    public int? Amtdon { get; set; }

    /// <summary>
    /// PAYMENT
    /// </summary>
    public int? Amtpaid { get; set; }

    public DateTime? Dateofdon { get; set; }

    public DateTime? Datepaid { get; set; }

    public string? Dateofann { get; set; }

    public string? Userd { get; set; }

    public string? Chdrno { get; set; }

    public int? Pfno { get; set; }

    public string? Mode { get; set; }

    public string? Place { get; set; }

    /// <summary>
    /// VOUCHER NO
    /// </summary>
    public string? Vcno { get; set; }

    public string? Details { get; set; }

    public short? Alotdays { get; set; }

    public string? Lastvis { get; set; }

    public string? Code { get; set; }

    public string? Part { get; set; }

    public short? Tdays { get; set; }

    public string? Rupees { get; set; }

    public string? Cash { get; set; }

    public string? Occ { get; set; }

    public string? Bdt { get; set; }

    public string? Pan { get; set; }

    public byte[] SsmaTimeStamp { get; set; } = null!;

    public int? DonarTypeId { get; set; }

    public string? NewBondNo { get; set; }
}

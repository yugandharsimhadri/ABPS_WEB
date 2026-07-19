using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblTeluguMasam
{
    public int TMasamId { get; set; }

    public string? TMasamName { get; set; }

    public string? TMasamNameEnglish { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

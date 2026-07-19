using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblTeluguThidi
{
    public int TThidiId { get; set; }

    public string? TThidiName { get; set; }

    public string? TThidiNameEnglish { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

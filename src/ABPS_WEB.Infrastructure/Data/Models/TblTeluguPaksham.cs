using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblTeluguPaksham
{
    public int TPakshamId { get; set; }

    public string? TPakshamName { get; set; }

    public string? TPakshamNameEnglish { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

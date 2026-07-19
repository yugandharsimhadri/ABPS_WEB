using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblDayMaster
{
    public int DayId { get; set; }

    public int? DayNo { get; set; }

    public int? MonthId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblErrorLog
{
    public int ErrorLogId { get; set; }

    public DateTime? LogDate { get; set; }

    public string? LogLevel { get; set; }

    public string? Logger { get; set; }

    public string? Message { get; set; }

    public string? Exception { get; set; }
}

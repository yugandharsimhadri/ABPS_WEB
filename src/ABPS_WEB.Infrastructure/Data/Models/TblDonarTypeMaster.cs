using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblDonarTypeMaster
{
    public int Dtid { get; set; }

    public string? Donartype { get; set; }

    public int? Days { get; set; }

    public string? Code { get; set; }

    public int? Amount { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblOperator
{
    public int OperatorId { get; set; }

    public string? OperatorName { get; set; }

    public string? Password { get; set; }

    public string? Status { get; set; }

    public DateTime? LastLoginDateTime { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

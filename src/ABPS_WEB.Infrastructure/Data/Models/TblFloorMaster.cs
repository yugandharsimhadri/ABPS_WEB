using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblFloorMaster
{
    public short FloorId { get; set; }

    public string? FloorDescription { get; set; }

    public string? Active { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

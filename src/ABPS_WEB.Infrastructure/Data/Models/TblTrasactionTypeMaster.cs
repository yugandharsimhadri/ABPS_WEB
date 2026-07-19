using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblTrasactionTypeMaster
{
    public int TrasactionTypeId { get; set; }

    public string? TransactionType { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

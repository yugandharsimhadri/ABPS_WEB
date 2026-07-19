using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblPaymentSubTypeMaster
{
    public int PaymentSubTypeId { get; set; }

    public string? PaymentSubType { get; set; }

    public int PaymentTypeId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

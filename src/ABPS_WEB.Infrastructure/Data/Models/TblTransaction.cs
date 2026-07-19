using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblTransaction
{
    public int TransactionId { get; set; }

    public int? TransactionCategoryId { get; set; }

    public int? DonarBookingId { get; set; }

    public int TrasactionTypeId { get; set; }

    public DateTime? TransactionDate { get; set; }

    public decimal? Amount { get; set; }

    public int? PaymentTypeId { get; set; }

    public int? PaymentSubTypeId { get; set; }

    public string? ReferenceNumber { get; set; }

    public string? Notes { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }

    public string? BondNo { get; set; }
}

using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblDonation
{
    public int DonationId { get; set; }

    public int DonarId { get; set; }

    public int DonationTypeId { get; set; }

    public decimal DonationAmount { get; set; }

    public DateTime? DonationDate { get; set; }

    public int? PaymentTypeId { get; set; }

    public int? PaymentSubTypeId { get; set; }

    public string? TransactionNo { get; set; }

    public string? TransactionDetails { get; set; }

    public string? BondNo { get; set; }

    public int RoomDays { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

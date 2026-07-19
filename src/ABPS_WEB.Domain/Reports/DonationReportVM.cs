using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPS_WEB.Domain.Reports
{
    public class DonationReportVM
    {
        [DisplayName("Donation ID")]
        public int DonationId { get; set; }

        [DisplayName("Donation Date")]
        public DateTime? DonationDate { get; set; }

        [DisplayName("Donor Name")]
        public string DonorName { get; set; }

        [DisplayName("Phone No")]
        public string PhoneNo { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Donation Type")]
        public string DonationType { get; set; }

        [DisplayName("Donation Amount")]
        public decimal DonationAmount { get; set; }

        [DisplayName("Payment Type")]
        public string PaymentType { get; set; }

        [DisplayName("Transaction No")]
        public string TransactionNo { get; set; }

        [DisplayName("Occasion Name")]
        public string OccasionName { get; set; }

        [DisplayName("In the Name Of")]
        public string InTheNameOf { get; set; }

        [DisplayName("Room Days")]
        public int? RoomDays { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("Bond No")]
        public string BondNo { get; set; }
    }

}

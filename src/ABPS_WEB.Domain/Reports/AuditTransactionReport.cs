using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPS_WEB.Domain.Reports
{
    public class AuditTransactionReport
    {
        [DisplayName("Booking ID")]
        public int? BookingId { get; set; }

        [DisplayName("Room Numbers")]
        public string RoomNos { get; set; }

        [DisplayName("Booked Name")]
        public string BookedName { get; set; }

        [DisplayName("Checkout Date")]
        public DateTime? CheckoutDate { get; set; }

        [DisplayName("No. of Days")]
        public int? NoofDays { get; set; }

        [DisplayName("Per Day Amount")]
        public decimal? PerDayAmount { get; set; }

        [DisplayName("Total Amount")]
        public decimal? TotalAmount { get; set; }

        [DisplayName("Room Amount")]
        public decimal? RoomAmount { get; set; }

        [DisplayName("Donation Amount")]
        public decimal? DonationAmount { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("Created Timestamp")]
        public DateTime? CreatedTimestamp { get; set; }

    }
}

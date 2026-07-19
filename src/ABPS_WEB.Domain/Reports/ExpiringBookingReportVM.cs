using System;
using System.ComponentModel;

namespace ABPS_WEB.Domain.Reports
{
    public class ExpiringBookingReportVM
    {
        [DisplayName("Booking ID")]
        public int BookingId { get; set; }

        [DisplayName("Guest Name")]
        public string Name { get; set; }

        [DisplayName("Phone No")]
        public string PhoneNo { get; set; }

        [DisplayName("No. of Rooms")]
        public int? NoOfRooms { get; set; }

        [DisplayName("Total Amount")]
        public decimal? TotalAmount { get; set; }

        [DisplayName("Booking Ledger ID")]
        public int BookingLedgerId { get; set; }

        [DisplayName("No. of Days")]
        public int? NoOfDays { get; set; }

        [DisplayName("Check-in Date/Time")]
        public DateTime? CheckinDateTime { get; set; }

        [DisplayName("Checkout Date/Time")]
        public DateTime? CheckoutDateTime { get; set; }

        [DisplayName("Expected Checkout Date")]
        public DateTime ExpectedCheckoutDate { get; set; }
    }
}

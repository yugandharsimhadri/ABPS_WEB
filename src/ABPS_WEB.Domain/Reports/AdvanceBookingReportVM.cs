using System;
using System.ComponentModel;

namespace ABPS_WEB.Domain.Reports
{
    public class AdvanceBookingReportVM
    {
        [DisplayName("Advance Booking ID")]
        public int AdvanceBookingId { get; set; }

        [DisplayName("Guest Name")]
        public string GuestName { get; set; }

        [DisplayName("Phone No")]
        public string PhoneNo { get; set; }

        [DisplayName("Aadhaar No")]
        public string AadharNo { get; set; }

        [DisplayName("No. of People")]
        public int NoPeople { get; set; }

        [DisplayName("Rooms Requested")]
        public int NoRoomsRequested { get; set; }

        [DisplayName("Room Category")]
        public string RoomCategory { get; set; }

        [DisplayName("Booking Type")]
        public string BookingType { get; set; }

        [DisplayName("Bond No")]
        public string BondNo { get; set; }

        [DisplayName("Requested Check-in Date")]
        public DateTime RequestedCheckInDate { get; set; }

        [DisplayName("Requested Nights")]
        public int RequestedNights { get; set; }

        [DisplayName("Advance Deposit Amount")]
        public decimal AdvanceDepositAmount { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Notes")]
        public string Notes { get; set; }

        [DisplayName("Created Timestamp")]
        public DateTime CreatedTimestamp { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPS_WEB.Domain.Reports
{
    public class RoomReportVM
    {
        [DisplayName("Room ID")]
        public int RoomId { get; set; }

        [DisplayName("Room No")]
        public string RoomNo { get; set; }

        [DisplayName("Category")]
        public string Category { get; set; }

        [DisplayName("Floor")]
        public string Floor { get; set; }

        [DisplayName("Beds")]
        public int Beds { get; set; }

        [DisplayName("Allowed")]
        public int Allowed { get; set; }

        [DisplayName("Price")]
        public decimal Price { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Customer Name")]
        public string CustomerName { get; set; }

        [DisplayName("Phone No")]
        public string PhoneNo { get; set; }

        [DisplayName("Booking Date")]
        public DateTime? BookingDate { get; set; }

        [DisplayName("Booking ID")]
        public int? BookingId { get; set; }
    }
}

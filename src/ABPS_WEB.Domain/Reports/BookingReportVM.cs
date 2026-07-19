using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPS_WEB.Domain.Reports
{
    public class BookingReportVM
    {
        public int BookingId { get; set; }          // BookingId
        public int NoOfRooms { get; set; }         // Count of rooms in booking
        public string Name { get; set; }           // Guest name

        public DateTime DateBooking { get; set; }  // Booking date (date part)
        public TimeSpan TimeBooking { get; set; }  // Booking time (time part)

        public DateTime? DateArrived { get; set; } // Check-in date (date part)
        public TimeSpan? TimeArrived { get; set; } // Check-in time (time part)

        public DateTime? DateCheckOut { get; set; } // Check-out date (date part)
        public TimeSpan? TimeCheckOut { get; set; } // Check-out time (time part)

        public decimal Advance { get; set; }       // Advance amount
        public int Persons { get; set; }           // Number of persons
        public string UserName { get; set; }           // CreatedBy / UserName
        public string PhoneNo { get; set; }        // Phone number
        public string RoomNos { get; set; }        // Comma-separated list of room numbers
                                                   //public string RoomNos { get; set; }        // Comma-separated list of room numbers

    }
}

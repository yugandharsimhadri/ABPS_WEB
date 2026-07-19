using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPS_WEB.Domain.Reports
{
    // [DisplayName] drives the grid column headers (DataGridColumnHelper.RightAlignNumericColumns
    // sets e.Column.Header from PropertyDescriptor.DisplayName) and the Excel/PDF export headers
    // (GenericReportWindow.Export_Click / PdfExportHelperV2 both read PropertyDescriptor.DisplayName
    // too), so one attribute per property covers on-screen and exported output at once.
    public class OccupancyReportVM
    {
        [DisplayName("Guest Name")]
        public string Name { get; set; }

        [DisplayName("Phone No")]
        public string PhoneNo { get; set; }

        [DisplayName("No. of Rooms")]
        public int? NoOfRooms { get; set; }

        [DisplayName("Room Numbers")]
        public string RoomNos { get; set; }

        [DisplayName("Total Amount")]
        public decimal? TotalAmount { get; set; }

        [DisplayName("No. of People")]
        public int? NoPeople { get; set; }

        [DisplayName("No. of Days")]
        public int? NoOfDays { get; set; }

        [DisplayName("Check-in Date/Time")]
        public DateTime? CheckinDateTime { get; set; }

        [DisplayName("Expected Checkout Date")]
        public DateTime? ExpectedCheckoutDate { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPS_WEB.Domain.Reports
{
    public class ReceiptData
    {
        public ReceiptType ReceiptType { get; set; }

        public string ReceiptNo { get; set; }

        public DateTime? ReceiptDate { get; set; }
        public TimeSpan? ReceiptTime { get; set; }
        public string CustomerName { get; set; }

        public string Address { get; set; }

        public string PhoneNo { get; set; }

        public decimal Amount { get; set; }

        public string AmountWords { get; set; }

        public string RoomNo { get; set; }

        public int Persons { get; set; }

        public int NoOfRooms { get; set; }

        public string RoomNos { get; set; }

        public string Days { get; set; }

        public DateTime? ArrivedDate { get; set; }

        public TimeSpan? ArrivedTime { get; set; }

        public DateTime? VacatedDate { get; set; }

        public TimeSpan? VacatedTime { get; set; }

        public decimal DonationAmount { get; set; }

        public decimal MaintenanceAmount { get; set; }

        public decimal TotalAmount { get; set; }

        public decimal Maintenance { get; set; }

    }

    public enum ReceiptType
    {
        Donation,
        Precaution,
        Annadanam
    }
}

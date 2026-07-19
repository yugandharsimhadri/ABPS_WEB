using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPS_WEB.Domain.Reports
{
    public class UserTransactionDetails
    {
        [DisplayName("Transaction Date")]
        public DateTime? TransactionDate { get; set; }

        [DisplayName("Category")]
        public string CategoryName { get; set; }

        [DisplayName("Type")]
        public string TypeName { get; set; }

        [DisplayName("Booking ID")]
        public int? DonarBookingId { get; set; }

        [DisplayName("Amount")]
        public decimal Amount { get; set; }

        [DisplayName("Payment Type")]
        public string PaymentType { get; set; }

        [DisplayName("Payment Sub Type")]
        public string PaymentSubType { get; set; }

        [DisplayName("Reference No")]
        public string ReferenceNumber { get; set; }

        [DisplayName("Bond No")]
        public string BondNo { get; set; }

        [DisplayName("Notes")]
        public string Notes { get; set; }

        [DisplayName("Created By")]
        public string CreatedBy { get; set; }

        [DisplayName("Created Timestamp")]
        public DateTime? CreatedTimestamp { get; set; }
    }
}

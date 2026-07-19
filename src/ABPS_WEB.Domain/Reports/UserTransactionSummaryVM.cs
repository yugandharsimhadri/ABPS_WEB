using System;
using System.ComponentModel;

namespace ABPS_WEB.Domain.Reports
{
    public class UserTransactionSummaryVM
    {
        [DisplayName("Transaction Date")]
        public DateTime? TransactionDate { get; set; }

        [DisplayName("Booking ID")]
        public int? DonarBookingId { get; set; }

        [DisplayName("Category")]
        public string CategoryName { get; set; }

        [DisplayName("Login User")]
        public string LoginUser { get; set; }

        [DisplayName("Advance")]
        public decimal? Advance { get; set; }

        [DisplayName("Payment")]
        public decimal? Payment { get; set; }

        [DisplayName("Refund")]
        public decimal? Refund { get; set; }
    }
}

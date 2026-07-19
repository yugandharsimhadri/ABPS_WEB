using System.ComponentModel;

namespace ABPS_WEB.Domain.Reports
{
    public class TransactionSummaryMonthWise
    {
        [DisplayName("Year")]
        public int Year { get; set; }

        [DisplayName("Month")]
        public int Month { get; set; }

        [DisplayName("Month Name")]
        public string MonthName { get; set; }

        [DisplayName("Category")]
        public string Category { get; set; }

        [DisplayName("Type")]
        public string Type { get; set; }

        [DisplayName("Total Transactions")]
        public int TotalTransactions { get; set; }

        [DisplayName("Total Amount")]
        public decimal? TotalAmount { get; set; }
    }
}

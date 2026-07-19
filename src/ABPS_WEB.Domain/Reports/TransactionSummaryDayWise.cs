using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPS_WEB.Domain.Reports
{
    public class TransactionSummaryDayWise
    {
        [DisplayName("Date")]
        public DateTime? Date { get; set; }

        [DisplayName("Category")]
        public string Category { get; set; }

        [DisplayName("Type")]
        public string Type { get; set; }

        [DisplayName("Total Transactions")]
        public int TotalTransactions { get; set; }

        [DisplayName("Total Amount")]
        public decimal TotalAmount { get; set; }
    }
}

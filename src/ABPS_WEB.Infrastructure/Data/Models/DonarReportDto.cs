using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ABPS_WEB.Infrastructure.Data.Models
{
    internal class DonarReportDto
    {
        public int DonarId { get; set; }

        public required string Name { get; set; }

        public required string PhoneNo { get; set; }

        public required string EmailId { get; set; }

        public required string Pan { get; set; }

        public required string Aadhaar { get; set; }

        public required string Address { get; set; }
    }
}

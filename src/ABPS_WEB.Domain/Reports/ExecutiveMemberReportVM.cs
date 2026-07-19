using System;
using System.ComponentModel;

namespace ABPS_WEB.Domain.Reports
{
    public class ExecutiveMemberReportVM
    {
        [DisplayName("Member Name")]
        public string MemberName { get; set; }

        [DisplayName("Member Type")]
        public string MemberType { get; set; }

        [DisplayName("Phone")]
        public string Phone { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Active")]
        public string IsActive { get; set; }

        [DisplayName("Effective From")]
        public DateOnly? EffectiveFrom { get; set; }

        [DisplayName("Effective To")]
        public DateOnly? EffectiveTo { get; set; }
    }
}

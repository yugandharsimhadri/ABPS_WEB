using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblBoardMember
{
    public int MemberId { get; set; }

    public string? MemberName { get; set; }

    public string? MemberTname { get; set; }

    public string? MemberType { get; set; }

    public string? Phone { get; set; }

    public string? Email { get; set; }

    public string? Address { get; set; }

    public string? IsActive { get; set; }

    public DateOnly? EffectiveFrom { get; set; }

    public DateOnly? EffectiveTo { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

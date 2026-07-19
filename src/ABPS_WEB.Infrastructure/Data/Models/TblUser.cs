using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblUser
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? MobileNumber { get; set; }

    public int RoleId { get; set; }

    public string? IsActive { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime UpdatedTimestamp { get; set; }
}

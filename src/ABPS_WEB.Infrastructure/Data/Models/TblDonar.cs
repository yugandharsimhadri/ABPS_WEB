using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblDonar
{
    public int DonarId { get; set; }

    public int DonarTypeId { get; set; }

    public string? Name { get; set; }

    public string? TeluguName { get; set; }

    public string? Gotram { get; set; }

    public string? PhoneNo { get; set; }

    public string? EmailId { get; set; }

    public string? Pan { get; set; }

    public string? Aadhaar { get; set; }

    public string? Address { get; set; }

    public int DistrictId { get; set; }

    public int StateId { get; set; }

    public int CountryId { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

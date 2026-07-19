using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblDistrict
{
    public int DistrictId { get; set; }

    public int StateId { get; set; }

    public int CountryId { get; set; }

    public string? DistrictName { get; set; }

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

using System;
using System.Collections.Generic;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblExtendroom
{
    public int ExtendRoomId { get; set; }

    public int Bookingid { get; set; }

    public int Prevnoofdays { get; set; }

    public DateTime Precheckout { get; set; }

    public int Extendeddays { get; set; }

    public DateTime Newcheckout { get; set; }

    public int Totaldays { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }
}

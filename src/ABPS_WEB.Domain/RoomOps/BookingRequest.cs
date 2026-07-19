namespace ABPS_WEB.Domain.RoomOps;

// Ported verbatim from ABPS_WPF/Models/ViewModel/BookingRequest.cs.
public class BookingRequest
{
    public required string Name { get; set; }
    public string? Address { get; set; }
    public required string AadhaarNo { get; set; }
    public required string PhoneNo { get; set; }
    public int NoOfPeople { get; set; }

    public int CatID { get; set; }
    public required string BookCategory { get; set; }
    public string? Details { get; set; }
    public int DTID { get; set; }
    public int DonarID { get; set; }
    public string? BondNo { get; set; }

    public int NoOfRooms { get; set; }
    public decimal Advance { get; set; }
    public decimal TotalAmount { get; set; }

    public string? GroupBooking { get; set; }
    public string? AdvanceBooking { get; set; }

    public int NoOfDays { get; set; }
    public int TotalAllowed { get; set; }
    public DateTime CheckinDate { get; set; }

    public int PaymentTypeID { get; set; }
    public int PaymentSubTypeID { get; set; }

    public string? PaymentType { get; set; }
    public string? PaymentSubType { get; set; }

    public string? TrasactionNo { get; set; }

    public List<RoomVM> Rooms { get; set; } = new();
}

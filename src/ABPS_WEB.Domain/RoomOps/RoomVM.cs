namespace ABPS_WEB.Domain.RoomOps;

// Ported verbatim from ABPS_WPF/Models/ViewModel/RoomVM.cs.
public class RoomVM
{
    public int RoomId { get; set; }
    public string RoomCategory { get; set; } = null!;
    public int RtId { get; set; }
    public string RoomNo { get; set; } = null!;
    public decimal? RtPrice { get; set; }
    public decimal? RoomAdvance { get; set; }
    public short FloorId { get; set; }
    public string? FloorDescription { get; set; }
    public int Beds { get; set; }
    public int Allowed { get; set; }
    public bool IsAvailable { get; set; }
}

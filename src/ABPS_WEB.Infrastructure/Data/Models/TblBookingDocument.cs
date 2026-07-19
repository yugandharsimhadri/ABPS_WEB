using System;

namespace ABPS_WEB.Infrastructure.Data.Models;

public partial class TblBookingDocument
{
    public int BookingDocumentId { get; set; }

    public int BookingId { get; set; }

    public string DocumentType { get; set; } = null!;

    public string? FileName { get; set; }

    public string? ContentType { get; set; }

    public byte[] FileData { get; set; } = null!;

    public string? CreatedBy { get; set; }

    public DateTime CreatedTimestamp { get; set; }
}

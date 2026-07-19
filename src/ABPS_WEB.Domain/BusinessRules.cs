namespace ABPS_WEB.Domain;

/// <summary>
/// Ported verbatim from ABPS_WPF/Helper/BusinessRules.cs. Central home for business constants
/// so a rate/rule change only happens in one place. (Future: load from a settings table so
/// changes don't need a redeploy — same note as the WPF version.)
/// </summary>
public static class BusinessRules
{
    /// <summary>Portion of the room charge treated as "room donation", per day per room (₹).</summary>
    public const double RoomDonationPerDayPerRoom = 900.0;

    /// <summary>Stays shorter than this many hours are charged a flat 1-day rate.</summary>
    public const int OneDayThresholdHours = 26;
}

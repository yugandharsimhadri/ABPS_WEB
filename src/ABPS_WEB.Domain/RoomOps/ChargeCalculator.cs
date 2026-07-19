namespace ABPS_WEB.Domain.RoomOps;

public record RoomChargeLine(string RoomNo, decimal PerDay, int Days, decimal Subtotal, bool ClosedEarly);
public record RoomChargeBreakdown(List<RoomChargeLine> Lines, decimal Total);
public record DonationSplit(decimal RoomDonation, decimal OtherDonation);
public record Settlement(decimal Refund, decimal PayDue);

public record RoomChargeInput(string RoomNo, decimal PerDay, DateTime? StoredCheckout);

/// <summary>
/// Ported from ABPS_WPF/VacateRoomWindow.xaml.cs's ComputeRoomCharges/RecalcCore — this was
/// client-side code-behind logic in the desktop app; moved server-side here per
/// docs/APPLICATION_REVIEW.md item 38 ("business logic belongs in a service, not a View"),
/// and because a React frontend shouldn't duplicate money math.
///
/// NOTE — a discrepancy worth flagging, not silently resolved: docs/BUSINESS_RULES.md §1
/// documents an "hours < 26 → flat 1-day charge, >= 26 hours → calendar-day difference" rule.
/// The ABPS_WPF code actually in production today does NOT implement that hour threshold —
/// it's a pure calendar-date difference (min 1 day) with no hourly special case. Since the
/// mandate is to match ABPS_WPF's actual current behavior (not the older written spec), this
/// ports the calendar-date-diff behavior exactly as WPF runs it today. Whether the 26-hour
/// rule was intentionally dropped during the WPF rewrite or is a regression is a product
/// question, not a coding one — flagged for a decision, not decided here.
/// </summary>
public static class ChargeCalculator
{
    /// <summary>Each room bills its own segment: a room still checked in is billed from the
    /// original check-in to <paramref name="finalCheckout"/>; a room already closed early
    /// (via Partial Checkout) is billed from check-in to ITS OWN stored checkout date.</summary>
    public static RoomChargeBreakdown ComputeRoomCharges(
        DateTime checkinDate, DateTime finalCheckout, IEnumerable<RoomChargeInput> rooms)
    {
        var lines = new List<RoomChargeLine>();
        decimal total = 0;
        foreach (var r in rooms)
        {
            bool closedEarly = r.StoredCheckout.HasValue;
            DateTime effectiveCheckout = r.StoredCheckout ?? finalCheckout;
            int days = Math.Max((effectiveCheckout.Date - checkinDate.Date).Days, 1);
            decimal subtotal = r.PerDay * days;
            lines.Add(new RoomChargeLine(r.RoomNo, r.PerDay, days, subtotal, closedEarly));
            total += subtotal;
        }
        return new RoomChargeBreakdown(lines, total);
    }

    public static DonationSplit ComputeDonationSplit(decimal adjustedAmount, int noofdays, int roomCount)
    {
        decimal cap = (decimal)BusinessRules.RoomDonationPerDayPerRoom * Math.Max(noofdays, 1) * roomCount;
        return adjustedAmount > cap
            ? new DonationSplit(cap, adjustedAmount - cap)
            : new DonationSplit(adjustedAmount, 0);
    }

    public static Settlement ComputeSettlement(decimal adjustedAmount, decimal advance) =>
        adjustedAmount > advance
            ? new Settlement(0, adjustedAmount - advance)
            : new Settlement(advance - adjustedAmount, 0);
}

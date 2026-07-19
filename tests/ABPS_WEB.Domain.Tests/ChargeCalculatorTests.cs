using ABPS_WEB.Domain.RoomOps;
using Xunit;

namespace ABPS_WEB.Domain.Tests;

public class ChargeCalculatorTests
{
    [Fact]
    public void ComputeRoomCharges_SingleRoom_TwoDays()
    {
        var checkin = new DateTime(2026, 7, 1, 14, 0, 0);
        var checkout = new DateTime(2026, 7, 3, 10, 0, 0);
        var rooms = new[] { new RoomChargeInput("101", 900m, null) };

        var result = ChargeCalculator.ComputeRoomCharges(checkin, checkout, rooms);

        Assert.Equal(1800m, result.Total);
        Assert.Equal(2, result.Lines[0].Days);
    }

    [Fact]
    public void ComputeRoomCharges_SameDayCheckout_MinimumOneDayCharged()
    {
        var checkin = new DateTime(2026, 7, 1, 14, 0, 0);
        var checkout = new DateTime(2026, 7, 1, 18, 0, 0);
        var rooms = new[] { new RoomChargeInput("101", 900m, null) };

        var result = ChargeCalculator.ComputeRoomCharges(checkin, checkout, rooms);

        Assert.Equal(900m, result.Total);
        Assert.Equal(1, result.Lines[0].Days);
    }

    [Fact]
    public void ComputeRoomCharges_RoomClosedEarly_BillsItsOwnStoredCheckout()
    {
        var checkin = new DateTime(2026, 7, 1);
        var finalCheckout = new DateTime(2026, 7, 5); // rest of booking runs 4 days
        var rooms = new[]
        {
            new RoomChargeInput("101", 900m, new DateTime(2026, 7, 2)), // partial-checked-out after 1 day
            new RoomChargeInput("102", 900m, null),
        };

        var result = ChargeCalculator.ComputeRoomCharges(checkin, finalCheckout, rooms);

        Assert.Equal(1, result.Lines[0].Days);
        Assert.True(result.Lines[0].ClosedEarly);
        Assert.Equal(4, result.Lines[1].Days);
        Assert.False(result.Lines[1].ClosedEarly);
        Assert.Equal(900m + 3600m, result.Total);
    }

    [Fact]
    public void ComputeDonationSplit_UnderCap_AllRoomDonation()
    {
        var split = ChargeCalculator.ComputeDonationSplit(1500m, noofdays: 1, roomCount: 2); // cap = 900*1*2=1800

        Assert.Equal(1500m, split.RoomDonation);
        Assert.Equal(0m, split.OtherDonation);
    }

    [Fact]
    public void ComputeDonationSplit_OverCap_SplitsExcessToOtherDonation()
    {
        var split = ChargeCalculator.ComputeDonationSplit(2500m, noofdays: 1, roomCount: 2); // cap = 1800

        Assert.Equal(1800m, split.RoomDonation);
        Assert.Equal(700m, split.OtherDonation);
        Assert.Equal(2500m, split.RoomDonation + split.OtherDonation); // invariant from BUSINESS_RULES.md §2
    }

    [Fact]
    public void ComputeSettlement_AdjustedBelowAdvance_Refund()
    {
        var settlement = ChargeCalculator.ComputeSettlement(adjustedAmount: 1500m, advance: 2000m);

        Assert.Equal(500m, settlement.Refund);
        Assert.Equal(0m, settlement.PayDue);
    }

    [Fact]
    public void ComputeSettlement_AdjustedAboveAdvance_PayDue()
    {
        var settlement = ChargeCalculator.ComputeSettlement(adjustedAmount: 2500m, advance: 2000m);

        Assert.Equal(0m, settlement.Refund);
        Assert.Equal(500m, settlement.PayDue);
    }

    [Fact]
    public void ComputeSettlement_ExactMatch_ZeroRefundAndPayDue()
    {
        var settlement = ChargeCalculator.ComputeSettlement(adjustedAmount: 2000m, advance: 2000m);

        Assert.Equal(0m, settlement.Refund);
        Assert.Equal(0m, settlement.PayDue);
    }
}

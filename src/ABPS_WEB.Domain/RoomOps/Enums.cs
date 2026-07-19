using System.ComponentModel;

namespace ABPS_WEB.Domain.RoomOps;

// Ported verbatim from ABPS_WPF/Enums/BookingStatus.cs — same numeric values (tbl_* status
// columns in the shared DB already contain these), just relocated to the Domain project.

public enum BookingStatus
{
    CheckOut = 1,
    CheckIn = 2,
    Cancelled = 3,
    Booked = 4,
    Reserved = 5,
    Shifted = 6
}

public enum PrintTypeStatus
{
    CheckOut = 1,
    CheckIn = 2,
    Cancel = 3,
    ShiftRoom = 4,
    ExtendRoom = 5,
    PartialChceckout = 6,
    PartialCancel = 7,
    AddRoom = 8
}

public enum RoomStatus
{
    Available = 1,
    Occupied = 2,
    Vacant = 3,
    Cleaning = 4,
    Maintenance = 5,
    Reserved = 6
}

public enum PaymentType
{
    Cash = 1,
    Online = 2,
    Card = 3
}

public enum RoomTypes
{
    AirConditionar = 1,
    NonAC = 2,
    LuxuryRoom = 3,
    Dormentary = 4,
    Lockers = 5,
    FunctionHall = 6
}

public enum TransactionType
{
    Advance = 1,
    Payment = 2,
    Refund = 3
}

public enum TransactionCategory
{
    RoomBooking = 1,
    Donation = 2,
    CAF = 3
}

public enum EnumDonorTypes
{
    [Description("ONE DAY ANNADANAM")]
    OneDayAnnadanam = 1,

    [Description("MAHARAJA POSHAKULU")]
    MaharajaPoshakulu = 2,

    [Description("RAJA POSHAKULU")]
    RajaPoshakulu = 3,

    [Description("POSHAKULU")]
    Poshakulu = 4,

    [Description("DATHALU")]
    Dathalu = 5,

    [Description("OTHER DONATION")]
    OtherDonation = 6,

    [Description("AKSHAYA MAHARAJA POSHAKULU")]
    AkshayaMaharajaPoshakulu = 7,

    [Description("ANNADANAM DONATION")]
    AnnadanamDonation = 8,

    [Description("ONEDAY SWEET DATHA")]
    OneDaySweetDatha = 9
}

namespace ABPS_WEB.Domain.RoomOps;

public record SaveBookingResult(bool Success, string Message, int BookingId);
public record OperationResult(bool Success, string Message);
public record BookingCategory(int CatId, string Category);

public record BookingSummary(int BookingId, DateTime BookingDate, string? Name, string? PhoneNo, string? Details, decimal? Advance, decimal? TotalAmount, DateTime? CheckinDateTime);

public record BookingRoomLine(int RoomId, string? RoomNo, decimal RoomAmount);

public record VacateRoomRequest(
    int BookingId, DateTime CheckoutDateTime, decimal AdjustedAmount, decimal RefundAmount,
    string PaymentType, int PaymentSubTypeId, string? TransactionNo, decimal PayAmount,
    int NoOfDays, decimal RoomDonation, decimal OtherDonation);

public record ExtendRoomRequest(
    int BookingId, int NoOfDays, int ExtendDays, int TotalExtendDays, decimal UpdateTotalAmount,
    decimal TotalAdvance, decimal AddAdvance, DateTime NewCheckout, int PaymentTypeId,
    int PaymentSubTypeId, string? TransactionNo, DateTime OldCheckout);

public record ShiftRoomRequest(
    int FromRoomId, int ToRoomId, decimal AddAdvance, decimal TotalAdvance, decimal TotalAmount,
    int BookingId, int PaymentTypeId, int PaymentSubTypeId, string? TransactionNo, DateTime Checkout);

public record CancelRoomRequest(int BookingId, List<int> SelectedRoomIds, DateTime Checkout);

public record CancelCompleteRoomRequest(
    int BookingId, List<int> SelectedRoomIds, decimal Refund, decimal Payment, DateTime Checkout,
    int PaymentTypeId, int PaymentSubTypeId, string? TransactionNo);

public record PartialCheckoutRequest(int BookingId, List<int> SelectedRoomIds, DateTime Checkout);

public record AddAdditionalRoomRequest(
    int BookingId, decimal UpdateTotalAmount, decimal TotalAdvance, decimal AddAdvance,
    DateTime BookingDate, int PaymentTypeId, int PaymentSubTypeId, string? TransactionNo, List<int> SelectedRoomIds);

public record SaveRoomDetailsRequest(
    string RoomNo, int RoomTypeId, int FloorId, int RoomStatusId, int BookingId, decimal Price,
    int Beds, int Allowed, decimal AdvanceAmount);

public record ModifyRoomDetailsRequest(
    int RoomId, string RoomNo, int RoomTypeId, int FloorId, int RoomStatusId, int BookingId, decimal Price,
    int Beds, int Allowed, decimal AdvanceAmount);

public record RoomCounts(int Available, int Occupied);

/// <summary>Ported from ABPS_WPF's Models/ViewModel/DonarBondVM.cs — donor + donation +
/// remaining-annual-room-day-allowance lookup by Bond No, used by Book Room's Donar category flow.</summary>
public record DonarBondVM(
    int DonarId, string? Name, string? TeluguName, string? PhoneNo, string? Aadhaar, string? Address,
    string? BondNo, int RoomDays, decimal DonationAmount, DateTime DonationDate, string? DonationType, int UsedDays);

/// <summary>Slim projection of ABPS_WPF's RoomController.GetCustomerByAadhar — repeat-guest
/// autofill (most recent booking's contact details for a given Aadhaar number).</summary>
public record CustomerLookupResult(string? Name, string? PhoneNo, string? Address, string? AadharNo);

/// <summary>Ported from ABPS_WPF's Controllers/RoomController.cs, in full.</summary>
public interface IRoomService
{
    Task<List<RoomVM>> GetAllRoomsAsync();
    Task<List<RoomVM>> GetAllDormLockerRoomsAsync();
    Task<RoomCounts> GetRoomCountsAsync();
    Task<RoomCounts> GetDormLockerRoomCountsAsync();
    Task<DonarBondVM?> GetDonarByBondNoAsync(string bondNo);
    Task<CustomerLookupResult?> GetCustomerByAadharAsync(string aadhaar);
    Task<List<BookingCategory>> GetCategoriesAsync();
    Task<SaveBookingResult> SaveBookingAsync(BookingRequest request);
    Task<OperationResult> SaveRoomDetailsAsync(SaveRoomDetailsRequest request);
    Task<OperationResult> ModifyRoomDetailsAsync(ModifyRoomDetailsRequest request);

    Task<int> GetBookingIdByRoomNoAsync(string roomNo);
    Task<bool> ValidateActiveBookingAsync(int bookingId);
    Task<BookingSummary?> GetBookingDetailsAsync(int bookingId);
    Task<List<BookingRoomLine>> GetBookingRoomsAsync(int bookingId);

    Task<OperationResult> VacateRoomAsync(VacateRoomRequest request);
    Task<OperationResult> ExtendRoomAsync(ExtendRoomRequest request);
    Task<OperationResult> ShiftRoomAsync(ShiftRoomRequest request);
    Task<OperationResult> CancelRoomAsync(CancelRoomRequest request);
    Task<OperationResult> CancelCompleteRoomAsync(CancelCompleteRoomRequest request);
    Task<OperationResult> PartialCheckoutAsync(PartialCheckoutRequest request);
    Task<OperationResult> AddAdditionalRoomAsync(AddAdditionalRoomRequest request);

    Task SaveBookingDocumentAsync(int bookingId, string documentType, string? fileName, string? contentType, byte[] fileData);
}

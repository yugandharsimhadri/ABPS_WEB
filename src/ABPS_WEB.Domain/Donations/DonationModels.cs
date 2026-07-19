namespace ABPS_WEB.Domain.Donations;

public record DonorDto(int DonarId, int DonarTypeId, string? Name, string? TeluguName, string? Gothram,
    string? PhoneNo, string? EmailId, string? Pan, string? Aadhaar, string? Address,
    int DistrictId, int StateId, int CountryId);

public record DonationDto(int DonationId, int DonarId, int DonationTypeId, decimal DonationAmount,
    DateTime? DonationDate, int? PaymentTypeId, int? PaymentSubTypeId, string? TransactionNo,
    string? BondNo, int RoomDays);

public record DonationOccasionDto(int DonationOccationId, int DonarId, int DonationId, int? OcassionId,
    string OcassionName, string? IntheNameof, string? TeluguIntheNameof, DateOnly? AnandaanamDate);

/// <summary>Donor identity/contact fields — same shape across Donation Entry, Historical
/// Donations, and both "existing donor" variants in ABPS_WPF's DonationController.</summary>
public record DonorInput(int DonationTypeId, string? Name, string? TeluguName, string? PhoneNo,
    string? Address, string? Gothram, string? Pan, string? Aadhaar, int DistrictId, int StateId, int CountryId);

/// <summary>Telugu/English calendar occasion fields (marriage day, birthday, in-memory-of, etc.)
/// — same shape ABPS_WPF's DonationEntryWindow/HistoricalDonationsWindow both capture.</summary>
public record DonationOccasionInput(int OccasionId, string? Others, string? IntheNameOf, string? TeluguIntheNameOf,
    string AnandanamType, DateTime AnnadanamDate, int TMasamId, int TPakshamId, int TThiduluId, string? DateEnglishTelugu);

public record DonationAmountInput(double Amount, int PaymentTypeId, int PaymentSubTypeId,
    string? TransactionNo, string? BondNo, DateTime DonationDate, int RoomDays);

public record SaveDonationRequest(DonorInput Donor, DonationOccasionInput Occasion, DonationAmountInput Donation,
    int TransactionCategory, string OccasionName, int TransactionTypeId);

public record SaveHistoricalDonationRequest(DonorInput Donor, DonationOccasionInput Occasion,
    DonationAmountInput Donation, string OccasionName);

public record UpdateHistoricalDonationRequest(string BondNo, DonorInput Donor, DonationOccasionInput Occasion,
    DonationAmountInput Donation, string OccasionName);

public record SaveDonationForExistingDonorRequest(int ExistingDonarId, int DonationTypeId, DonationOccasionInput Occasion,
    DonationAmountInput Donation, int TransactionCategory, string OccasionName, int TransactionTypeId);

public record SaveHistoricalDonationForExistingDonorRequest(int ExistingDonarId, int DonationTypeId, DonationOccasionInput Occasion,
    DonationAmountInput Donation, string OccasionName);

public record DonationOperationResult(bool Success, string Message);

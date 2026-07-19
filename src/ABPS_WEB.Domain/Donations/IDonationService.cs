namespace ABPS_WEB.Domain.Donations;

/// <summary>Ported from ABPS_WPF's Controllers/DonationController.cs, in full.</summary>
public interface IDonationService
{
    Task<List<DonorDto>> GetDonorDetailsByIdAsync(int donarId);
    Task<List<DonationDto>> GetDonationDetailsByBondNoAsync(string bondNo);
    Task<List<DonationOccasionDto>> GetDonationOccasionDetailsByBondNoAsync(string bondNo);
    Task<List<DonorDto>> SearchDonorsAsync(string searchTerm, string searchType);
    Task<bool> CheckIfBondNoExistsAsync(string bondNo);

    Task<DonationOperationResult> SaveDonationAsync(SaveDonationRequest request);
    Task<DonationOperationResult> SaveHistoricalDonationAsync(SaveHistoricalDonationRequest request);
    Task<DonationOperationResult> UpdateHistoricalDonationAsync(UpdateHistoricalDonationRequest request);
    Task<DonationOperationResult> SaveDonationForExistingDonorAsync(SaveDonationForExistingDonorRequest request);
    Task<DonationOperationResult> SaveHistoricalDonationForExistingDonorAsync(SaveHistoricalDonationForExistingDonorRequest request);
}

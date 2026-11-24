using ABPS_WEB.Domain.Auth;
using ABPS_WEB.Domain.Donations;
using ABPS_WEB.Infrastructure.Data;
using ABPS_WEB.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ABPS_WEB.Infrastructure.Donations;

/// <summary>Ported from ABPS_WPF's Controllers/DonationController.cs, in full — same 5 lookup
/// queries and 5 save/update operations, same transaction boundaries.</summary>
public class DonationService : IDonationService
{
    private readonly AbpsContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly ILogger<DonationService> _logger;

    public DonationService(AbpsContext context, ICurrentUserService currentUser, ILogger<DonationService> logger)
    {
        _context = context;
        _currentUser = currentUser;
        _logger = logger;
    }

    private static DonorDto ToDto(TblDonar d) => new(d.DonarId, d.DonarTypeId, d.Name, d.TeluguName, d.Gotram,
        d.PhoneNo, d.EmailId, d.Pan, d.Aadhaar, d.Address, d.DistrictId, d.StateId, d.CountryId);

    private static DonationDto ToDto(TblDonation d) => new(d.DonationId, d.DonarId, d.DonationTypeId,
        d.DonationAmount, d.DonationDate, d.PaymentTypeId, d.PaymentSubTypeId, d.TransactionNo, d.BondNo, d.RoomDays);

    private static DonationOccasionDto ToDto(TblDonationOccation o) => new(o.DonationOccationId, o.DonarId,
        o.DonationId, o.OcassionId, o.OcassionName, o.IntheNameof, o.TeluguIntheNameof, o.AnandaanamDate);

    public async Task<List<DonorDto>> GetDonorDetailsByIdAsync(int donarId)
    {
        try
        {
            var donors = await _context.TblDonars.Where(d => d.DonarId == donarId).ToListAsync();
            return donors.Select(ToDto).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetDonorDetailsById");
            return new List<DonorDto>();
        }
    }

    public async Task<List<DonationDto>> GetDonationDetailsByBondNoAsync(string bondNo)
    {
        try
        {
            var donations = await _context.TblDonations.Where(d => d.BondNo.ToLower() == bondNo.ToLower()).ToListAsync();
            return donations.Select(ToDto).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetDonationDetailsByBondNo");
            return new List<DonationDto>();
        }
    }

    public async Task<List<DonationOccasionDto>> GetDonationOccasionDetailsByBondNoAsync(string bondNo)
    {
        try
        {
            var occasions = await _context.TblDonationOccations.Where(o => o.BondNo.ToLower() == bondNo.ToLower()).ToListAsync();
            return occasions.Select(ToDto).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetDonationOccasionDetailsByBondNo");
            return new List<DonationOccasionDto>();
        }
    }

    public async Task<List<DonorDto>> SearchDonorsAsync(string searchTerm, string searchType)
    {
        try
        {
            string term = searchTerm.Trim();
            // Lower-cased: SQL Server's collation was case-insensitive; PostgreSQL's is not.
            string termLower = term.ToLower();
            IQueryable<TblDonar> query = _context.TblDonars;
            query = searchType switch
            {
                "Name" => query.Where(d => d.Name != null && d.Name.ToLower().Contains(termLower)),
                "Phone" => query.Where(d => d.PhoneNo != null && d.PhoneNo.Contains(term)),
                "Aadhar" => query.Where(d => d.Aadhaar != null && d.Aadhaar.Contains(term)),
                "PAN" => query.Where(d => d.Pan != null && d.Pan.ToLower().Contains(termLower)),
                _ => query.Where(d => d.Name != null && d.Name.ToLower().Contains(termLower)),
            };
            var results = await query.OrderByDescending(d => d.DonarId).Take(50).ToListAsync();
            return results.Select(ToDto).ToList();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SearchDonors");
            return new List<DonorDto>();
        }
    }

    public async Task<bool> CheckIfBondNoExistsAsync(string bondNo)
    {
        try
        {
            return await _context.TblDonations.AnyAsync(d => d.BondNo.ToLower() == bondNo.ToLower());
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in CheckIfBondNoExists");
            return false;
        }
    }

    public async Task<DonationOperationResult> SaveDonationAsync(SaveDonationRequest req)
    {
        _logger.LogInformation("SaveDonation → BondNo={BondNo} Amount={Amount}", req.Donation.BondNo, req.Donation.Amount);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var donar = new TblDonar
                {
                    DonarTypeId = req.Donor.DonationTypeId,
                    Name = req.Donor.Name,
                    TeluguName = req.Donor.TeluguName,
                    PhoneNo = req.Donor.PhoneNo,
                    Address = req.Donor.Address,
                    Gotram = req.Donor.Gothram,
                    Pan = req.Donor.Pan,
                    Aadhaar = req.Donor.Aadhaar,
                    DistrictId = req.Donor.DistrictId,
                    StateId = req.Donor.StateId,
                    CountryId = req.Donor.CountryId
                };
                _context.TblDonars.Add(donar);
                await _context.SaveChangesAsync();

                var donation = new TblDonation
                {
                    DonarId = donar.DonarId,
                    DonationTypeId = req.Donor.DonationTypeId,
                    DonationAmount = (decimal)req.Donation.Amount,
                    DonationDate = req.Donation.DonationDate,
                    PaymentTypeId = req.Donation.PaymentTypeId,
                    PaymentSubTypeId = req.Donation.PaymentSubTypeId,
                    TransactionNo = req.Donation.TransactionNo,
                    BondNo = req.Donation.BondNo,
                    RoomDays = req.Donation.RoomDays,
                    CreatedBy = _currentUser.UserName
                };
                _context.TblDonations.Add(donation);
                await _context.SaveChangesAsync();

                _context.TblDonationOccations.Add(new TblDonationOccation
                {
                    DonarId = donar.DonarId,
                    DonationId = donation.DonationId,
                    OcassionId = req.Occasion.OccasionId,
                    OcassionName = req.OccasionName,
                    IntheNameof = req.Occasion.IntheNameOf,
                    TeluguIntheNameof = req.Occasion.TeluguIntheNameOf,
                    AnandaanamDateType = req.Occasion.AnandanamType,
                    AnandaanamDate = DateOnly.FromDateTime(req.Occasion.AnnadanamDate),
                    TMasamId = req.Occasion.TMasamId,
                    TPaskshamId = req.Occasion.TPakshamId,
                    TThiduluId = req.Occasion.TThiduluId,
                    BondNo = req.Donation.BondNo,
                    CreatedBy = _currentUser.UserName,
                    DateEnglishTelugu = req.Occasion.DateEnglishTelugu
                });

                _context.TblTransactions.Add(new TblTransaction
                {
                    TransactionCategoryId = req.TransactionCategory,
                    DonarBookingId = donar.DonarId,
                    TrasactionTypeId = req.TransactionTypeId,
                    TransactionDate = req.Donation.DonationDate,
                    Amount = (decimal)req.Donation.Amount,
                    PaymentTypeId = req.Donation.PaymentTypeId,
                    PaymentSubTypeId = req.Donation.PaymentSubTypeId,
                    ReferenceNumber = req.Donation.TransactionNo,
                    Notes = $"Donation for {req.Occasion.IntheNameOf} ({req.OccasionName})",
                    CreatedBy = _currentUser.UserName,
                    BondNo = req.Donation.BondNo
                });

                await _context.SaveChangesAsync();
                await tx.CommitAsync();
                return new DonationOperationResult(true, "Success");
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error in SaveDonation transaction");
                return new DonationOperationResult(false, "Error: " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SaveDonation");
            return new DonationOperationResult(false, "Error: " + ex.Message);
        }
    }

    public async Task<DonationOperationResult> SaveHistoricalDonationAsync(SaveHistoricalDonationRequest req)
    {
        _logger.LogInformation("SaveHistoricalDonation → BondNo={BondNo} Amount={Amount}", req.Donation.BondNo, req.Donation.Amount);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var donar = new TblDonar
                {
                    DonarTypeId = req.Donor.DonationTypeId,
                    Name = req.Donor.Name,
                    TeluguName = req.Donor.TeluguName,
                    PhoneNo = req.Donor.PhoneNo,
                    Address = req.Donor.Address,
                    Gotram = req.Donor.Gothram,
                    Pan = req.Donor.Pan,
                    Aadhaar = req.Donor.Aadhaar,
                    DistrictId = req.Donor.DistrictId,
                    StateId = req.Donor.StateId,
                    CountryId = req.Donor.CountryId
                };
                _context.TblDonars.Add(donar);
                await _context.SaveChangesAsync();

                var donation = new TblDonation
                {
                    DonarId = donar.DonarId,
                    DonationTypeId = req.Donor.DonationTypeId,
                    DonationAmount = (decimal)req.Donation.Amount,
                    DonationDate = req.Donation.DonationDate,
                    PaymentTypeId = req.Donation.PaymentTypeId,
                    PaymentSubTypeId = req.Donation.PaymentSubTypeId,
                    TransactionNo = req.Donation.TransactionNo,
                    BondNo = req.Donation.BondNo,
                    RoomDays = req.Donation.RoomDays,
                    CreatedBy = _currentUser.UserName
                };
                _context.TblDonations.Add(donation);
                await _context.SaveChangesAsync();

                _context.TblDonationOccations.Add(new TblDonationOccation
                {
                    DonarId = donar.DonarId,
                    DonationId = donation.DonationId,
                    OcassionId = req.Occasion.OccasionId,
                    OcassionName = req.OccasionName,
                    IntheNameof = req.Occasion.IntheNameOf,
                    TeluguIntheNameof = req.Occasion.TeluguIntheNameOf,
                    AnandaanamDateType = req.Occasion.AnandanamType,
                    AnandaanamDate = DateOnly.FromDateTime(req.Occasion.AnnadanamDate),
                    TMasamId = req.Occasion.TMasamId,
                    TPaskshamId = req.Occasion.TPakshamId,
                    TThiduluId = req.Occasion.TThiduluId,
                    BondNo = req.Donation.BondNo,
                    CreatedBy = _currentUser.UserName,
                    DateEnglishTelugu = req.Occasion.DateEnglishTelugu
                });

                // No tbl_Transaction row — Historical Donations records donor+donation+occasion
                // only, matching ABPS_WPF's SaveHistoricalDonationDetails exactly (no transaction
                // insert there, unlike the live Donation Entry flow's SaveDonationDetails).
                await _context.SaveChangesAsync();
                await tx.CommitAsync();
                return new DonationOperationResult(true, "Success");
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error in SaveHistoricalDonation transaction");
                return new DonationOperationResult(false, "Error: " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SaveHistoricalDonation");
            return new DonationOperationResult(false, "Error: " + ex.Message);
        }
    }

    public async Task<DonationOperationResult> UpdateHistoricalDonationAsync(UpdateHistoricalDonationRequest req)
    {
        _logger.LogInformation("UpdateHistoricalDonation → BondNo={BondNo}", req.BondNo);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var existingDonation = await _context.TblDonations.FirstOrDefaultAsync(d => d.BondNo.ToLower() == req.BondNo.ToLower());
                if (existingDonation == null)
                {
                    await tx.RollbackAsync();
                    return new DonationOperationResult(false, "Error: Donation not found for the provided BondNo.");
                }

                var existingOccasion = await _context.TblDonationOccations.FirstOrDefaultAsync(o => o.BondNo.ToLower() == req.BondNo.ToLower());
                if (existingOccasion == null)
                {
                    await tx.RollbackAsync();
                    return new DonationOperationResult(false, "Error: Donation occasion not found for the provided BondNo.");
                }

                var existingDonar = await _context.TblDonars.FirstOrDefaultAsync(d => d.DonarId == existingDonation.DonarId);
                if (existingDonar == null)
                {
                    await tx.RollbackAsync();
                    return new DonationOperationResult(false, "Error: Donar not found for the existing donation.");
                }

                existingDonation.BondNo = req.BondNo;
                existingDonation.DonationAmount = (decimal)req.Donation.Amount;
                existingDonation.DonationDate = req.Donation.DonationDate;
                existingDonation.PaymentTypeId = req.Donation.PaymentTypeId;
                existingDonation.PaymentSubTypeId = req.Donation.PaymentSubTypeId;
                existingDonation.DonationTypeId = req.Donor.DonationTypeId;
                existingDonation.RoomDays = req.Donation.RoomDays;

                existingOccasion.OcassionId = req.Occasion.OccasionId;
                existingOccasion.OcassionName = req.OccasionName;
                existingOccasion.IntheNameof = req.Occasion.IntheNameOf;
                existingOccasion.TeluguIntheNameof = req.Occasion.TeluguIntheNameOf;
                existingOccasion.AnandaanamDateType = req.Occasion.AnandanamType;
                existingOccasion.AnandaanamDate = DateOnly.FromDateTime(req.Occasion.AnnadanamDate);
                existingOccasion.TMasamId = req.Occasion.TMasamId;
                existingOccasion.TPaskshamId = req.Occasion.TPakshamId;
                existingOccasion.TThiduluId = req.Occasion.TThiduluId;
                existingOccasion.DateEnglishTelugu = req.Occasion.DateEnglishTelugu;

                existingDonar.DonarTypeId = req.Donor.DonationTypeId;
                existingDonar.Name = req.Donor.Name;
                existingDonar.TeluguName = req.Donor.TeluguName;
                existingDonar.PhoneNo = req.Donor.PhoneNo;
                existingDonar.Address = req.Donor.Address;
                existingDonar.Gotram = req.Donor.Gothram;
                existingDonar.Pan = req.Donor.Pan;
                existingDonar.Aadhaar = req.Donor.Aadhaar;
                existingDonar.DistrictId = req.Donor.DistrictId;
                existingDonar.StateId = req.Donor.StateId;
                existingDonar.CountryId = req.Donor.CountryId;

                await _context.SaveChangesAsync();
                await tx.CommitAsync();
                return new DonationOperationResult(true, "Success");
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error in UpdateHistoricalDonation transaction");
                return new DonationOperationResult(false, "Error: " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in UpdateHistoricalDonation");
            return new DonationOperationResult(false, "Error: " + ex.Message);
        }
    }

    public async Task<DonationOperationResult> SaveDonationForExistingDonorAsync(SaveDonationForExistingDonorRequest req)
    {
        _logger.LogInformation("SaveDonationForExistingDonor → DonarId={DonarId} BondNo={BondNo}", req.ExistingDonarId, req.Donation.BondNo);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var donation = new TblDonation
                {
                    DonarId = req.ExistingDonarId,
                    DonationTypeId = req.DonationTypeId,
                    DonationAmount = (decimal)req.Donation.Amount,
                    DonationDate = req.Donation.DonationDate,
                    PaymentTypeId = req.Donation.PaymentTypeId,
                    PaymentSubTypeId = req.Donation.PaymentSubTypeId,
                    TransactionNo = req.Donation.TransactionNo,
                    BondNo = req.Donation.BondNo,
                    RoomDays = req.Donation.RoomDays,
                    CreatedBy = _currentUser.UserName
                };
                _context.TblDonations.Add(donation);
                await _context.SaveChangesAsync();

                _context.TblDonationOccations.Add(new TblDonationOccation
                {
                    DonarId = req.ExistingDonarId,
                    DonationId = donation.DonationId,
                    OcassionId = req.Occasion.OccasionId,
                    OcassionName = req.OccasionName,
                    IntheNameof = req.Occasion.IntheNameOf,
                    TeluguIntheNameof = req.Occasion.TeluguIntheNameOf,
                    AnandaanamDateType = req.Occasion.AnandanamType,
                    AnandaanamDate = DateOnly.FromDateTime(req.Occasion.AnnadanamDate),
                    TMasamId = req.Occasion.TMasamId,
                    TPaskshamId = req.Occasion.TPakshamId,
                    TThiduluId = req.Occasion.TThiduluId,
                    BondNo = req.Donation.BondNo,
                    CreatedBy = _currentUser.UserName,
                    DateEnglishTelugu = req.Occasion.DateEnglishTelugu
                });

                _context.TblTransactions.Add(new TblTransaction
                {
                    TransactionCategoryId = req.TransactionCategory,
                    DonarBookingId = req.ExistingDonarId,
                    TrasactionTypeId = req.TransactionTypeId,
                    TransactionDate = req.Donation.DonationDate,
                    Amount = (decimal)req.Donation.Amount,
                    PaymentTypeId = req.Donation.PaymentTypeId,
                    PaymentSubTypeId = req.Donation.PaymentSubTypeId,
                    ReferenceNumber = req.Donation.TransactionNo,
                    Notes = $"Donation for {req.Occasion.IntheNameOf} ({req.OccasionName})",
                    CreatedBy = _currentUser.UserName,
                    BondNo = req.Donation.BondNo
                });

                await _context.SaveChangesAsync();
                await tx.CommitAsync();
                return new DonationOperationResult(true, "Success");
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error in SaveDonationForExistingDonor transaction");
                return new DonationOperationResult(false, "Error: " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SaveDonationForExistingDonor");
            return new DonationOperationResult(false, "Error: " + ex.Message);
        }
    }

    public async Task<DonationOperationResult> SaveHistoricalDonationForExistingDonorAsync(SaveHistoricalDonationForExistingDonorRequest req)
    {
        _logger.LogInformation("SaveHistoricalDonationForExistingDonor → DonarId={DonarId} BondNo={BondNo}", req.ExistingDonarId, req.Donation.BondNo);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var donation = new TblDonation
                {
                    DonarId = req.ExistingDonarId,
                    DonationTypeId = req.DonationTypeId,
                    DonationAmount = (decimal)req.Donation.Amount,
                    DonationDate = req.Donation.DonationDate,
                    PaymentTypeId = req.Donation.PaymentTypeId,
                    PaymentSubTypeId = req.Donation.PaymentSubTypeId,
                    TransactionNo = req.Donation.TransactionNo,
                    BondNo = req.Donation.BondNo,
                    RoomDays = req.Donation.RoomDays,
                    CreatedBy = _currentUser.UserName
                };
                _context.TblDonations.Add(donation);
                await _context.SaveChangesAsync();

                _context.TblDonationOccations.Add(new TblDonationOccation
                {
                    DonarId = req.ExistingDonarId,
                    DonationId = donation.DonationId,
                    OcassionId = req.Occasion.OccasionId,
                    OcassionName = req.OccasionName,
                    IntheNameof = req.Occasion.IntheNameOf,
                    TeluguIntheNameof = req.Occasion.TeluguIntheNameOf,
                    AnandaanamDateType = req.Occasion.AnandanamType,
                    AnandaanamDate = DateOnly.FromDateTime(req.Occasion.AnnadanamDate),
                    TMasamId = req.Occasion.TMasamId,
                    TPaskshamId = req.Occasion.TPakshamId,
                    TThiduluId = req.Occasion.TThiduluId,
                    BondNo = req.Donation.BondNo,
                    CreatedBy = _currentUser.UserName,
                    DateEnglishTelugu = req.Occasion.DateEnglishTelugu
                });

                await _context.SaveChangesAsync();
                await tx.CommitAsync();
                return new DonationOperationResult(true, "Success");
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error in SaveHistoricalDonationForExistingDonor transaction");
                return new DonationOperationResult(false, "Error: " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in SaveHistoricalDonationForExistingDonor");
            return new DonationOperationResult(false, "Error: " + ex.Message);
        }
    }
}

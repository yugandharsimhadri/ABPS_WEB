using ABPS_WEB.Domain.Donations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABPS_WEB.Api.Controllers;

/// <summary>Ported from ABPS_WPF's DonationController — Donation Entry + Historical
/// Donations screens (Send Greetings/WhatsApp gets its own NotificationsController).</summary>
[ApiController]
[Authorize]
[Route("api/donations")]
public class DonationsController : ControllerBase
{
    private readonly IDonationService _donations;

    public DonationsController(IDonationService donations) => _donations = donations;

    [HttpGet("donors/{donarId}")]
    public async Task<ActionResult<List<DonorDto>>> GetDonorById(int donarId) => Ok(await _donations.GetDonorDetailsByIdAsync(donarId));

    [HttpGet("by-bond-no/{bondNo}")]
    public async Task<ActionResult<List<DonationDto>>> GetByBondNo(string bondNo) => Ok(await _donations.GetDonationDetailsByBondNoAsync(bondNo));

    [HttpGet("occasions/by-bond-no/{bondNo}")]
    public async Task<ActionResult<List<DonationOccasionDto>>> GetOccasionsByBondNo(string bondNo) => Ok(await _donations.GetDonationOccasionDetailsByBondNoAsync(bondNo));

    [HttpGet("donors/search")]
    public async Task<ActionResult<List<DonorDto>>> SearchDonors([FromQuery] string term, [FromQuery] string type) =>
        Ok(await _donations.SearchDonorsAsync(term, type));

    [HttpGet("bond-no/{bondNo}/exists")]
    public async Task<ActionResult<bool>> BondNoExists(string bondNo) => Ok(await _donations.CheckIfBondNoExistsAsync(bondNo));

    [HttpPost]
    public async Task<IActionResult> Save(SaveDonationRequest request) => FromResult(await _donations.SaveDonationAsync(request));

    [HttpPost("historical")]
    public async Task<IActionResult> SaveHistorical(SaveHistoricalDonationRequest request) => FromResult(await _donations.SaveHistoricalDonationAsync(request));

    [HttpPut("historical")]
    public async Task<IActionResult> UpdateHistorical(UpdateHistoricalDonationRequest request) => FromResult(await _donations.UpdateHistoricalDonationAsync(request));

    [HttpPost("existing-donor")]
    public async Task<IActionResult> SaveForExistingDonor(SaveDonationForExistingDonorRequest request) => FromResult(await _donations.SaveDonationForExistingDonorAsync(request));

    [HttpPost("historical/existing-donor")]
    public async Task<IActionResult> SaveHistoricalForExistingDonor(SaveHistoricalDonationForExistingDonorRequest request) => FromResult(await _donations.SaveHistoricalDonationForExistingDonorAsync(request));

    private IActionResult FromResult(DonationOperationResult result) =>
        result.Success ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
}

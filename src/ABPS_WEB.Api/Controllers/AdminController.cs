using ABPS_WEB.Domain.Admin;
using ABPS_WEB.Domain.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ABPS_WEB.Api.Controllers;

/// <summary>Ported from ABPS_WPF's Controllers/AdminController.cs, in full — Members (Executive
/// Board), User Registration, Admin re-auth, and Donor Type CRUD (see IAdminService for the
/// note on where that last piece actually comes from in ABPS_WPF).</summary>
[ApiController]
[Authorize]
[Route("api/admin")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _admin;

    public AdminController(IAdminService admin) => _admin = admin;

    [HttpGet("members")]
    public async Task<ActionResult<List<ExecutiveMemberReportVM>>> GetMembers([FromQuery] DateTime fromDate, [FromQuery] DateTime toDate, [FromQuery] string? statusId, [FromQuery] int selectedIndex = 0)
    {
        var result = statusId == null
            ? await _admin.GetExecutiveMembersAsync(fromDate, toDate)
            : await _admin.GetExecutiveMembersAsync(fromDate, toDate, statusId, selectedIndex);
        return Ok(result);
    }

    [HttpGet("members/all")]
    public async Task<ActionResult<List<MemberVM>>> GetAllMembers() => Ok(await _admin.GetAllMembersAsync());

    [HttpPost("members")]
    public async Task<IActionResult> SaveMember(SaveExecutiveMemberRequest request) => FromResult(await _admin.SaveExecutiveMemberAsync(request));

    [HttpPut("members/{memberId}")]
    public async Task<IActionResult> ModifyMember(int memberId, ModifyExecutiveMemberRequest request) =>
        FromResult(await _admin.ModifyExecutiveMemberAsync(request with { MemberId = memberId }));

    [HttpGet("users")]
    public async Task<ActionResult<List<UserVM>>> GetUsers() => Ok(await _admin.GetExistingUsersAsync());

    [HttpGet("users/{userId}")]
    public async Task<ActionResult<UserVM>> GetUser(int userId)
    {
        var user = await _admin.GetUserByIdAsync(userId);
        return user == null ? NotFound() : Ok(user);
    }

    [HttpGet("users/exists")]
    public async Task<ActionResult<bool>> UserExists([FromQuery] string username) => Ok(await _admin.CheckIfUserExistsAsync(username));

    [HttpPost("users")]
    public async Task<IActionResult> AddUser(SaveUserRequest request) => FromResult(await _admin.AddUserAsync(request));

    [HttpPut("users/{userId}")]
    public async Task<IActionResult> ModifyUser(int userId, ModifyUserRequest request) =>
        FromResult(await _admin.ModifyUserAsync(request with { UserId = userId }));

    [HttpPost("validate")]
    public async Task<IActionResult> ValidateAdminCredentials([FromBody] AdminCredentials credentials) =>
        FromResult(await _admin.ValidateAdminCredentialsAsync(credentials.Username, credentials.Password));

    [HttpGet("donor-types")]
    public async Task<ActionResult<List<DonorTypeVM>>> GetDonorTypes() => Ok(await _admin.GetDonorTypesAsync());

    [HttpPost("donor-types")]
    public async Task<IActionResult> SaveDonorType(SaveDonorTypeRequest request) => FromResult(await _admin.SaveDonorTypeAsync(request));

    [HttpPut("donor-types/{dtid}")]
    public async Task<IActionResult> ModifyDonorType(int dtid, ModifyDonorTypeRequest request) =>
        FromResult(await _admin.ModifyDonorTypeAsync(request with { Dtid = dtid }));

    private IActionResult FromResult(ABPS_WEB.Domain.RoomOps.OperationResult result) =>
        result.Success ? Ok(new { message = result.Message }) : BadRequest(new { message = result.Message });
}

public record AdminCredentials(string Username, string Password);

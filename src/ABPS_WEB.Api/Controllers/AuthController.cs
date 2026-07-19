using ABPS_WEB.Api.Auth;
using ABPS_WEB.Api.Contracts;
using ABPS_WEB.Domain.Auth;
using Microsoft.AspNetCore.Mvc;

namespace ABPS_WEB.Api.Controllers;

/// <summary>Ported from ABPS_WPF's MainWindow.xaml.cs BtnLogin_Click — same validation order,
/// same lockout messages, same 5-attempts/15-minute lockout. JWT issuance replaces
/// GlobalVariables.StartSession (a desktop-process-wide static) since a web API is stateless
/// between requests.</summary>
[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly LoginLockoutTracker _lockout;
    private readonly JwtTokenService _jwt;

    public AuthController(IAuthService authService, LoginLockoutTracker lockout, JwtTokenService jwt)
    {
        _authService = authService;
        _lockout = lockout;
        _jwt = jwt;
    }

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
    {
        var username = request.Username?.Trim() ?? "";
        var password = request.Password ?? "";

        if (username.Length == 0 || password.Length == 0)
            return BadRequest(new { message = "Enter a user name and password." });

        var (isLocked, minutesLeft) = _lockout.CheckLockout(username);
        if (isLocked)
            return StatusCode(423, new { message = $"Account locked. Try again in {minutesLeft} minute(s)." });

        var result = await _authService.ValidateUserAndGetRoleAsync(username, password);

        if (result.Valid)
        {
            _lockout.RecordSuccess(username);
            var token = _jwt.CreateToken(result.UserName!, result.RoleId!.Value);
            var roleName = Enum.IsDefined(typeof(UserRoles), result.RoleId!.Value)
                ? ((UserRoles)result.RoleId!.Value).ToString()
                : "Unknown";
            return Ok(new LoginResponse(token, result.UserName!, result.RoleId!.Value, roleName));
        }

        bool triggeredLockout = _lockout.RecordFailure(username);
        return Unauthorized(new
        {
            message = triggeredLockout
                ? "Too many failed attempts. Account locked for 15 minutes."
                : "Invalid user name or password."
        });
    }
}

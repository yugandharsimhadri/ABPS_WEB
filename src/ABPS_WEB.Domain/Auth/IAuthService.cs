namespace ABPS_WEB.Domain.Auth;

public record AuthResult(bool Valid, string? UserName, int? RoleId);

/// <summary>Ported from ABPS_WPF's AdminController.ValidateUserAndGetRole — same DB lookup
/// and credential check, now with PBKDF2 verification/lazy-upgrade instead of raw SHA-256.</summary>
public interface IAuthService
{
    Task<AuthResult> ValidateUserAndGetRoleAsync(string username, string password);
}

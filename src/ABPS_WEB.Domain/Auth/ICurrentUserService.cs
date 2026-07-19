namespace ABPS_WEB.Domain.Auth;

/// <summary>Replaces ABPS_WPF's GlobalVariables.CurrentUser (a process-wide static set once at
/// login) — a stateless API has no such thing, so CreatedBy/UpdatedBy stamping instead reads
/// the authenticated request's JWT identity per call. Implemented in the Api project (where
/// HttpContext lives) so Domain/Infrastructure stay free of ASP.NET Core dependencies.</summary>
public interface ICurrentUserService
{
    string? UserName { get; }
}

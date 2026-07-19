namespace ABPS_WEB.Api.Contracts;

public record LoginRequest(string Username, string Password);

public record LoginResponse(string Token, string Username, int RoleId, string RoleName);

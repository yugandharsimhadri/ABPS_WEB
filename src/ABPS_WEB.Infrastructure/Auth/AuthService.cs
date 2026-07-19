using ABPS_WEB.Domain.Auth;
using ABPS_WEB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ABPS_WEB.Infrastructure.Auth;

public class AuthService : IAuthService
{
    private readonly AbpsContext _context;

    public AuthService(AbpsContext context) => _context = context;

    public async Task<AuthResult> ValidateUserAndGetRoleAsync(string username, string password)
    {
        var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.UserName == username);
        if (user == null) return new AuthResult(false, null, null);

        var (valid, needsUpgrade) = PasswordHasher.Verify(password, user.PasswordHash);
        if (!valid) return new AuthResult(false, null, null);

        if (needsUpgrade)
        {
            user.PasswordHash = PasswordHasher.Hash(password);
            await _context.SaveChangesAsync();
        }

        return new AuthResult(true, user.UserName, user.RoleId);
    }
}

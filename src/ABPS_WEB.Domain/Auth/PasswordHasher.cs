using System.Security.Cryptography;
using System.Text;

namespace ABPS_WEB.Domain.Auth;

/// <summary>
/// Replaces ABPS_WPF's AdminController.HashPassword (unsalted SHA-256 — fine for a desktop
/// app on a controlled machine, not once login is reachable over HTTP). New hashes use
/// salted PBKDF2. Existing tbl_User rows still hold the old SHA-256 hex hash; VerifyAndUpgrade
/// recognizes that format, verifies against it, and signals that the caller should re-hash
/// and save the password in the new format — a lazy, login-triggered migration that needs no
/// password reset and no access to anyone's plaintext password.
/// </summary>
public static class PasswordHasher
{
    private const int Pbkdf2Iterations = 100_000;
    private const int SaltSizeBytes = 16;
    private const int KeySizeBytes = 32;
    private const string Pbkdf2Prefix = "PBKDF2$";

    public static string Hash(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(SaltSizeBytes);
        byte[] key = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password), salt, Pbkdf2Iterations, HashAlgorithmName.SHA256, KeySizeBytes);
        return $"{Pbkdf2Prefix}{Pbkdf2Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(key)}";
    }

    /// <summary>Verifies <paramref name="password"/> against <paramref name="storedHash"/> in
    /// either format. <c>NeedsUpgrade</c> is true only when a legacy SHA-256 hash verified
    /// successfully — the caller should then call <see cref="Hash"/> and persist the result.</summary>
    public static (bool Valid, bool NeedsUpgrade) Verify(string password, string storedHash)
    {
        if (storedHash.StartsWith(Pbkdf2Prefix, StringComparison.Ordinal))
            return (VerifyPbkdf2(password, storedHash), false);

        // Legacy format: lowercase hex SHA-256, unsalted — ABPS_WPF's AdminController.HashPassword.
        bool legacyValid = string.Equals(storedHash, LegacySha256Hex(password), StringComparison.OrdinalIgnoreCase);
        return (legacyValid, legacyValid);
    }

    private static bool VerifyPbkdf2(string password, string storedHash)
    {
        var parts = storedHash.Substring(Pbkdf2Prefix.Length).Split('$');
        if (parts.Length != 3 || !int.TryParse(parts[0], out var iterations)) return false;

        byte[] salt = Convert.FromBase64String(parts[1]);
        byte[] expectedKey = Convert.FromBase64String(parts[2]);
        byte[] actualKey = Rfc2898DeriveBytes.Pbkdf2(
            Encoding.UTF8.GetBytes(password), salt, iterations, HashAlgorithmName.SHA256, expectedKey.Length);

        return CryptographicOperations.FixedTimeEquals(actualKey, expectedKey);
    }

    private static string LegacySha256Hex(string password) =>
        Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes(password))).ToLowerInvariant();
}

using ABPS_WEB.Domain.Auth;
using System.Security.Cryptography;
using System.Text;
using Xunit;

namespace ABPS_WEB.Domain.Tests;

public class PasswordHasherTests
{
    [Fact]
    public void Hash_Then_Verify_Succeeds_WithoutUpgrade()
    {
        var hash = PasswordHasher.Hash("CorrectHorseBatteryStaple");

        var (valid, needsUpgrade) = PasswordHasher.Verify("CorrectHorseBatteryStaple", hash);

        Assert.True(valid);
        Assert.False(needsUpgrade);
    }

    [Fact]
    public void Verify_WrongPassword_Fails()
    {
        var hash = PasswordHasher.Hash("CorrectHorseBatteryStaple");

        var (valid, _) = PasswordHasher.Verify("WrongPassword", hash);

        Assert.False(valid);
    }

    [Fact]
    public void Verify_LegacySha256Hash_Succeeds_AndFlagsUpgrade()
    {
        // Reproduces ABPS_WPF's AdminController.HashPassword exactly (unsalted SHA-256 hex),
        // simulating an existing tbl_User row that predates the PBKDF2 migration.
        var legacyHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("OldPassword123"))).ToLowerInvariant();

        var (valid, needsUpgrade) = PasswordHasher.Verify("OldPassword123", legacyHash);

        Assert.True(valid);
        Assert.True(needsUpgrade);
    }

    [Fact]
    public void Verify_LegacySha256Hash_WrongPassword_Fails_WithoutUpgrade()
    {
        var legacyHash = Convert.ToHexString(SHA256.HashData(Encoding.UTF8.GetBytes("OldPassword123"))).ToLowerInvariant();

        var (valid, needsUpgrade) = PasswordHasher.Verify("WrongGuess", legacyHash);

        Assert.False(valid);
        Assert.False(needsUpgrade);
    }

    [Fact]
    public void Hash_ProducesDifferentSaltEachTime()
    {
        var hash1 = PasswordHasher.Hash("SamePassword");
        var hash2 = PasswordHasher.Hash("SamePassword");

        Assert.NotEqual(hash1, hash2);
        Assert.True(PasswordHasher.Verify("SamePassword", hash1).Valid);
        Assert.True(PasswordHasher.Verify("SamePassword", hash2).Valid);
    }
}

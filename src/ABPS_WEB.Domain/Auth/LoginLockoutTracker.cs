using System.Collections.Concurrent;

namespace ABPS_WEB.Domain.Auth;

/// <summary>
/// Ported from ABPS_WPF's MainWindow.xaml.cs — 5 failed attempts locks the account for 15
/// minutes. That was a static Dictionary scoped to one desktop process; here it's a singleton
/// service with the same in-memory, non-DB-persisted semantics (resets on API restart) —
/// tbl_User has no lockout columns today, so persisting this would be a new behavior, not a
/// port. ConcurrentDictionary replaces the plain Dictionary since a web API handles requests
/// concurrently, unlike the WPF app's single UI thread.
/// </summary>
public class LoginLockoutTracker
{
    public const int MaxFailedAttempts = 5;
    public static readonly TimeSpan LockoutDuration = TimeSpan.FromMinutes(15);

    private readonly ConcurrentDictionary<string, (int Count, DateTime LockedUntil)> _failedAttempts =
        new(StringComparer.OrdinalIgnoreCase);

    public (bool IsLocked, int MinutesLeft) CheckLockout(string username)
    {
        if (_failedAttempts.TryGetValue(username, out var state) && state.LockedUntil > DateTime.UtcNow)
            return (true, (int)Math.Ceiling((state.LockedUntil - DateTime.UtcNow).TotalMinutes));
        return (false, 0);
    }

    public void RecordSuccess(string username) => _failedAttempts.TryRemove(username, out _);

    /// <returns>true if this failure just triggered the lockout.</returns>
    public bool RecordFailure(string username)
    {
        int count = (_failedAttempts.TryGetValue(username, out var prev) ? prev.Count : 0) + 1;
        bool triggeredLockout = count >= MaxFailedAttempts;
        DateTime lockedUntil = triggeredLockout ? DateTime.UtcNow.Add(LockoutDuration) : DateTime.MinValue;
        _failedAttempts[username] = (triggeredLockout ? 0 : count, lockedUntil);
        return triggeredLockout;
    }
}

namespace ABPS_WEB.Domain.Auth;

// Ported verbatim from ABPS_WPF/Enums/BookingStatus.cs — same numeric values, since
// tbl_User.RoleId in the shared DB already contains these.
public enum UserRoles
{
    Admin = 1,
    User = 2,
    Accountant = 3,
    SuperUser = 4
}

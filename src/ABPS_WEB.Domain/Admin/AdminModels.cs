namespace ABPS_WEB.Domain.Admin;

public record MemberVM(
    int MemberId, string? MemberName, string? MemberTname, string? MemberType, string? Phone, string? Email,
    string? Address, string? IsActive, DateTime? EffectiveFrom, DateTime? EffectiveTo);

public record SaveExecutiveMemberRequest(
    string MemberName, string MemberTname, string Phone, string Email, string Address,
    string MemberType, string IsActive, DateTime EffectiveFrom, DateTime EffectiveTo);

public record ModifyExecutiveMemberRequest(
    int MemberId, string MemberName, string MemberTname, string Phone, string Email, string Address,
    string MemberType, string IsActive, DateTime EffectiveFrom, DateTime EffectiveTo);

public record UserVM(int UserId, string? UserName, string? Email, string? MobileNumber, int RoleId, string? IsActive);

public record SaveUserRequest(string UserName, string Password, int RoleId, string IsActive, string Email, string PhoneNo);

public record ModifyUserRequest(int UserId, string NewPassword, int NewRoleId, string IsActive, string Email, string PhoneNo);

public record DonorTypeVM(int Dtid, string? Donartype, string? Code, int? Amount, int? Days);

public record SaveDonorTypeRequest(string Donartype, string Code, int Amount, int Days);

public record ModifyDonorTypeRequest(int Dtid, string Donartype, string Code, int Amount, int Days);

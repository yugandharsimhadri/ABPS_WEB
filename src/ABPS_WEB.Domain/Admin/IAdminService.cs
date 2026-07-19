using ABPS_WEB.Domain.Reports;
using ABPS_WEB.Domain.RoomOps;

namespace ABPS_WEB.Domain.Admin;

/// <summary>Ported from ABPS_WPF's Controllers/AdminController.cs, in full — Members
/// (Executive Board), User Registration, and Admin re-auth (AdminAuthWindow's gate before
/// entering the Admin screens). Donor Type CRUD is ported here too: ABPS_WPF's
/// DonarTypesWindow.xaml.cs has no dedicated controller and hits AbpsContext directly, so
/// there's no separate source file to mirror — the Save/Modify logic below matches its
/// Save_Click handler exactly.</summary>
public interface IAdminService
{
    Task<List<ExecutiveMemberReportVM>> GetExecutiveMembersAsync(DateTime fromDate, DateTime toDate);
    Task<List<ExecutiveMemberReportVM>> GetExecutiveMembersAsync(DateTime fromDate, DateTime toDate, string statusId, int selectedIndex);

    /// <summary>Ported from ABPS_WPF's MembersWindow.xaml.cs LoadGrid — a direct
    /// TblBoardMembers query (no dedicated controller method), unlike the report-flavored
    /// GetExecutiveMembersAsync overloads above. Used by the Members CRUD screen, which needs
    /// MemberId to know which row it's editing.</summary>
    Task<List<MemberVM>> GetAllMembersAsync();
    Task<OperationResult> ModifyExecutiveMemberAsync(ModifyExecutiveMemberRequest request);
    Task<OperationResult> SaveExecutiveMemberAsync(SaveExecutiveMemberRequest request);

    Task<OperationResult> AddUserAsync(SaveUserRequest request);
    Task<List<UserVM>> GetExistingUsersAsync();
    Task<UserVM?> GetUserByIdAsync(int userId);
    Task<OperationResult> ModifyUserAsync(ModifyUserRequest request);
    Task<bool> CheckIfUserExistsAsync(string username);

    Task<OperationResult> ValidateAdminCredentialsAsync(string username, string password);

    Task<List<DonorTypeVM>> GetDonorTypesAsync();
    Task<OperationResult> SaveDonorTypeAsync(SaveDonorTypeRequest request);
    Task<OperationResult> ModifyDonorTypeAsync(ModifyDonorTypeRequest request);
}

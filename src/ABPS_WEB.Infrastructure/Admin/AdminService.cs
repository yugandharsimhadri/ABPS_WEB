using ABPS_WEB.Domain.Admin;
using ABPS_WEB.Domain.Auth;
using ABPS_WEB.Domain.Reports;
using ABPS_WEB.Domain.RoomOps;
using ABPS_WEB.Infrastructure.Data;
using ABPS_WEB.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ABPS_WEB.Infrastructure.Admin;

public class AdminService : IAdminService
{
    private readonly AbpsContext _context;
    private readonly ICurrentUserService _currentUser;

    public AdminService(AbpsContext context, ICurrentUserService currentUser)
    {
        _context = context;
        _currentUser = currentUser;
    }

    public async Task<List<ExecutiveMemberReportVM>> GetExecutiveMembersAsync(DateTime fromDate, DateTime toDate)
    {
        var from = DateOnly.FromDateTime(fromDate);
        var to = DateOnly.FromDateTime(toDate);
        return await _context.TblBoardMembers
            .Where(x => x.EffectiveFrom >= from && x.EffectiveTo <= to)
            .Select(x => new ExecutiveMemberReportVM
            {
                MemberName = x.MemberName,
                MemberType = x.MemberType,
                Phone = x.Phone,
                Email = x.Email,
                Address = x.Address,
                IsActive = x.IsActive,
                EffectiveFrom = x.EffectiveFrom,
                EffectiveTo = x.EffectiveTo
            }).ToListAsync();
    }

    public async Task<List<ExecutiveMemberReportVM>> GetExecutiveMembersAsync(DateTime fromDate, DateTime toDate, string statusId, int selectedIndex)
    {
        var from = DateOnly.FromDateTime(fromDate);
        var to = DateOnly.FromDateTime(toDate);
        _context.Database.SetCommandTimeout(180);
        return await _context.TblBoardMembers
            .Where(x => x.EffectiveFrom >= from && x.EffectiveTo <= to
                     && (selectedIndex == 0 || x.IsActive == statusId))
            .Select(x => new ExecutiveMemberReportVM
            {
                MemberName = x.MemberName,
                MemberType = x.MemberType,
                Phone = x.Phone,
                Email = x.Email,
                Address = x.Address,
                IsActive = x.IsActive,
                EffectiveFrom = x.EffectiveFrom,
                EffectiveTo = x.EffectiveTo
            }).ToListAsync();
    }

    public async Task<List<MemberVM>> GetAllMembersAsync() =>
        await _context.TblBoardMembers
            .OrderBy(m => m.MemberName)
            .Select(m => new MemberVM(m.MemberId, m.MemberName, m.MemberTname, m.MemberType, m.Phone, m.Email,
                m.Address, m.IsActive, m.EffectiveFrom == null ? null : m.EffectiveFrom.Value.ToDateTime(TimeOnly.MinValue),
                m.EffectiveTo == null ? null : m.EffectiveTo.Value.ToDateTime(TimeOnly.MinValue)))
            .ToListAsync();

    public async Task<OperationResult> ModifyExecutiveMemberAsync(ModifyExecutiveMemberRequest request)
    {
        var member = await _context.TblBoardMembers.FirstOrDefaultAsync(x => x.MemberId == request.MemberId);
        if (member == null) return new OperationResult(false, "Member not found.");

        member.MemberName = request.MemberName;
        member.MemberTname = request.MemberTname;
        member.Address = request.Address;
        member.Phone = request.Phone;
        member.Email = request.Email;
        member.IsActive = request.IsActive;
        member.MemberType = request.MemberType;
        member.EffectiveFrom = DateOnly.FromDateTime(request.EffectiveFrom);
        member.EffectiveTo = DateOnly.FromDateTime(request.EffectiveTo);
        member.CreatedBy = _currentUser.UserName;
        await _context.SaveChangesAsync();
        return new OperationResult(true, "Success");
    }

    public async Task<OperationResult> SaveExecutiveMemberAsync(SaveExecutiveMemberRequest request)
    {
        _context.TblBoardMembers.Add(new TblBoardMember
        {
            MemberName = request.MemberName,
            MemberTname = request.MemberTname,
            Address = request.Address,
            Phone = request.Phone,
            Email = request.Email,
            IsActive = request.IsActive,
            MemberType = request.MemberType,
            EffectiveFrom = DateOnly.FromDateTime(request.EffectiveFrom),
            EffectiveTo = DateOnly.FromDateTime(request.EffectiveTo),
            CreatedBy = _currentUser.UserName
        });
        await _context.SaveChangesAsync();
        return new OperationResult(true, "Success");
    }

    public async Task<OperationResult> AddUserAsync(SaveUserRequest request)
    {
        _context.TblUsers.Add(new TblUser
        {
            UserName = request.UserName,
            PasswordHash = PasswordHasher.Hash(request.Password),
            RoleId = request.RoleId,
            IsActive = request.IsActive,
            MobileNumber = request.PhoneNo,
            Email = request.Email,
            CreatedBy = _currentUser.UserName
        });
        await _context.SaveChangesAsync();
        return new OperationResult(true, "Success");
    }

    public async Task<List<UserVM>> GetExistingUsersAsync()
    {
        var users = await _context.TblUsers
            .Select(u => new UserVM(u.UserId, u.UserName, u.Email, u.MobileNumber, u.RoleId, u.IsActive))
            .ToListAsync();
        users.Insert(0, new UserVM(0, "Select User", null, null, 0, null));
        return users;
    }

    public async Task<UserVM?> GetUserByIdAsync(int userId)
    {
        return await _context.TblUsers
            .Where(u => u.UserId == userId)
            .Select(u => new UserVM(u.UserId, u.UserName, u.Email, u.MobileNumber, u.RoleId, u.IsActive))
            .FirstOrDefaultAsync();
    }

    public async Task<OperationResult> ModifyUserAsync(ModifyUserRequest request)
    {
        var user = await _context.TblUsers.FirstOrDefaultAsync(u => u.UserId == request.UserId);
        if (user == null) return new OperationResult(false, "User not found.");

        user.PasswordHash = PasswordHasher.Hash(request.NewPassword);
        user.IsActive = request.IsActive;
        user.RoleId = request.NewRoleId;
        user.Email = request.Email;
        user.MobileNumber = request.PhoneNo;
        user.UpdatedBy = _currentUser.UserName;
        user.UpdatedTimestamp = DateTime.Now;
        await _context.SaveChangesAsync();
        return new OperationResult(true, "Success");
    }

    public async Task<bool> CheckIfUserExistsAsync(string username) =>
        await _context.TblUsers.AnyAsync(x => x.UserName.ToLower() == username.ToLower());

    public async Task<OperationResult> ValidateAdminCredentialsAsync(string username, string password)
    {
        var user = await _context.TblUsers.FirstOrDefaultAsync(x => x.UserName.ToLower() == username.ToLower());
        if (user == null) return new OperationResult(false, "Invalid credentials.");

        var (valid, needsUpgrade) = PasswordHasher.Verify(password, user.PasswordHash);
        if (!valid) return new OperationResult(false, "Invalid credentials.");

        if (needsUpgrade)
        {
            user.PasswordHash = PasswordHasher.Hash(password);
            await _context.SaveChangesAsync();
        }

        if (user.RoleId == (int)UserRoles.Admin || user.RoleId == (int)UserRoles.Accountant)
            return new OperationResult(true, "Success");

        return new OperationResult(false, "Unauthorized access.");
    }

    public async Task<List<DonorTypeVM>> GetDonorTypesAsync() =>
        await _context.TblDonarTypeMasters
            .OrderBy(t => t.Donartype)
            .Select(t => new DonorTypeVM(t.Dtid, t.Donartype, t.Code, t.Amount, t.Days))
            .ToListAsync();

    public async Task<OperationResult> SaveDonorTypeAsync(SaveDonorTypeRequest request)
    {
        _context.TblDonarTypeMasters.Add(new TblDonarTypeMaster
        {
            Donartype = request.Donartype,
            Code = request.Code,
            Amount = request.Amount,
            Days = request.Days,
            CreatedBy = _currentUser.UserName
        });
        await _context.SaveChangesAsync();
        return new OperationResult(true, "Success");
    }

    public async Task<OperationResult> ModifyDonorTypeAsync(ModifyDonorTypeRequest request)
    {
        var t = await _context.TblDonarTypeMasters.FindAsync(request.Dtid);
        if (t == null) return new OperationResult(false, "Donation type not found.");

        t.Donartype = request.Donartype;
        t.Code = request.Code;
        t.Amount = request.Amount;
        t.Days = request.Days;
        await _context.SaveChangesAsync();
        return new OperationResult(true, "Success");
    }
}

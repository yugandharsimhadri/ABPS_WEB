using ABPS_WEB.Domain.Lookups;
using ABPS_WEB.Domain.RoomOps;
using ABPS_WEB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace ABPS_WEB.Infrastructure.Lookups;

/// <summary>Ported from ABPS_WPF's Controllers/DataCache.cs. The WPF version ran each query
/// in parallel via Task.Run + a separate AbpsContext per task — that was purely a desktop
/// startup-latency optimization (multiple concurrent Azure SQL connections), not user-facing
/// behavior, and a single injected DbContext can't run concurrent queries anyway (EF Core
/// throws if you try). Queries run sequentially here instead; same data, same placeholder
/// rows, different (and simpler) execution shape.</summary>
public class LookupService : ILookupService
{
    private readonly AbpsContext _context;

    public LookupService(AbpsContext context) => _context = context;

    public async Task<LookupsResponse> GetAllAsync()
    {
        var beds = await GetBedsAsync();
        var countries = await _context.TblCountries
            .Select(c => new IdName(c.CountryId, c.CountryName)).ToListAsync();
        // DataCache leaves Districts/States/Countries without a "Select..." placeholder row
        // (commented out in the WPF source) — intentionally not added here either.
        var states = await _context.TblStates
            .Select(s => new StateOption(s.StateId, s.CountryId, s.StateName)).ToListAsync();
        var districts = await _context.TblDistricts
            .Select(d => new DistrictOption(d.DistrictId, d.StateId, d.CountryId, d.DistrictName)).ToListAsync();

        var donationTypes = await _context.TblDonarTypeMasters
            .Select(d => new IdName(d.Dtid, d.Donartype)).ToListAsync();
        donationTypes.Insert(0, new IdName(-1, "Select Donation Type"));

        var paymentTypes = await _context.TblPaymentTypeMasters
            .Select(p => new IdName(p.PaymentTypeId, p.PaymentType)).ToListAsync();
        paymentTypes.Insert(0, new IdName(0, "Select Payment Type"));

        var paymentSubTypes = await _context.TblPaymentSubTypeMasters
            .Select(p => new PaymentSubTypeOption(p.PaymentSubTypeId, p.PaymentSubType, p.PaymentTypeId)).ToListAsync();
        paymentSubTypes.Insert(0, new PaymentSubTypeOption(0, "Select Payment Sub Type", 0));

        var occasions = await _context.TblOccassionMasters
            .Select(o => new IdName(o.OccassionId, o.OccassionName)).ToListAsync();
        occasions.Insert(0, new IdName(0, "Select Occassion"));

        var bookingCategories = await _context.TblBookingCategories
            .Select(c => new IdName(c.CatId, c.Category)).ToListAsync();
        bookingCategories.Insert(0, new IdName(0, "Select Donar Type"));

        var smsTemplates = await _context.TblSmstemplates
            .Select(s => new SmsTemplateOption(s.SmstemplateId, s.Smscontent, s.OccassionId)).ToListAsync();

        var teluguMasams = await _context.TblTeluguMasams
            .Select(t => new TeluguCalendarOption(t.TMasamId, t.TMasamName, t.TMasamNameEnglish)).ToListAsync();
        var teluguPakshams = await _context.TblTeluguPakshams
            .Select(t => new TeluguCalendarOption(t.TPakshamId, t.TPakshamName, t.TPakshamNameEnglish)).ToListAsync();
        var teluguThidis = await _context.TblTeluguThidis
            .Select(t => new TeluguCalendarOption(t.TThidiId, t.TThidiName, t.TThidiNameEnglish)).ToListAsync();

        var transactionTypes = await _context.TblTrasactionTypeMasters
            .Select(t => new IdName(t.TrasactionTypeId, t.TransactionType)).ToListAsync();
        transactionTypes.Insert(0, new IdName(0, "Transaction Type"));

        var transactionCategories = await _context.TblTrasactionCategoryMasters
            .Select(t => new IdName(t.TrasactionCategoryId, t.TransactionCategory)).ToListAsync();
        transactionCategories.Insert(0, new IdName(0, "Category Status"));

        var bookingStatuses = await _context.TblBookingStatuses
            .Select(b => new IdName(b.BookingStatusId, b.BookingStatusDesc)).ToListAsync();
        bookingStatuses.Insert(0, new IdName(0, "Select Booking Status"));

        var roomStatuses = await _context.TblRoomStatuses
            .Select(r => new IdName(r.RoomStatusId, r.RoomStatusDesc)).ToListAsync();
        roomStatuses.Insert(0, new IdName(0, "Select Room Status"));

        var roomTypes = await _context.TblRoomTypes
            .Where(rt => rt.RoomCategory != "DOR" && rt.RoomCategory != "LOC")
            .Select(rt => new RoomTypeOption(rt.RtId, rt.RtDesc, rt.RoomCategory)).ToListAsync();
        roomTypes.Insert(0, new RoomTypeOption(0, "Select Room Type", "Select Room Type"));

        var floors = await _context.TblFloorMasters
            .Select(f => new FloorOption(f.FloorId, f.FloorDescription)).ToListAsync();
        floors.Insert(0, new FloorOption(0, "Select Floor"));

        var userRoles = await _context.TblUserRoles
            .Select(u => new IdName(u.RoleId, u.Role)).ToListAsync();
        userRoles.Insert(0, new IdName(0, "Select Role"));

        return new LookupsResponse(beds, countries, states, districts, donationTypes, paymentTypes,
            paymentSubTypes, occasions, bookingCategories, smsTemplates, teluguMasams, teluguPakshams,
            teluguThidis, transactionTypes, transactionCategories, bookingStatuses, roomStatuses,
            roomTypes, floors, userRoles);
    }

    private async Task<List<BedOption>> GetBedsAsync()
    {
        var beds = await _context.TblRooms
            .Where(r => r.RoomStatusId == (int)RoomStatus.Available)
            .Select(r => r.Beds)
            .Distinct()
            .OrderBy(b => b)
            .Select(b => new BedOption(b, b + " Beds"))
            .ToListAsync();
        beds.Insert(0, new BedOption(-1, "Select Beds"));
        return beds;
    }
}

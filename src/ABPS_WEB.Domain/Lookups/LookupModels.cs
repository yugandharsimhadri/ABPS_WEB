namespace ABPS_WEB.Domain.Lookups;

public record IdName(int Id, string? Name);
public record BedOption(int Value, string Text);
public record RoomTypeOption(int RtId, string? RtDesc, string RoomCategory);
public record FloorOption(int FloorId, string? FloorDescription);
public record PaymentSubTypeOption(int PaymentSubTypeId, string? PaymentSubType, int PaymentTypeId);
public record SmsTemplateOption(int SmstemplateId, string? Smscontent, int? OccassionId);
public record StateOption(int StateId, int CountryId, string? StateName);
public record DistrictOption(int DistrictId, int StateId, int CountryId, string? DistrictName);
public record TeluguCalendarOption(int Id, string? Name, string? NameEnglish);

/// <summary>Ported from ABPS_WPF's Controllers/DataCache.cs — every WPF dropdown loaded once
/// at login into a static cache; this is the same "load everything once" data, served as one
/// response instead of 19 in-process static lists. "Select ..." placeholder rows are included
/// exactly where DataCache inserts them (and deliberately omitted for Districts/States/
/// Countries, which DataCache leaves unprefixed too — see LookupService for the per-list note).</summary>
public record LookupsResponse(
    List<BedOption> Beds,
    List<IdName> Countries,
    List<StateOption> States,
    List<DistrictOption> Districts,
    List<IdName> DonationTypes,
    List<IdName> PaymentTypes,
    List<PaymentSubTypeOption> PaymentSubTypes,
    List<IdName> Occasions,
    List<IdName> BookingCategories,
    List<SmsTemplateOption> SmsTemplates,
    List<TeluguCalendarOption> TeluguMasams,
    List<TeluguCalendarOption> TeluguPakshams,
    List<TeluguCalendarOption> TeluguThidis,
    List<IdName> TransactionTypes,
    List<IdName> TransactionCategories,
    List<IdName> BookingStatuses,
    List<IdName> RoomStatuses,
    List<RoomTypeOption> RoomTypes,
    List<FloorOption> Floors,
    List<IdName> UserRoles
);

public interface ILookupService
{
    Task<LookupsResponse> GetAllAsync();
}

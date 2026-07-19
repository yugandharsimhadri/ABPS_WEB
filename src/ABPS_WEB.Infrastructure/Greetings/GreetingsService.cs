using ABPS_WEB.Domain.Auth;
using ABPS_WEB.Domain.Greetings;
using ABPS_WEB.Domain.Notifications;
using ABPS_WEB.Domain.Reports;
using ABPS_WEB.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ABPS_WEB.Infrastructure.Greetings;

public class GreetingsService : IGreetingsService
{
    private readonly AbpsContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly IWhatsAppClient _whatsApp;
    private readonly IReportService _reports;
    private readonly ILogger<GreetingsService> _logger;

    public GreetingsService(AbpsContext context, ICurrentUserService currentUser, IWhatsAppClient whatsApp, IReportService reports, ILogger<GreetingsService> logger)
    {
        _context = context;
        _currentUser = currentUser;
        _whatsApp = whatsApp;
        _reports = reports;
        _logger = logger;
    }

    public async Task<List<GreetingRecipient>> FindByEnglishDateAsync(DateTime date)
    {
        var query = from occ in _context.TblDonationOccations
                    join donar in _context.TblDonars on occ.DonarId equals donar.DonarId
                    where occ.AnandaanamDate.HasValue
                        && occ.AnandaanamDate.Value.Day == date.Day
                        && occ.AnandaanamDate.Value.Month == date.Month
                        && occ.DateEnglishTelugu == "E"
                    select new GreetingRecipient(donar.Name, donar.PhoneNo, occ.BondNo, occ.IntheNameof, occ.OcassionName, occ.OcassionId ?? 0);

        return await query.ToListAsync();
    }

    public async Task<List<GreetingRecipient>> FindByTeluguCalendarAsync(int tMasamId, int tPakshamId, int tThiduluId)
    {
        var query = from occ in _context.TblDonationOccations
                    join donar in _context.TblDonars on occ.DonarId equals donar.DonarId
                    where occ.TMasamId == tMasamId
                        && occ.TPaskshamId == tPakshamId
                        && occ.TThiduluId == tThiduluId
                        && occ.DateEnglishTelugu == "T"
                    select new GreetingRecipient(donar.Name, donar.PhoneNo, occ.BondNo, occ.IntheNameof, occ.OcassionName, occ.OcassionId ?? 0);

        return await query.ToListAsync();
    }

    public async Task<int> GetSentTodayCountAsync() =>
        await _context.TblSmstransactions.CountAsync(x => x.CreatedTimestamp.Date == DateTime.Now.Date);

    public async Task<SendGreetingsResult> SendGreetingsAsync(SendGreetingsRequest request)
    {
        int sent = 0, attempted = 0;
        var createdBy = _currentUser.UserName;
        var templates = await _context.TblSmstemplates.ToListAsync();

        foreach (var r in request.Recipients)
        {
            attempted++;
            var toNumber = string.IsNullOrWhiteSpace(r.PhoneNo) ? "" : "91" + r.PhoneNo;
            if (toNumber.Length == 0) continue;

            string template = templates.FirstOrDefault(t => t.OccassionId == r.OcassionId)?.Smscontent
                ?? "Greetings {Name}! Today marks the {OcassionName} anniversary in the name of {IntheNameof} (Bond No {BondNo}).";
            string content = template
                .Replace("{Name}", r.Name)
                .Replace("{IntheNameof}", r.IntheNameof)
                .Replace("{OcassionName}", r.OcassionName)
                .Replace("{BondNo}", r.BondNo);

            try
            {
                await _whatsApp.SendTemplateMessageAsync(toNumber, "hello_world");
                var textResp = await _whatsApp.SendTextMessageAsync(toNumber, content);
                if (!string.IsNullOrEmpty(textResp))
                {
                    var txResult = await _reports.StoreSmsTransactionAsync(0, r.BondNo ?? "", r.OcassionId, request.SmsSendType,
                        r.PhoneNo, content, request.SmsDate, request.TMasamId, request.TPakshamId, request.TThiduluId, createdBy);
                    if (txResult == "Success") sent++;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "SendGreetings failed for {PhoneNo}", r.PhoneNo);
            }
        }

        return new SendGreetingsResult(sent, attempted);
    }
}

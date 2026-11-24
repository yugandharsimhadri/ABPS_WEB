using ABPS_WEB.Domain.AdvanceBooking;
using ABPS_WEB.Domain.Reports;
using ABPS_WEB.Domain.RoomOps;
using ABPS_WEB.Infrastructure.Data;
using Npgsql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ABPS_WEB.Infrastructure.Reports;

/// <summary>Ported from ABPS_WPF's Controllers/ReportController.cs — see IReportService.cs for
/// the two confirmed-dead methods deliberately not ported. Every query-timeout catch (originally
/// SqlException -2 on SQL Server, now IsTimeout on PostgreSQL; the 3-minute "shorten your date range" message) is preserved —
/// these reports have no fixed date range and can span years of data.</summary>
public class ReportService : IReportService
{
    private readonly AbpsContext _context;
    private readonly ILogger<ReportService> _logger;

    public ReportService(AbpsContext context, ILogger<ReportService> logger)
    {
        _context = context;
        _logger = logger;
    }

    private static TimeoutException Timeout() => new(
        "This search is beyond the execution time limit (3 minutes). Please shorten your date range or add more filters, then try again.");

    /// <summary>Npgsql signals a client-side command timeout as an NpgsqlException wrapping a
    /// TimeoutException; a server-side cancellation (statement_timeout) is SQLSTATE 57014.</summary>
    private static bool IsTimeout(NpgsqlException ex) =>
        ex.InnerException is TimeoutException || ex is PostgresException { SqlState: "57014" };

    public async Task<List<DonationReportVM>> GetDonationReportAsync(DateTime fromDate, DateTime toDate)
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            return await (from donation in _context.TblDonations
                          join donar in _context.TblDonars on donation.DonarId equals donar.DonarId
                          join donarType in _context.TblDonarTypeMasters on donation.DonationTypeId equals donarType.Dtid
                          join paymentType in _context.TblPaymentTypeMasters on donation.PaymentTypeId equals paymentType.PaymentTypeId
                          join occ in _context.TblDonationOccations on donation.DonationId equals occ.DonationId into occJoin
                          from occ in occJoin.DefaultIfEmpty()
                          where donation.DonationDate >= fromDate && donation.DonationDate <= toDate
                          select new DonationReportVM
                          {
                              DonationId = donation.DonationId,
                              BondNo = donation.BondNo,
                              DonationDate = donation.DonationDate,
                              DonorName = donar.Name,
                              PhoneNo = donar.PhoneNo,
                              Address = donar.Address,
                              DonationType = donarType.Donartype,
                              DonationAmount = donation.DonationAmount,
                              PaymentType = paymentType.PaymentType,
                              TransactionNo = donation.TransactionNo,
                              OccasionName = occ.OcassionName,
                              InTheNameOf = occ.IntheNameof,
                              RoomDays = donation.RoomDays,
                              CreatedBy = donation.CreatedBy
                          }).OrderByDescending(x => x.DonationDate).ToListAsync();
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in GetDonationReport");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetDonationReport");
            throw;
        }
    }

    public async Task<List<AdvanceBookingReportVM>> GetAdvanceBookingReportAsync(DateTime fromDate, DateTime toDate, int statusId = 0)
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            // Raw projection first, then map the status enum to a friendly string in memory —
            // a switch/enum-cast inside the LINQ-to-Entities query risks EF Core translation
            // failures, same caution already used in the WPF source.
            var raw = await (from a in _context.TblAdvanceBookings
                             where a.RequestedCheckInDate >= fromDate && a.RequestedCheckInDate <= toDate
                                   && (statusId == 0 || a.StatusId == statusId)
                             join rt in _context.TblRoomTypes on a.RoomCategoryRtId equals rt.RtId into rtJoin
                             from rt in rtJoin.DefaultIfEmpty()
                             join cat in _context.TblBookingCategories on a.CatId equals cat.CatId into catJoin
                             from cat in catJoin.DefaultIfEmpty()
                             orderby a.RequestedCheckInDate
                             select new
                             {
                                 a.AdvanceBookingId,
                                 a.GuestName,
                                 a.PhoneNo,
                                 a.AadharNo,
                                 a.NoPeople,
                                 a.NoRoomsRequested,
                                 RoomCategory = rt != null ? rt.RtDesc : null,
                                 BookingType = cat != null ? cat.Category : null,
                                 a.BondNo,
                                 a.RequestedCheckInDate,
                                 a.RequestedNights,
                                 a.AdvanceDepositAmount,
                                 a.StatusId,
                                 a.Notes,
                                 a.CreatedTimestamp
                             }).ToListAsync();

            return raw.Select(a => new AdvanceBookingReportVM
            {
                AdvanceBookingId = a.AdvanceBookingId,
                GuestName = a.GuestName,
                PhoneNo = a.PhoneNo,
                AadharNo = a.AadharNo ?? "",
                NoPeople = a.NoPeople,
                NoRoomsRequested = a.NoRoomsRequested,
                RoomCategory = a.RoomCategory ?? "Any",
                BookingType = a.BookingType ?? "",
                BondNo = a.BondNo ?? "",
                RequestedCheckInDate = a.RequestedCheckInDate,
                RequestedNights = a.RequestedNights,
                AdvanceDepositAmount = a.AdvanceDepositAmount ?? 0,
                Status = Enum.IsDefined(typeof(AdvanceBookingStatus), a.StatusId)
                    ? ((AdvanceBookingStatus)a.StatusId).ToString()
                    : "Unknown",
                Notes = a.Notes ?? "",
                CreatedTimestamp = a.CreatedTimestamp
            }).ToList();
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in GetAdvanceBookingReport");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAdvanceBookingReport");
            throw;
        }
    }

    public async Task<List<BookingReportVM>> GetBookingReportAsync(DateTime fromDate, DateTime toDate, int statusId)
    {
        try
        {
            // No fixed date range and can span years of bookings — the app-wide 30s command
            // timeout is too tight for a wide search, so give it up to 3 minutes.
            _context.Database.SetCommandTimeout(180);

            var bookings = await _context.TblBookingDetails
                .Where(bd => bd.BookingDate >= fromDate && bd.BookingDate <= toDate
                             && (statusId == 0 || bd.BookingStatusId == statusId))
                .ToListAsync();

            var roomsByBooking = (await (
                from br in _context.TblBookingRooms
                join bd2 in _context.TblBookingDetails on br.BookingId equals bd2.BookingId
                join rTemp in _context.TblRooms on br.RoomId equals rTemp.RoomId into rJoin
                from r in rJoin.DefaultIfEmpty()
                where bd2.BookingDate >= fromDate && bd2.BookingDate <= toDate
                      && (statusId == 0 || bd2.BookingStatusId == statusId)
                select new { br.BookingId, br.CheckInDate, br.CheckOutDate, RoomNo = r.RoomNo })
                .ToListAsync())
                .GroupBy(x => x.BookingId!.Value)
                .ToDictionary(g => g.Key, g => g.ToList());

            return bookings
                .Select(bd =>
                {
                    roomsByBooking.TryGetValue(bd.BookingId, out var roomRows);
                    var firstRoom = roomRows?.FirstOrDefault();
                    return new BookingReportVM
                    {
                        BookingId = bd.BookingId,
                        NoOfRooms = roomRows?.Count ?? 0,
                        Name = bd.Name,
                        DateBooking = bd.BookingDate!.Value.Date,
                        TimeBooking = bd.BookingDate.Value.TimeOfDay,
                        DateArrived = firstRoom?.CheckInDate?.Date,
                        TimeArrived = firstRoom?.CheckInDate?.TimeOfDay,
                        DateCheckOut = firstRoom?.CheckOutDate?.Date,
                        TimeCheckOut = firstRoom?.CheckOutDate?.TimeOfDay,
                        Advance = bd.Advance ?? 0,
                        Persons = bd.NoPeople ?? 0,
                        UserName = bd.CreatedBy,
                        PhoneNo = bd.PhoneNo,
                        RoomNos = string.Join(", ", roomRows?.Select(r => r.RoomNo).Distinct() ?? Enumerable.Empty<string>())
                    };
                })
                .OrderByDescending(x => x.DateBooking.Add(x.TimeBooking))
                .ThenBy(x => x.DateBooking)
                .ToList();
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in GetBookingReport");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetBookingReport");
            throw;
        }
    }

    public async Task<ReceiptData?> GetPrecautionReceiptAsync(int bookingId)
    {
        try
        {
            var data = await (from bd in _context.TblBookingDetails
                              join br in _context.TblBookingRooms on bd.BookingId equals br.BookingId
                              join r in _context.TblRooms on br.RoomId equals r.RoomId
                              where bd.BookingId == bookingId
                              group new { bd, br, r } by bd.BookingId into g
                              select new ReceiptData
                              {
                                  ReceiptType = ReceiptType.Precaution,
                                  ReceiptNo = g.Key.ToString(),
                                  ReceiptDate = g.First().bd.BookingDate,
                                  ReceiptTime = g.First().bd.BookingDate!.Value.TimeOfDay,
                                  ArrivedDate = g.First().br.CheckInDate,
                                  ArrivedTime = g.First().br.CheckInDate!.Value.TimeOfDay,
                                  VacatedDate = g.First().br.CheckOutDate,
                                  VacatedTime = g.First().br.CheckOutDate!.Value.TimeOfDay,
                                  CustomerName = g.First().bd.Name,
                                  Address = "HYD",
                                  PhoneNo = g.First().bd.PhoneNo,
                                  Amount = g.First().bd.Advance ?? 0,
                                  Persons = g.First().bd.NoPeople ?? 0,
                                  NoOfRooms = g.Count(),
                                  RoomNos = string.Join(", ", g.Select(x => x.r.RoomNo)),
                                  MaintenanceAmount = 50,
                                  DonationAmount = 0,
                                  TotalAmount = g.First().bd.Advance ?? 0,
                                  AmountWords = IndianNumberHelper.NumberToCurrencyWords((int)(g.First().bd.Advance ?? 0))
                              }).FirstOrDefaultAsync();
            return data;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetPrecautionReceipt");
            return null;
        }
    }

    public async Task<List<RoomReportVM>> GetRoomReportAsync(int rtId, int floorId, int statusId, int beds)
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            return await (
                from r in _context.TblRooms
                join rt in _context.TblRoomTypes on r.RtId equals rt.RtId
                join fm in _context.TblFloorMasters on r.FloorId equals fm.FloorId
                join rs in _context.TblRoomStatuses on r.RoomStatusId equals rs.RoomStatusId
                join bd in _context.TblBookingDetails on r.BookingId equals bd.BookingId into bdJoin
                from bd in bdJoin.DefaultIfEmpty()
                where (rtId == 0 || r.RtId == rtId)
                    && (floorId == 0 || r.FloorId == floorId)
                    && (statusId == 0 || r.RoomStatusId == statusId)
                    && (beds == -1 || r.Beds == beds)
                select new RoomReportVM
                {
                    RoomId = r.RoomId,
                    RoomNo = r.RoomNo ?? string.Empty,
                    Category = rt.RoomCategory,
                    Price = r.Cost,
                    Floor = fm.FloorDescription ?? string.Empty,
                    Beds = r.Beds,
                    Allowed = r.Allowed,
                    Status = rs.RoomStatusDesc ?? string.Empty,
                    CustomerName = r.RoomStatusId == (int)RoomStatus.Occupied
                        ? (bd != null ? bd.Name ?? string.Empty : string.Empty)
                        : string.Empty,
                    PhoneNo = r.RoomStatusId == (int)RoomStatus.Occupied
                        ? (bd != null ? bd.PhoneNo ?? string.Empty : string.Empty)
                        : string.Empty,
                    BookingDate = r.RoomStatusId == (int)RoomStatus.Occupied
                        ? (bd == null ? (DateTime?)null : bd.BookingDate)
                        : null,
                    BookingId = r.RoomStatusId == (int)RoomStatus.Occupied ? r.BookingId : 0
                }).ToListAsync();
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in GetRoomReport");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetRoomReport");
            throw;
        }
    }

    public async Task<string> StoreSmsTransactionAsync(int donarId, string bondNo, int occasionId, string smsSendType,
        string? phoneNo, string content, DateTime smsDate, int tMasamId, int tPaksham, int tThidulu, string? createdBy)
    {
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            _context.TblSmstransactions.Add(new Data.Models.TblSmstransaction
            {
                DonarId = donarId,
                BondNo = bondNo,
                OccasionId = occasionId,
                PhoneNo = phoneNo,
                DateType = smsSendType,
                Content = content,
                Smsdate = DateOnly.FromDateTime(smsDate.Date),
                TMasamId = tMasamId,
                TPaskshamId = tPaksham,
                TThiduluId = tThidulu,
                CreatedBy = createdBy,
            });
            await _context.SaveChangesAsync();
            await tx.CommitAsync();
            return "Success";
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in StoreSmsTransaction");
            return "Error adding the room: " + ex.Message;
        }
    }

    public async Task<List<TransactionReportVM>> GetTransactionReportAsync(DateTime fromDate, DateTime toDate, int categoryId, int typeId)
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            var datasource = await (from t in _context.TblTransactions
                                    join tc in _context.TblTrasactionCategoryMasters on t.TransactionCategoryId equals tc.TrasactionCategoryId
                                    join tt in _context.TblTrasactionTypeMasters on t.TrasactionTypeId equals tt.TrasactionTypeId
                                    join pt in _context.TblPaymentTypeMasters on t.PaymentTypeId equals pt.PaymentTypeId
                                    join pst in _context.TblPaymentSubTypeMasters on t.PaymentSubTypeId equals pst.PaymentSubTypeId into pstGroup
                                    from pst in pstGroup.DefaultIfEmpty()
                                    where t.TransactionDate >= fromDate && t.TransactionDate < toDate
                                          && (categoryId == 0 || t.TransactionCategoryId == categoryId)
                                          && (typeId == 0 || t.TrasactionTypeId == typeId)
                                    orderby t.CreatedTimestamp descending
                                    select new TransactionReportVM
                                    {
                                        TransactionId = t.TransactionId,
                                        TransactionDate = t.TransactionDate,
                                        CategoryName = tc.TransactionCategory,
                                        TypeName = tt.TransactionType,
                                        DonarBookingId = t.DonarBookingId,
                                        Amount = t.Amount,
                                        PaymentType = pt.PaymentType,
                                        PaymentSubType = pst != null ? pst.PaymentSubType : "",
                                        ReferenceNumber = t.ReferenceNumber,
                                        BondNo = t.BondNo,
                                        Notes = t.Notes,
                                        CreatedBy = t.CreatedBy,
                                        CreatedTimestamp = t.CreatedTimestamp
                                    }).ToListAsync();

            var totalAmount = datasource.Sum(x =>
                (x.TypeName == "Advance" || x.TypeName == "Payment") ? x.Amount :
                (x.TypeName == "Refund") ? -x.Amount : 0);

            datasource.Add(new TransactionReportVM
            {
                TransactionId = 0,
                TransactionDate = null,
                CategoryName = "TOTAL",
                TypeName = "",
                DonarBookingId = null,
                Amount = totalAmount,
                PaymentType = "",
                PaymentSubType = "",
                ReferenceNumber = "",
                BondNo = "",
                Notes = "",
                CreatedBy = "",
                CreatedTimestamp = DateTime.Now
            });

            return datasource;
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in getTransactionReport");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in getTransactionReport");
            throw;
        }
    }

    public async Task<List<TransactionSummaryDayWise>> GetTransactionDaySummaryAsync(DateTime fromDate, DateTime toDate, int categoryId, int typeId)
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            var query = await (from t in _context.TblTransactions
                               join tc in _context.TblTrasactionCategoryMasters on t.TransactionCategoryId equals tc.TrasactionCategoryId
                               join tt in _context.TblTrasactionTypeMasters on t.TrasactionTypeId equals tt.TrasactionTypeId
                               where t.TransactionDate >= fromDate && t.TransactionDate < toDate
                                     && (categoryId == 0 || t.TransactionCategoryId == categoryId)
                                     && (typeId == 0 || t.TrasactionTypeId == typeId)
                               select new { t.TransactionDate, t.Amount, tc.TransactionCategory, tt.TransactionType }).ToListAsync();

            var grouped = (categoryId > 0 || typeId > 0)
                ? query.GroupBy(x => new { Date = x.TransactionDate!.Value.Date, x.TransactionCategory, x.TransactionType })
                    .Select(g => new TransactionSummaryDayWise
                    {
                        Date = g.Key.Date,
                        Category = g.Key.TransactionCategory,
                        Type = g.Key.TransactionType,
                        TotalTransactions = g.Count(),
                        TotalAmount = (decimal)g.Sum(x => (x.TransactionType == "Advance" || x.TransactionType == "Payment") ? x.Amount : (x.TransactionType == "Refund") ? -x.Amount : 0)
                    })
                : query.GroupBy(x => x.TransactionDate!.Value.Date)
                    .Select(g => new TransactionSummaryDayWise
                    {
                        Date = g.Key,
                        Category = null,
                        Type = null,
                        TotalTransactions = g.Count(),
                        TotalAmount = (decimal)g.Sum(x => (x.TransactionType == "Advance" || x.TransactionType == "Payment") ? x.Amount : (x.TransactionType == "Refund") ? -x.Amount : 0)
                    });

            var datasource = grouped.OrderByDescending(x => x.Date).ToList();
            var netTotal = datasource.Sum(x => x.TotalAmount);
            datasource.Add(new TransactionSummaryDayWise { Date = null, Category = "Net Total", Type = "", TotalTransactions = datasource.Sum(x => x.TotalTransactions), TotalAmount = netTotal });
            return datasource;
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in getTransactionDaySummary");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in getTransactionDaySummary");
            throw;
        }
    }

    public async Task<List<TransactionSummaryDayCategory>> GetTransactionDaySummaryByGroupAsync(DateTime fromDate, DateTime toDate, int categoryId, int typeId, string groupBy)
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            var queryList = await (from t in _context.TblTransactions
                                   join tc in _context.TblTrasactionCategoryMasters on t.TransactionCategoryId equals tc.TrasactionCategoryId
                                   join tt in _context.TblTrasactionTypeMasters on t.TrasactionTypeId equals tt.TrasactionTypeId
                                   where t.TransactionDate >= fromDate && t.TransactionDate < toDate
                                         && (categoryId == 0 || t.TransactionCategoryId == categoryId)
                                         && (typeId == 0 || t.TrasactionTypeId == typeId)
                                   select new { t.TransactionDate, t.Amount, tc.TransactionCategory, tt.TransactionType }).ToListAsync();

            var grouped = groupBy == "Category"
                ? queryList.GroupBy(x => new { Date = x.TransactionDate!.Value.Date, GroupName = x.TransactionCategory })
                    .Select(g => new TransactionSummaryDayCategory
                    {
                        Date = g.Key.Date,
                        GroupName = g.Key.GroupName,
                        TotalTransactions = g.Count(),
                        TotalAmount = (decimal)g.Sum(x => (x.TransactionType == "Advance" || x.TransactionType == "Payment") ? x.Amount : (x.TransactionType == "Refund") ? -x.Amount : 0)
                    })
                : queryList.GroupBy(x => new { Date = x.TransactionDate!.Value.Date, GroupName = x.TransactionType })
                    .Select(g => new TransactionSummaryDayCategory
                    {
                        Date = g.Key.Date,
                        GroupName = g.Key.GroupName,
                        TotalTransactions = g.Count(),
                        TotalAmount = (decimal)g.Sum(x => (x.TransactionType == "Advance" || x.TransactionType == "Payment") ? x.Amount : (x.TransactionType == "Refund") ? -x.Amount : 0)
                    });

            var datasource = grouped.OrderByDescending(x => x.Date).ToList();
            var netTotal = datasource.Sum(x => x.TotalAmount);
            var totalTransactions = datasource.Sum(x => x.TotalTransactions);
            datasource.Add(new TransactionSummaryDayCategory { Date = null, GroupName = "Net Total", TotalTransactions = totalTransactions, TotalAmount = netTotal });
            return datasource;
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in getTransactionDaySummary with groupby");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in getTransactionDaySummary with groupby");
            throw;
        }
    }

    public async Task<List<TransactionSummaryMonthWise>> GetTransactionMonthSummaryAsync(DateTime fromDate, DateTime toDate, int categoryId, int typeId)
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            var query = await (from t in _context.TblTransactions
                               join tc in _context.TblTrasactionCategoryMasters on t.TransactionCategoryId equals tc.TrasactionCategoryId
                               join tt in _context.TblTrasactionTypeMasters on t.TrasactionTypeId equals tt.TrasactionTypeId
                               where t.TransactionDate >= fromDate && t.TransactionDate < toDate
                                     && (categoryId == 0 || t.TransactionCategoryId == categoryId)
                                     && (typeId == 0 || t.TrasactionTypeId == typeId)
                               select new { t.TransactionDate, t.Amount, tc.TransactionCategory, tt.TransactionType }).ToListAsync();

            var grouped = (categoryId > 0 || typeId > 0)
                ? query.GroupBy(x => new { Year = x.TransactionDate!.Value.Year, Month = x.TransactionDate!.Value.Month, x.TransactionCategory, x.TransactionType })
                    .Select(g => new TransactionSummaryMonthWise
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        MonthName = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM"),
                        Category = g.Key.TransactionCategory,
                        Type = g.Key.TransactionType,
                        TotalTransactions = g.Count(),
                        TotalAmount = g.Sum(x => (x.TransactionType == "Advance" || x.TransactionType == "Payment") ? x.Amount : (x.TransactionType == "Refund") ? -x.Amount : 0)
                    })
                : query.GroupBy(x => new { Year = x.TransactionDate!.Value.Year, Month = x.TransactionDate!.Value.Month })
                    .Select(g => new TransactionSummaryMonthWise
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        MonthName = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM"),
                        Category = null,
                        Type = null,
                        TotalTransactions = g.Count(),
                        TotalAmount = g.Sum(x => (x.TransactionType == "Advance" || x.TransactionType == "Payment") ? x.Amount : (x.TransactionType == "Refund") ? -x.Amount : 0)
                    });

            var datasource = grouped.OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).ToList();
            var netTotal = datasource.Sum(x => x.TotalAmount);
            datasource.Add(new TransactionSummaryMonthWise { Year = 0, Month = 0, MonthName = "", Category = "Net Total", Type = "", TotalTransactions = datasource.Sum(x => x.TotalTransactions), TotalAmount = netTotal });
            return datasource;
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in getTransactionMonthSummary");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in getTransactionMonthSummary");
            throw;
        }
    }

    public async Task<List<TransactionSummaryMonthCategory>> GetTransactionMonthSummaryByGroupAsync(DateTime fromDate, DateTime toDate, int categoryId, int typeId, string? groupBy)
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            var query = await (from t in _context.TblTransactions
                               join tc in _context.TblTrasactionCategoryMasters on t.TransactionCategoryId equals tc.TrasactionCategoryId
                               join tt in _context.TblTrasactionTypeMasters on t.TrasactionTypeId equals tt.TrasactionTypeId
                               where t.TransactionDate >= fromDate && t.TransactionDate < toDate
                                     && (categoryId == 0 || t.TransactionCategoryId == categoryId)
                                     && (typeId == 0 || t.TrasactionTypeId == typeId)
                               select new { t.TransactionDate, t.Amount, tc.TransactionCategory, tt.TransactionType }).ToListAsync();

            var datasource = groupBy == "Category"
                ? query.GroupBy(x => new { Year = x.TransactionDate!.Value.Year, Month = x.TransactionDate!.Value.Month, GroupName = x.TransactionCategory })
                    .Select(g => new TransactionSummaryMonthCategory
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        MonthName = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM"),
                        GroupName = g.Key.GroupName,
                        TotalTransactions = g.Count(),
                        TotalAmount = g.Sum(x => (x.TransactionType == "Advance" || x.TransactionType == "Payment") ? x.Amount : (x.TransactionType == "Refund") ? -x.Amount : 0)
                    })
                : query.GroupBy(x => new { Year = x.TransactionDate!.Value.Year, Month = x.TransactionDate!.Value.Month, GroupName = x.TransactionType })
                    .Select(g => new TransactionSummaryMonthCategory
                    {
                        Year = g.Key.Year,
                        Month = g.Key.Month,
                        MonthName = new DateTime(g.Key.Year, g.Key.Month, 1).ToString("MMMM"),
                        GroupName = g.Key.GroupName,
                        TotalTransactions = g.Count(),
                        TotalAmount = g.Sum(x => (x.TransactionType == "Advance" || x.TransactionType == "Payment") ? x.Amount : (x.TransactionType == "Refund") ? -x.Amount : 0)
                    });

            var result = datasource.OrderByDescending(x => x.Year).ThenByDescending(x => x.Month).ToList();
            var netTotal = result.Sum(x => x.TotalAmount);
            result.Add(new TransactionSummaryMonthCategory { Year = 0, Month = 0, MonthName = "", GroupName = "Net Total", TotalTransactions = result.Sum(x => x.TotalTransactions), TotalAmount = netTotal });
            return result;
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in getTransactionMonthSummary with groupby");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in getTransactionMonthSummary with groupby");
            throw;
        }
    }

    public async Task<List<ExpiringBookingReportVM>> GetExpiringBookingReportAsync()
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            return await _context.TblBookingDetails
                .Where(bd => bd.BookingStatusId == (int)BookingStatus.CheckIn)
                .Join(
                    _context.TblBookingLedgers.Where(bl => bl.CheckinDateTime != null),
                    bd => bd.BookingId, bl => bl.BookingId,
                    (bd, bl) => new ExpiringBookingReportVM
                    {
                        BookingId = bd.BookingId,
                        Name = bd.Name,
                        PhoneNo = bd.PhoneNo,
                        NoOfRooms = bd.NoOfRooms,
                        TotalAmount = bd.TotalAmount,
                        BookingLedgerId = bl.BookingLedgerId,
                        NoOfDays = bl.NoOfDays,
                        CheckinDateTime = bl.CheckinDateTime,
                        CheckoutDateTime = bl.CheckoutDateTime,
                        ExpectedCheckoutDate = bl.CheckinDateTime!.Value.AddDays(bl.NoOfDays ?? 0)
                    })
                .OrderBy(x => x.ExpectedCheckoutDate)
                .ToListAsync();
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in getExpiringBookingReport");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in getExpiringBookingReport");
            throw;
        }
    }

    public async Task<List<UserOption>> GetAllUsersAsync()
    {
        try
        {
            var users = await _context.TblUsers
                .Where(u => u.IsActive == "Y")
                .Select(u => new UserOption(u.UserId, u.UserName))
                .ToListAsync();
            users.Insert(0, new UserOption(0, "Select User"));
            return users;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in getallusers");
            return new List<UserOption>();
        }
    }

    public async Task<List<UserTransactionDetails>> GetTransactionReportByUserAsync(DateTime fromDate, DateTime toDate, string userName, int selectedIndex, int paymentTypeId)
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            var result = await (from t in _context.TblTransactions
                                where t.TransactionDate >= fromDate && t.TransactionDate < toDate
                                    && (selectedIndex == 0 || t.CreatedBy == userName)
                                    && (paymentTypeId == 0 || t.PaymentTypeId == paymentTypeId)
                                join tc in _context.TblTrasactionCategoryMasters on t.TransactionCategoryId equals tc.TrasactionCategoryId into tcGroup
                                from tc in tcGroup.DefaultIfEmpty()
                                join tt in _context.TblTrasactionTypeMasters on t.TrasactionTypeId equals tt.TrasactionTypeId into ttGroup
                                from tt in ttGroup.DefaultIfEmpty()
                                join pt in _context.TblPaymentTypeMasters on t.PaymentTypeId equals pt.PaymentTypeId into ptGroup
                                from pt in ptGroup.DefaultIfEmpty()
                                join pst in _context.TblPaymentSubTypeMasters on t.PaymentSubTypeId equals pst.PaymentSubTypeId into pstGroup
                                from pst in pstGroup.DefaultIfEmpty()
                                select new
                                {
                                    t.TransactionDate,
                                    CategoryName = tc != null ? tc.TransactionCategory : null,
                                    TypeName = tt != null ? tt.TransactionType : null,
                                    t.DonarBookingId,
                                    t.Amount,
                                    PaymentType = pt != null ? pt.PaymentType : null,
                                    PaymentSubType = pst != null ? pst.PaymentSubType : null,
                                    t.ReferenceNumber,
                                    t.BondNo,
                                    t.Notes,
                                    t.CreatedBy,
                                    t.CreatedTimestamp
                                }).ToListAsync();

            var list = result.Select(x => new UserTransactionDetails
            {
                TransactionDate = x.TransactionDate,
                CategoryName = x.CategoryName,
                TypeName = x.TypeName,
                DonarBookingId = x.DonarBookingId,
                Amount = x.Amount ?? 0,
                PaymentType = x.PaymentType,
                PaymentSubType = x.PaymentSubType,
                ReferenceNumber = x.ReferenceNumber,
                BondNo = x.BondNo,
                Notes = x.Notes,
                CreatedBy = x.CreatedBy,
                CreatedTimestamp = x.CreatedTimestamp
            }).ToList();

            var netTotal = list.Sum(x => (x.TypeName == "Advance" || x.TypeName == "Payment") ? x.Amount : (x.TypeName == "Refund") ? -x.Amount : 0);
            list.Add(new UserTransactionDetails
            {
                TransactionDate = null,
                CategoryName = "Net Total",
                TypeName = "",
                DonarBookingId = null,
                Amount = netTotal,
                PaymentType = "",
                PaymentSubType = "",
                ReferenceNumber = "",
                BondNo = "",
                Notes = "",
                CreatedBy = "",
                CreatedTimestamp = null
            });

            return list;
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in getTransactionReport by user");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in getTransactionReport by user");
            throw;
        }
    }

    public async Task<List<UserTransactionSummaryVM>> GetTransactionUserSummaryReportAsync(DateTime fromDate, DateTime toDate, string userName, int selectedIndex, int paymentTypeId)
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            var raw = await (from t in _context.TblTransactions
                             where t.TransactionDate >= fromDate && t.TransactionDate < toDate
                                 && (selectedIndex == 0 || t.CreatedBy == userName)
                                 && (paymentTypeId == 0 || t.PaymentTypeId == paymentTypeId)
                             join tc in _context.TblTrasactionCategoryMasters on t.TransactionCategoryId equals tc.TrasactionCategoryId into tcGroup
                             from tc in tcGroup.DefaultIfEmpty()
                             join tt in _context.TblTrasactionTypeMasters on t.TrasactionTypeId equals tt.TrasactionTypeId into ttGroup
                             from tt in ttGroup.DefaultIfEmpty()
                             select new { t, tc, tt }).ToListAsync();

            var result = raw.Select(x => new UserTransactionSummaryVM
            {
                TransactionDate = x.t.TransactionDate,
                DonarBookingId = x.t.DonarBookingId,
                CategoryName = x.tc?.TransactionCategory,
                LoginUser = x.t.CreatedBy,
                Advance = x.tt?.TrasactionTypeId == (int)TransactionType.Advance ? x.t.Amount ?? 0 : 0,
                Payment = x.tt?.TrasactionTypeId == (int)TransactionType.Payment ? x.t.Amount ?? 0 : 0,
                Refund = x.tt?.TrasactionTypeId == (int)TransactionType.Refund ? x.t.Amount ?? 0 : 0
            }).ToList();

            var totalAdvance = result.Sum(r => r.Advance);
            var totalPayment = result.Sum(r => r.Payment);
            var totalRefund = result.Sum(r => r.Refund);

            result.Add(new UserTransactionSummaryVM
            {
                TransactionDate = null,
                DonarBookingId = null,
                CategoryName = "Total",
                LoginUser = "",
                Advance = totalAdvance,
                Payment = totalPayment,
                Refund = totalRefund
            });

            return result;
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in GetTransactionUserSummaryReport");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetTransactionUserSummaryReport");
            throw;
        }
    }

    public async Task<int> GetBookingStatusIdAsync(int bookingId)
    {
        try
        {
            return await _context.TblBookingDetails
                .Where(bd => bd.BookingId == bookingId)
                .Select(bd => bd.BookingStatusId)
                .FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in getbookingStatusID");
            return -1;
        }
    }

    public async Task<List<OccupancyReportVM>> GetOccupancyReportAsync()
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            var data = await _context.TblBookingDetails
                .Where(bd => bd.BookingStatusId == (int)BookingStatus.CheckIn)
                .Join(
                    _context.TblBookingLedgers.Where(bl => bl.CheckinDateTime != null),
                    bd => bd.BookingId, bl => bl.BookingId,
                    (bd, bl) => new
                    {
                        bd.BookingId,
                        bd.Name,
                        bd.PhoneNo,
                        bd.NoOfRooms,
                        bd.TotalAmount,
                        bd.NoPeople,
                        bl.BookingLedgerId,
                        bl.NoOfDays,
                        bl.CheckinDateTime,
                        ExpectedCheckoutDate = bl.CheckinDateTime!.Value.AddDays(bl.NoOfDays ?? 0)
                    })
                .OrderBy(x => x.ExpectedCheckoutDate)
                .ToListAsync();

            var bookingRooms = (await _context.TblBookingRooms
                .Join(_context.TblRooms, br => br.RoomId, r => r.RoomId, (br, r) => new { br.BookingId, r.RoomNo })
                .ToListAsync())
                .GroupBy(x => x.BookingId)
                .ToDictionary(g => g.Key, g => string.Join(",", g.Select(x => x.RoomNo)));

            // No synthetic "TOTAL" row — GenericReportWindow already sums numeric columns
            // client-side; an injected pre-summed row used to double-count them.
            return data.Select(x => new OccupancyReportVM
            {
                Name = x.Name,
                PhoneNo = x.PhoneNo,
                NoOfRooms = x.NoOfRooms,
                RoomNos = bookingRooms.TryGetValue(x.BookingId, out var rn) ? rn : "",
                TotalAmount = x.TotalAmount,
                NoPeople = x.NoPeople,
                NoOfDays = x.NoOfDays,
                CheckinDateTime = x.CheckinDateTime,
                ExpectedCheckoutDate = x.ExpectedCheckoutDate
            }).ToList();
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in getOccupancyReport");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in getOccupancyReport");
            throw;
        }
    }

    public async Task<List<AuditTransactionReport>> GetAuditTransactionReportAsync(DateTime fromDate, DateTime toDate)
    {
        try
        {
            _context.Database.SetCommandTimeout(180);
            var roomNosByBooking = (await _context.TblBookingRooms
                .Join(_context.TblRooms, br => br.RoomId, r => r.RoomId, (br, r) => new { br.BookingId, r.RoomNo })
                .ToListAsync())
                .GroupBy(x => x.BookingId)
                .ToDictionary(g => g.Key, g => string.Join(",", g.Select(x => x.RoomNo)));

            // Phase 1: query only raw fields EF can translate.
            var query = await (from tr in _context.TblTransactionRoomDonations
                               where tr.CreatedTimestamp >= fromDate && tr.CreatedTimestamp <= toDate
                               join bd in _context.TblBookingDetails on tr.BookingId equals bd.BookingId into bdJoin
                               from bd in bdJoin.DefaultIfEmpty()
                               select new
                               {
                                   tr.BookingId,
                                   bd.Name,
                                   tr.CheckoutDate,
                                   tr.NoofDays,
                                   tr.TotalAmount,
                                   tr.RoomAmount,
                                   tr.DonationAmount,
                                   tr.CreatedBy,
                                   tr.CreatedTimestamp
                               })
                               .OrderBy(x => x.CreatedTimestamp)
                               .ToListAsync();

            // Phase 2: client-side projection with calculations.
            var result = query.Select(x => new AuditTransactionReport
            {
                BookingId = x.BookingId,
                RoomNos = (x.BookingId != null && roomNosByBooking.ContainsKey(x.BookingId.Value))
                    ? roomNosByBooking[x.BookingId.Value] : string.Empty,
                BookedName = x.Name ?? string.Empty,
                CheckoutDate = x.CheckoutDate,
                NoofDays = x.NoofDays ?? 0,
                PerDayAmount = Math.Round((x.TotalAmount ?? 0m) / Math.Max(x.NoofDays ?? 0, 1), 2),
                TotalAmount = x.TotalAmount ?? 0m,
                RoomAmount = x.RoomAmount ?? 0m,
                DonationAmount = x.DonationAmount ?? 0m,
                CreatedBy = x.CreatedBy ?? string.Empty,
                CreatedTimestamp = x.CreatedTimestamp
            }).ToList();

            result.Add(new AuditTransactionReport
            {
                BookingId = null,
                RoomNos = string.Empty,
                BookedName = "Net Total",
                CheckoutDate = null,
                NoofDays = result.Sum(r => r.NoofDays),
                PerDayAmount = null,
                TotalAmount = result.Sum(r => r.TotalAmount),
                RoomAmount = result.Sum(r => r.RoomAmount),
                DonationAmount = result.Sum(r => r.DonationAmount),
                CreatedBy = string.Empty,
                CreatedTimestamp = DateTime.Now
            });

            return result;
        }
        catch (NpgsqlException ex) when (IsTimeout(ex))
        {
            _logger.LogError(ex, "Timeout in GetAuditTransactionReport");
            throw Timeout();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in GetAuditTransactionReport");
            throw;
        }
    }
}

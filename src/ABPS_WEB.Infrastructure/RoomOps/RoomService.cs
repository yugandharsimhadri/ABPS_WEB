using ABPS_WEB.Domain.Auth;
using ABPS_WEB.Domain.Notifications;
using ABPS_WEB.Domain.RoomOps;
using ABPS_WEB.Infrastructure.Data;
using ABPS_WEB.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ABPS_WEB.Infrastructure.RoomOps;

/// <summary>Ported from ABPS_WPF's Controllers/RoomController.cs, in full, PLUS the WhatsApp
/// notification calls that lived in each screen's code-behind (BookRoomWindow.xaml.cs,
/// VacateRoomWindow.xaml.cs, etc.) rather than in RoomController itself — since that
/// screen-level logic has moved server-side here, the notification triggers move with it.
/// Every notify call is `_ = _whatsApp.NotifyAsync(...)` (discarded, unawaited) exactly like
/// the WPF version, so a WhatsApp outage can never affect an already-committed operation.
/// ILogger<T> replaces the WPF app's custom LogHelper/NLog — a platform-appropriate swap, not
/// a functional change; DB-backed error-log parity (tbl_ErrorLogs) is a follow-up, not
/// blocking this slice.</summary>
public class RoomService : IRoomService
{
    private readonly AbpsContext _context;
    private readonly ICurrentUserService _currentUser;
    private readonly IWhatsAppNotifier _whatsApp;
    private readonly ILogger<RoomService> _logger;

    public RoomService(AbpsContext context, ICurrentUserService currentUser, IWhatsAppNotifier whatsApp, ILogger<RoomService> logger)
    {
        _context = context;
        _currentUser = currentUser;
        _whatsApp = whatsApp;
        _logger = logger;
    }

    private async Task<(string? Name, string? PhoneNo)> GetGuestContactAsync(int bookingId)
    {
        var booking = await _context.TblBookingDetails
            .Where(x => x.BookingId == bookingId)
            .Select(x => new { x.Name, x.PhoneNo })
            .FirstOrDefaultAsync();
        return (booking?.Name, booking?.PhoneNo);
    }

    public async Task<List<RoomVM>> GetAllRoomsAsync()
    {
        try
        {
            var rooms = await (from r in _context.TblRooms
                                join rt in _context.TblRoomTypes on r.RtId equals rt.RtId
                                join fm in _context.TblFloorMasters on r.FloorId equals fm.FloorId into floorJoin
                                from fm in floorJoin.DefaultIfEmpty()
                                where r.RoomStatusId == (int)RoomStatus.Available
                                   && rt.RoomCategory != "DOR"
                                   && rt.RoomCategory != "LOC"
                                select new RoomVM
                                {
                                    RoomId = r.RoomId,
                                    RtId = r.RtId ?? 0,
                                    RoomNo = r.RoomNo ?? "",
                                    RtPrice = (decimal?)r.Cost,
                                    RoomAdvance = (decimal)r.AdvanceAmount,
                                    RoomCategory = rt.RoomCategory,
                                    FloorId = (short?)r.FloorId ?? 0,
                                    FloorDescription = fm != null ? fm.FloorDescription : "",
                                    Beds = r.Beds,
                                    Allowed = r.Allowed,
                                    IsAvailable = r.RoomStatusId == (int)RoomStatus.Available
                                }).ToListAsync();

            return rooms;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching rooms");
            return new List<RoomVM>();
        }
    }

    public async Task<List<RoomVM>> GetAllDormLockerRoomsAsync()
    {
        try
        {
            var rooms = await (from r in _context.TblRooms
                                join rt in _context.TblRoomTypes on r.RtId equals rt.RtId
                                join fm in _context.TblFloorMasters on r.FloorId equals fm.FloorId into floorJoin
                                from fm in floorJoin.DefaultIfEmpty()
                                where r.RoomStatusId == (int)RoomStatus.Available
                                   && (rt.RoomCategory == "DOR" || rt.RoomCategory == "LOC")
                                select new RoomVM
                                {
                                    RoomId = r.RoomId,
                                    RtId = r.RtId ?? 0,
                                    RoomNo = r.RoomNo ?? "",
                                    RtPrice = (decimal?)r.Cost,
                                    RoomAdvance = (decimal)r.AdvanceAmount,
                                    RoomCategory = rt.RoomCategory,
                                    FloorId = (short?)r.FloorId ?? 0,
                                    FloorDescription = fm != null ? fm.FloorDescription : "",
                                    Beds = r.Beds,
                                    Allowed = r.Allowed,
                                    IsAvailable = r.RoomStatusId == (int)RoomStatus.Available
                                }).ToListAsync();

            return rooms;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching dorm/locker rooms");
            return new List<RoomVM>();
        }
    }

    public async Task<RoomCounts> GetRoomCountsAsync()
    {
        try
        {
            var data = await (from r in _context.TblRooms
                               join rt in _context.TblRoomTypes on r.RtId equals rt.RtId
                               where rt.RoomCategory != "DOR" && rt.RoomCategory != "LOC"
                               select r)
                .GroupBy(x => 1)
                .Select(g => new
                {
                    Available = g.Count(x => x.RoomStatusId == (int)RoomStatus.Available),
                    Occupied = g.Count(x => x.RoomStatusId == (int)RoomStatus.Occupied)
                })
                .FirstOrDefaultAsync();

            return new RoomCounts(data?.Available ?? 0, data?.Occupied ?? 0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching room counts");
            return new RoomCounts(0, 0);
        }
    }

    public async Task<RoomCounts> GetDormLockerRoomCountsAsync()
    {
        try
        {
            var data = await (from r in _context.TblRooms
                               join rt in _context.TblRoomTypes on r.RtId equals rt.RtId
                               where rt.RoomCategory == "DOR" || rt.RoomCategory == "LOC"
                               select r)
                .GroupBy(x => 1)
                .Select(g => new
                {
                    Available = g.Count(x => x.RoomStatusId == (int)RoomStatus.Available),
                    Occupied = g.Count(x => x.RoomStatusId == (int)RoomStatus.Occupied)
                })
                .FirstOrDefaultAsync();

            return new RoomCounts(data?.Available ?? 0, data?.Occupied ?? 0);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching dorm/locker room counts");
            return new RoomCounts(0, 0);
        }
    }

    public async Task<DonarBondVM?> GetDonarByBondNoAsync(string bondNo)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(bondNo)) return null;

            int currentYear = DateTime.Now.Year;

            var result = await (from d in _context.TblDonars
                                 join dn in _context.TblDonations on d.DonarId equals dn.DonarId
                                 join dt in _context.TblDonarTypeMasters on dn.DonationTypeId equals dt.Dtid
                                 where dn.BondNo.ToLower() == bondNo.ToLower()
                                 orderby dn.DonationDate descending
                                 select new
                                 {
                                     d.DonarId,
                                     d.Name,
                                     d.TeluguName,
                                     d.PhoneNo,
                                     d.Aadhaar,
                                     d.Address,
                                     dn.BondNo,
                                     dn.RoomDays,
                                     dn.DonationAmount,
                                     dn.DonationDate,
                                     DonationType = dt.Donartype
                                 }).FirstOrDefaultAsync();

            if (result == null) return null;

            var bookingData = await (
                from bd in _context.TblBookingDetails
                join bl in _context.TblBookingLedgers on bd.BookingId equals bl.BookingId
                join br in _context.TblBookingRooms on bd.BookingId equals br.BookingId
                where bd.BondNo.ToLower() == bondNo.ToLower()
                      && bd.BookingDate.HasValue
                      && bd.BookingDate.Value.Year == currentYear
                      && (bd.BookingStatusId == (int)BookingStatus.CheckIn || bd.BookingStatusId == (int)BookingStatus.CheckOut)
                      && (br.BookingStatusId == (int)BookingStatus.CheckOut || br.BookingStatusId == (int)BookingStatus.CheckIn || br.BookingStatusId == (int)BookingStatus.Shifted)
                group br by new { bl.BookingId, bl.NoOfDays } into g
                select new { NoOfDays = g.Key.NoOfDays, RoomCount = g.Select(x => x.RoomId).Distinct().Count() }
            ).ToListAsync();

            int usedDays = bookingData.Sum(x => (x.NoOfDays ?? 0) * x.RoomCount);
            int remainingDays = Math.Max(0, result.RoomDays - usedDays);

            return new DonarBondVM(result.DonarId, result.Name, result.TeluguName, result.PhoneNo, result.Aadhaar,
                result.Address, result.BondNo, remainingDays, result.DonationAmount, (DateTime)result.DonationDate!, result.DonationType, usedDays);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching donor by bond number");
            return null;
        }
    }

    public async Task<CustomerLookupResult?> GetCustomerByAadharAsync(string aadhaar)
    {
        try
        {
            var result = await _context.TblBookingDetails
                .Where(x => x.AadharNo == aadhaar)
                .OrderByDescending(x => x.BookingDate)
                .Select(x => new CustomerLookupResult(x.Name, x.PhoneNo, x.Address, x.AadharNo))
                .FirstOrDefaultAsync();
            return result;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching customer by Aadhar");
            return null;
        }
    }

    public async Task<List<BookingCategory>> GetCategoriesAsync()
    {
        try
        {
            return await _context.TblBookingCategories
                .Select(c => new BookingCategory(c.CatId, c.Category ?? ""))
                .ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching booking categories");
            return new List<BookingCategory>();
        }
    }

    public async Task<OperationResult> SaveRoomDetailsAsync(SaveRoomDetailsRequest request)
    {
        try
        {
            _context.TblRooms.Add(new TblRoom
            {
                RoomNo = request.RoomNo,
                RtId = request.RoomTypeId,
                FloorId = request.FloorId,
                RoomStatusId = request.RoomStatusId,
                BookingId = request.BookingId,
                Cost = request.Price,
                AdvanceAmount = request.AdvanceAmount,
                Beds = request.Beds,
                Allowed = request.Allowed,
                CreatedBy = _currentUser.UserName
            });
            await _context.SaveChangesAsync();
            return new OperationResult(true, "Success");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving room details");
            return new OperationResult(false, "Error saving room details: " + ex.Message);
        }
    }

    public async Task<OperationResult> ModifyRoomDetailsAsync(ModifyRoomDetailsRequest request)
    {
        try
        {
            var room = await _context.TblRooms.FirstOrDefaultAsync(x => x.RoomId == request.RoomId);
            if (room == null) return new OperationResult(false, "Room not found");

            room.RoomNo = request.RoomNo;
            room.RtId = request.RoomTypeId;
            room.FloorId = request.FloorId;
            room.RoomStatusId = request.RoomStatusId;
            room.BookingId = request.BookingId;
            room.Cost = request.Price;
            room.AdvanceAmount = request.AdvanceAmount;
            room.Beds = request.Beds;
            room.Allowed = request.Allowed;
            room.CreatedBy = _currentUser.UserName;

            await _context.SaveChangesAsync();
            return new OperationResult(true, "Success");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error modifying room details");
            return new OperationResult(false, "Error modifying room details: " + ex.Message);
        }
    }

    public async Task<SaveBookingResult> SaveBookingAsync(BookingRequest req)
    {
        _logger.LogInformation("SaveBooking → BondNo={BondNo} Rooms={Rooms}", req.BondNo, req.NoOfRooms);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                DateTime now = DateTime.Now;

                // 1. INSERT BOOKING HEADER
                var booking = new TblBookingDetail
                {
                    BookingDate = now,
                    Name = req.Name,
                    AadharNo = req.AadhaarNo,
                    PhoneNo = req.PhoneNo,
                    NoPeople = req.NoOfPeople,
                    Address = req.Address,

                    CatId = req.CatID,
                    Details = req.Details,
                    Dtid = req.DTID,
                    DonarId = req.DonarID,
                    BondNo = req.BondNo,

                    NoOfRooms = req.NoOfRooms,
                    Advance = req.Advance,
                    TotalAmount = req.TotalAmount,
                    GroupBooking = req.GroupBooking,
                    AdvanceBooking = req.AdvanceBooking,
                    BookingStatusId = (int)BookingStatus.CheckIn,

                    CreatedBy = _currentUser.UserName,
                    CreatedTimestamp = now
                };

                _context.TblBookingDetails.Add(booking);
                await _context.SaveChangesAsync();

                int bookingId = booking.BookingId;

                // 2. INSERT BOOKING ROOMS
                foreach (var room in req.Rooms)
                {
                    // FOR UPDATE: locks the room row for the rest of this transaction so a
                    // concurrent booking of the same room blocks here and then fails the
                    // availability check below — prevents double-booking with multiple users.
                    // (PostgreSQL equivalent of SQL Server's WITH (UPDLOCK, ROWLOCK); identifiers
                    // are quoted because EF creates them case-preserved.)
                    var dbRoom = (await _context.TblRooms
                        .FromSqlInterpolated($"SELECT * FROM \"tbl_Room\" WHERE \"Room_ID\" = {room.RoomId} FOR UPDATE")
                        .ToListAsync())
                        .FirstOrDefault();

                    if (dbRoom == null || dbRoom.RoomStatusId != (int)RoomStatus.Available)
                        throw new Exception($"Room {room.RoomNo} already booked!");

                    _context.TblBookingRooms.Add(new TblBookingRoom
                    {
                        BookingId = bookingId,
                        BookingDate = now,
                        RoomId = room.RoomId,
                        GroupBooking = req.GroupBooking,
                        RoomAmount = room.RtPrice,
                        BookingStatusId = (int)BookingStatus.CheckIn,
                        CreatedBy = _currentUser.UserName,
                        CreatedTimestamp = now
                    });

                    dbRoom.RoomStatusId = (int)RoomStatus.Occupied;
                    dbRoom.BookingId = bookingId;
                }

                await _context.SaveChangesAsync();

                // 3. INSERT BOOKING LEDGER
                var ledger = new TblBookingLedger
                {
                    BookingId = bookingId,
                    NoOfDays = req.NoOfDays,
                    CheckinDateTime = req.CheckinDate,
                    CheckinInsertDatetime = DateTime.Now,
                    CheckinByuser = _currentUser.UserName,

                    Advance = req.Advance,
                    IntendedTotalAmount = req.TotalAmount,
                    IntendedRefundAmount = 0,

                    CreatedBy = _currentUser.UserName,
                    CreatedTimestamp = now
                };
                _context.TblBookingLedgers.Add(ledger);

                // 4. INSERT TRANSACTION (ADVANCE PAYMENT)
                _context.TblTransactions.Add(new TblTransaction
                {
                    TransactionCategoryId = (int)TransactionCategory.RoomBooking,
                    DonarBookingId = bookingId,
                    TrasactionTypeId = (int)TransactionType.Advance,
                    TransactionDate = now,
                    Amount = req.Advance,

                    PaymentTypeId = req.PaymentTypeID,
                    PaymentSubTypeId = req.PaymentSubTypeID,
                    ReferenceNumber = req.TrasactionNo,
                    Notes = "Advance Payment",

                    CreatedBy = _currentUser.UserName,
                    CreatedTimestamp = now
                });

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                _logger.LogInformation("SaveBooking ← BookingId={BookingId}", bookingId);

                decimal perDay = req.Rooms.Sum(r => r.RtPrice ?? 0);
                decimal balance = req.TotalAmount - req.Advance;
                _ = _whatsApp.NotifyAsync(req.PhoneNo,
                    $"Hi {req.Name}, room {string.Join(", ", req.Rooms.Select(r => r.RoomNo))} booked (Booking ID {bookingId}). " +
                    $"Rate ₹{perDay:N2}/day, Total ₹{req.TotalAmount:N2}, Advance paid ₹{req.Advance:N2}, Balance due ₹{balance:N2}.");

                return new SaveBookingResult(true, "Booking completed successfully", bookingId);
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error saving booking (rolled back)");
                return new SaveBookingResult(false, ex.Message, 0);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error saving booking");
            return new SaveBookingResult(false, "Error saving booking: " + ex.Message, 0);
        }
    }

    public async Task<int> GetBookingIdByRoomNoAsync(string roomNo)
    {
        try
        {
            return await (from bookingrooms in _context.TblBookingRooms
                          join bookingdetails in _context.TblBookingDetails on bookingrooms.BookingId equals bookingdetails.BookingId
                          join room in _context.TblRooms on bookingrooms.RoomId equals room.RoomId
                          join bookingstatus in _context.TblBookingStatuses on bookingdetails.BookingStatusId equals bookingstatus.BookingStatusId
                          where room.RoomNo.ToLower() == roomNo.ToLower()
                             && bookingstatus.BookingStatusDesc == "CheckIn"
                             && bookingrooms.BookingStatusId == (int)BookingStatus.CheckIn
                          orderby bookingrooms.BookingRoomId descending
                          select bookingdetails.BookingId).FirstOrDefaultAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching booking ID by room number");
            return 0;
        }
    }

    public async Task<bool> ValidateActiveBookingAsync(int bookingId)
    {
        try
        {
            return await _context.TblBookingDetails.AnyAsync(x => x.BookingId == bookingId && x.BookingStatusId == (int)BookingStatus.CheckIn);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating active booking");
            return false;
        }
    }

    public async Task<BookingSummary?> GetBookingDetailsAsync(int bookingId)
    {
        try
        {
            var booking = await _context.TblBookingDetails
                .Where(x => x.BookingId == bookingId)
                .Select(x => new { x.BookingId, x.BookingDate, x.Name, x.PhoneNo, x.Details, x.Advance, x.TotalAmount })
                .FirstOrDefaultAsync();
            if (booking == null) return null;

            var checkinDateTime = await _context.TblBookingLedgers
                .Where(l => l.BookingId == bookingId)
                .Select(l => l.CheckinDateTime)
                .FirstOrDefaultAsync();

            return new BookingSummary(booking.BookingId, booking.BookingDate ?? default, booking.Name, booking.PhoneNo,
                booking.Details, booking.Advance, booking.TotalAmount, checkinDateTime);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching booking details by booking ID");
            return null;
        }
    }

    public async Task<List<BookingRoomLine>> GetBookingRoomsAsync(int bookingId)
    {
        try
        {
            return await (from br in _context.TblBookingRooms
                          join r in _context.TblRooms on br.RoomId equals r.RoomId
                          where br.BookingId == bookingId && br.BookingStatusId == (int)BookingStatus.CheckIn
                          select new BookingRoomLine(r.RoomId, r.RoomNo, br.RoomAmount ?? 0)).ToListAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error fetching booking rooms");
            return new List<BookingRoomLine>();
        }
    }

    public async Task<OperationResult> VacateRoomAsync(VacateRoomRequest req)
    {
        _logger.LogInformation("VacateRoom → BookingId={BookingId} Days={Days}", req.BookingId, req.NoOfDays);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                // 1. UPDATE BOOKING LEDGER
                var ledger = await _context.TblBookingLedgers.FirstOrDefaultAsync(x => x.BookingId == req.BookingId);
                if (ledger != null)
                {
                    ledger.CheckoutDateTime = req.CheckoutDateTime;
                    ledger.ActualTotalAmount = req.AdjustedAmount;
                    ledger.ActualRefundAmount = req.RefundAmount;
                    ledger.CheckoutByuser = _currentUser.UserName;
                    ledger.CheckoutInsertDatetime = DateTime.Now;
                    ledger.NoOfDays = req.NoOfDays;
                }

                // 2. UPDATE BOOKING STATUS
                var booking = await _context.TblBookingDetails.FirstOrDefaultAsync(x => x.BookingId == req.BookingId);
                if (booking != null)
                {
                    booking.BookingStatusId = (int)BookingStatus.CheckOut;
                }

                // 3. UPDATE ROOMS TO AVAILABLE
                var rooms = await _context.TblBookingRooms
                    .Where(br => br.BookingId == req.BookingId && br.BookingStatusId == (int)BookingStatus.CheckIn)
                    .ToListAsync();
                foreach (var room in rooms)
                {
                    _context.TblVacateRooms.Add(new TblVacateRoom
                    {
                        BookingId = req.BookingId,
                        RoomId = room.RoomId,
                        CheckoutDateTime = req.CheckoutDateTime,
                        CreatedBy = _currentUser.UserName,
                        CreatedTimestamp = DateTime.Now
                    });

                    room.CheckOutDate = req.CheckoutDateTime;
                    room.BookingStatusId = (int)BookingStatus.CheckOut;
                    room.ModifiedBy = _currentUser.UserName;
                    room.ModifiedTimestamp = DateTime.Now;

                    var roomStatus = await _context.TblRooms.FirstOrDefaultAsync(x => x.RoomId == room.RoomId);
                    if (roomStatus != null)
                    {
                        roomStatus.BookingId = 0;
                        roomStatus.RoomStatusId = (int)RoomStatus.Available;
                    }
                }

                // 4. INSERT TRANSACTION (REFUND OR ADDITIONAL PAYMENT)
                if (req.RefundAmount > 0)
                {
                    _context.TblTransactions.Add(new TblTransaction
                    {
                        TransactionCategoryId = (int)TransactionCategory.RoomBooking,
                        DonarBookingId = req.BookingId,
                        TrasactionTypeId = (int)TransactionType.Refund,
                        TransactionDate = DateTime.Now,
                        Amount = req.RefundAmount,
                        PaymentTypeId = req.PaymentType == "Cash" ? (int)PaymentType.Cash : (int)PaymentType.Online,
                        PaymentSubTypeId = req.PaymentSubTypeId,
                        ReferenceNumber = req.TransactionNo,
                        Notes = "Refund on checkout",
                        CreatedBy = _currentUser.UserName,
                        CreatedTimestamp = DateTime.Now
                    });
                }
                else if (req.PayAmount > 0)
                {
                    _context.TblTransactions.Add(new TblTransaction
                    {
                        TransactionCategoryId = (int)TransactionCategory.RoomBooking,
                        DonarBookingId = req.BookingId,
                        TrasactionTypeId = (int)TransactionType.Payment,
                        TransactionDate = DateTime.Now,
                        Amount = req.PayAmount,
                        PaymentTypeId = req.PaymentType == "Cash" ? (int)PaymentType.Cash : (int)PaymentType.Online,
                        PaymentSubTypeId = req.PaymentSubTypeId,
                        ReferenceNumber = req.TransactionNo,
                        Notes = "Additional payment on checkout",
                        CreatedBy = _currentUser.UserName,
                        CreatedTimestamp = DateTime.Now
                    });
                }

                _context.TblTransactionRoomDonations.Add(new TblTransactionRoomDonation
                {
                    BookingId = req.BookingId,
                    CheckoutDate = req.CheckoutDateTime,
                    NoofDays = req.NoOfDays,
                    TotalAmount = req.AdjustedAmount,
                    RoomAmount = req.RoomDonation,
                    DonationAmount = req.OtherDonation,
                    Notes = "Room checkout details",
                    CreatedBy = _currentUser.UserName,
                    CreatedTimestamp = DateTime.Now
                });

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                // Structured settlement line — one grep solves any reconciliation dispute.
                _logger.LogInformation(
                    "SETTLEMENT VACATE booking={BookingId} total={Total:F2} refund={Refund:F2} pay={Pay:F2} roomDon={RoomDon:F2} otherDon={OtherDon:F2} days={Days} mode={Mode} user={User}",
                    req.BookingId, req.AdjustedAmount, req.RefundAmount, req.PayAmount, req.RoomDonation, req.OtherDonation, req.NoOfDays, req.PaymentType, _currentUser.UserName);

                string settleLine = req.RefundAmount > 0 ? $"Refund ₹{req.RefundAmount:N2}"
                    : req.PayAmount > 0 ? $"Additional amount collected ₹{req.PayAmount:N2}"
                    : "Fully settled";
                _ = _whatsApp.NotifyAsync(booking?.PhoneNo,
                    $"Hi {booking?.Name}, thank you for staying with us. Total ₹{req.AdjustedAmount:N2} for {req.NoOfDays} day(s), " +
                    $"Advance received ₹{booking?.Advance ?? 0:N2}, {settleLine}.");

                return new OperationResult(true, "Success");
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error vacating room (inner)");
                return new OperationResult(false, "Error vacating room: " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error vacating room");
            return new OperationResult(false, "Error vacating room: " + ex.Message);
        }
    }

    public async Task<OperationResult> ExtendRoomAsync(ExtendRoomRequest req)
    {
        _logger.LogInformation("ExtendRoom → BookingId={BookingId} ExtendDays={ExtendDays}", req.BookingId, req.ExtendDays);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                var ledger = await _context.TblBookingLedgers.FirstOrDefaultAsync(x => x.BookingId == req.BookingId)
                    ?? throw new Exception("Booking ledger not found!");

                ledger.NoOfDays = req.TotalExtendDays;
                ledger.IntendedTotalAmount = req.UpdateTotalAmount;
                ledger.Advance = req.TotalAdvance;
                ledger.ModifiedBy = _currentUser.UserName;
                ledger.ModifiedTimestamp = DateTime.Now;

                var booking = await _context.TblBookingDetails.FirstOrDefaultAsync(x => x.BookingId == req.BookingId)
                    ?? throw new Exception("Booking not found!");
                decimal prevAmount = booking.TotalAmount ?? 0;
                booking.TotalAmount = req.UpdateTotalAmount;
                booking.Advance = req.TotalAdvance;

                _context.TblExtendrooms.Add(new TblExtendroom
                {
                    Bookingid = req.BookingId,
                    Prevnoofdays = req.NoOfDays,
                    Precheckout = req.OldCheckout,
                    Extendeddays = req.ExtendDays,
                    Newcheckout = req.NewCheckout,
                    Totaldays = req.TotalExtendDays,
                    CreatedBy = _currentUser.UserName,
                    CreatedDate = DateTime.Now
                });

                if (req.AddAdvance > 0)
                {
                    _context.TblTransactions.Add(new TblTransaction
                    {
                        TransactionCategoryId = (int)TransactionCategory.RoomBooking,
                        DonarBookingId = req.BookingId,
                        TrasactionTypeId = (int)TransactionType.Advance,
                        TransactionDate = DateTime.Now,
                        Amount = req.AddAdvance,
                        PaymentTypeId = req.PaymentTypeId,
                        PaymentSubTypeId = req.PaymentSubTypeId,
                        ReferenceNumber = req.TransactionNo,
                        Notes = "Extending Room",
                        CreatedBy = _currentUser.UserName,
                        CreatedTimestamp = DateTime.Now
                    });
                }

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                decimal perDayAmount = req.NoOfDays > 0 ? prevAmount / req.NoOfDays : 0;
                _ = _whatsApp.NotifyAsync(booking.PhoneNo,
                    $"Hi {booking.Name}, stay extended to {req.NewCheckout:dd-MMM-yyyy HH:mm}. Rate ₹{perDayAmount:N2}/day, " +
                    $"New total ₹{req.UpdateTotalAmount:N2}, Additional advance paid ₹{req.AddAdvance:N2}, Total advance now ₹{req.TotalAdvance:N2}.");

                return new OperationResult(true, "Success");
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error extending room");
                return new OperationResult(false, "Error extending the room: " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error extending room");
            return new OperationResult(false, "Error extending the room: " + ex.Message);
        }
    }

    public async Task<OperationResult> ShiftRoomAsync(ShiftRoomRequest req)
    {
        _logger.LogInformation("ShiftRoom → BookingId={BookingId} From={From} To={To}", req.BookingId, req.FromRoomId, req.ToRoomId);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.TblShiftRooms.Add(new TblShiftRoom
                {
                    NewRoomId = req.ToRoomId,
                    OldRoomId = req.FromRoomId,
                    CreatedBy = _currentUser.UserName,
                    BookingId = req.BookingId,
                    CreatedTimestamp = DateTime.Now
                });

                var fromRoomGroupBooking = (await _context.TblBookingRooms
                    .FirstOrDefaultAsync(x => x.BookingId == req.BookingId && x.RoomId == req.FromRoomId))?.GroupBooking ?? "N";
                var toRoomCost = (await _context.TblRooms.FirstOrDefaultAsync(x => x.RoomId == req.ToRoomId))?.Cost ?? 0;

                _context.TblBookingRooms.Add(new TblBookingRoom
                {
                    BookingId = req.BookingId,
                    BookingDate = DateTime.Now,
                    RoomId = req.ToRoomId,
                    GroupBooking = fromRoomGroupBooking,
                    RoomAmount = toRoomCost,
                    BookingStatusId = (int)BookingStatus.CheckIn,
                    CreatedBy = _currentUser.UserName,
                    CreatedTimestamp = DateTime.Now
                });

                var fromBookingRoom = await _context.TblBookingRooms.FirstOrDefaultAsync(x => x.RoomId == req.FromRoomId && x.BookingId == req.BookingId);
                if (fromBookingRoom != null)
                {
                    fromBookingRoom.CheckOutDate = req.Checkout;
                    fromBookingRoom.BookingStatusId = (int)BookingStatus.Shifted;
                }
                await _context.SaveChangesAsync();

                var fromRoom = await _context.TblRooms.FirstOrDefaultAsync(x => x.RoomId == req.FromRoomId);
                if (fromRoom != null)
                {
                    fromRoom.BookingId = 0;
                    fromRoom.RoomStatusId = (int)RoomStatus.Available;
                }
                var toRoom = await _context.TblRooms.FirstOrDefaultAsync(x => x.RoomId == req.ToRoomId);
                if (toRoom != null)
                {
                    toRoom.BookingId = req.BookingId;
                    toRoom.RoomStatusId = (int)RoomStatus.Occupied;
                }
                var ledger = await _context.TblBookingLedgers.FirstOrDefaultAsync(x => x.BookingId == req.BookingId);
                if (ledger != null)
                {
                    ledger.Advance = req.TotalAdvance;
                    ledger.IntendedTotalAmount = req.TotalAmount;
                }
                var booking = await _context.TblBookingDetails.FirstOrDefaultAsync(x => x.BookingId == req.BookingId);
                if (booking != null)
                {
                    booking.TotalAmount = req.TotalAmount;
                    booking.Advance = req.TotalAdvance;
                }

                if (req.AddAdvance > 0)
                {
                    _context.TblTransactions.Add(new TblTransaction
                    {
                        TransactionCategoryId = (int)TransactionCategory.RoomBooking,
                        DonarBookingId = req.BookingId,
                        TrasactionTypeId = (int)TransactionType.Advance,
                        TransactionDate = DateTime.Now,
                        Amount = req.AddAdvance,
                        PaymentTypeId = req.PaymentTypeId,
                        PaymentSubTypeId = req.PaymentSubTypeId,
                        ReferenceNumber = req.TransactionNo,
                        Notes = "Additional advance for shift room",
                        CreatedBy = _currentUser.UserName,
                        CreatedTimestamp = DateTime.Now
                    });
                }

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                _ = _whatsApp.NotifyAsync(booking?.PhoneNo,
                    $"Hi {booking?.Name}, moved to room {toRoom?.RoomNo} (₹{toRoomCost:N2}/day). " +
                    $"New total ₹{req.TotalAmount:N2}, Additional advance ₹{req.AddAdvance:N2}, Total advance now ₹{req.TotalAdvance:N2}.");

                return new OperationResult(true, "Success");
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error shifting room");
                return new OperationResult(false, "Error shifting the room: " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error shifting room");
            return new OperationResult(false, "Error shifting the room: " + ex.Message);
        }
    }

    public async Task<OperationResult> PartialCheckoutAsync(PartialCheckoutRequest req)
    {
        _logger.LogInformation("PartialCheckout → BookingId={BookingId} Rooms={Rooms}", req.BookingId, req.SelectedRoomIds.Count);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            var checkedOutRooms = new List<(string? RoomNo, decimal PerDay)>();
            foreach (var roomId in req.SelectedRoomIds)
            {
                var bookingRoom = await _context.TblBookingRooms.FirstOrDefaultAsync(x => x.RoomId == roomId && x.BookingId == req.BookingId);
                if (bookingRoom != null)
                {
                    bookingRoom.CheckOutDate = req.Checkout;
                    bookingRoom.BookingStatusId = (int)BookingStatus.CheckOut;
                }

                var room = await _context.TblRooms.FirstOrDefaultAsync(x => x.RoomId == roomId && x.BookingId == req.BookingId);
                if (room != null)
                {
                    checkedOutRooms.Add((room.RoomNo, room.Cost));
                    room.BookingId = 0;
                    room.RoomStatusId = (int)RoomStatus.Available;
                }
            }

            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            var (guestName, guestPhone) = await GetGuestContactAsync(req.BookingId);
            string roomList = string.Join(", ", checkedOutRooms.Select(r => $"{r.RoomNo} (₹{r.PerDay:N2}/day)"));
            _ = _whatsApp.NotifyAsync(guestPhone,
                $"Hi {guestName}, room(s) {roomList} checked out. Final settlement will be done at checkout of the remaining room(s).");

            return new OperationResult(true, "Success");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during partial checkout of room");
            return new OperationResult(false, "Error adding the room: " + ex.Message);
        }
    }

    public async Task<OperationResult> CancelRoomAsync(CancelRoomRequest req)
    {
        _logger.LogInformation("CancelRoom → BookingId={BookingId} Rooms={Rooms}", req.BookingId, req.SelectedRoomIds.Count);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            foreach (var roomId in req.SelectedRoomIds)
            {
                var bookingRoom = await _context.TblBookingRooms.FirstOrDefaultAsync(x => x.RoomId == roomId && x.BookingId == req.BookingId);
                if (bookingRoom != null)
                {
                    bookingRoom.CheckOutDate = req.Checkout;
                    bookingRoom.BookingStatusId = (int)BookingStatus.Cancelled;
                }

                var room = await _context.TblRooms.FirstOrDefaultAsync(x => x.RoomId == roomId && x.BookingId == req.BookingId);
                if (room != null)
                {
                    room.BookingId = 0;
                    room.RoomStatusId = (int)RoomStatus.Available;
                }

                _context.TblCancelledRooms.Add(new TblCancelledRoom
                {
                    BookingId = req.BookingId,
                    RoomId = roomId,
                    CancelationDate = req.Checkout,
                    CreatedBy = _currentUser.UserName,
                    CreatedTimestamp = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();

            // If no active rooms remain (CheckIn/Booked/Reserved), close the booking header
            // so an all-cancelled booking cannot be vacated later.
            bool anyActiveRooms = await _context.TblBookingRooms.AnyAsync(x => x.BookingId == req.BookingId
                && (x.BookingStatusId == (int)BookingStatus.CheckIn
                 || x.BookingStatusId == (int)BookingStatus.Booked
                 || x.BookingStatusId == (int)BookingStatus.Reserved));
            if (!anyActiveRooms)
            {
                var booking = await _context.TblBookingDetails.FirstOrDefaultAsync(x => x.BookingId == req.BookingId);
                if (booking != null) booking.BookingStatusId = (int)BookingStatus.Cancelled;

                var ledger = await _context.TblBookingLedgers.FirstOrDefaultAsync(x => x.BookingId == req.BookingId);
                if (ledger != null && ledger.CheckoutDateTime == null)
                {
                    ledger.CheckoutDateTime = req.Checkout;
                    ledger.CheckoutByuser = _currentUser.UserName;
                    ledger.CheckoutInsertDatetime = DateTime.Now;
                }
                await _context.SaveChangesAsync();
                _logger.LogInformation("All rooms cancelled — booking {BookingId} header marked Cancelled", req.BookingId);
            }

            await tx.CommitAsync();

            var (guestName, guestPhone) = await GetGuestContactAsync(req.BookingId);
            _ = _whatsApp.NotifyAsync(guestPhone,
                $"Hi {guestName}, {req.SelectedRoomIds.Count} room(s) cancelled from booking {req.BookingId}.");

            return new OperationResult(true, "Success");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during cancellation of room");
            return new OperationResult(false, "Error adding the room: " + ex.Message);
        }
    }

    public async Task<OperationResult> CancelCompleteRoomAsync(CancelCompleteRoomRequest req)
    {
        _logger.LogInformation("CancelCompleteRoom → BookingId={BookingId} Rooms={Rooms}", req.BookingId, req.SelectedRoomIds.Count);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();

            var ledger = await _context.TblBookingLedgers.FirstOrDefaultAsync(x => x.BookingId == req.BookingId)
                ?? throw new Exception("Booking ledger not found!");
            ledger.CheckoutDateTime = req.Checkout;
            ledger.ActualTotalAmount = req.Payment;
            ledger.ActualRefundAmount = req.Refund;
            ledger.CheckoutByuser = _currentUser.UserName;
            ledger.CheckoutInsertDatetime = DateTime.Now;

            var booking = await _context.TblBookingDetails.FirstOrDefaultAsync(x => x.BookingId == req.BookingId)
                ?? throw new Exception("Booking not found!");
            booking.BookingStatusId = (int)BookingStatus.Cancelled;

            foreach (var roomId in req.SelectedRoomIds)
            {
                var bookingRoom = await _context.TblBookingRooms.FirstOrDefaultAsync(x => x.RoomId == roomId && x.BookingId == req.BookingId);
                if (bookingRoom != null)
                {
                    bookingRoom.CheckOutDate = req.Checkout;
                    bookingRoom.BookingStatusId = (int)BookingStatus.Cancelled;
                }

                var room = await _context.TblRooms.FirstOrDefaultAsync(x => x.RoomId == roomId && x.BookingId == req.BookingId);
                if (room != null)
                {
                    room.BookingId = 0;
                    room.RoomStatusId = (int)RoomStatus.Available;
                }

                _context.TblCancelledRooms.Add(new TblCancelledRoom
                {
                    BookingId = req.BookingId,
                    RoomId = roomId,
                    CancelationDate = req.Checkout,
                    CreatedBy = _currentUser.UserName,
                    CreatedTimestamp = DateTime.Now
                });

                await _context.SaveChangesAsync();
            }

            if (req.Refund > 0)
            {
                _context.TblTransactions.Add(new TblTransaction
                {
                    TransactionCategoryId = (int)TransactionCategory.RoomBooking,
                    DonarBookingId = req.BookingId,
                    TrasactionTypeId = (int)TransactionType.Refund,
                    TransactionDate = DateTime.Now,
                    Amount = req.Refund,
                    PaymentTypeId = req.PaymentTypeId,
                    PaymentSubTypeId = req.PaymentSubTypeId,
                    ReferenceNumber = req.TransactionNo,
                    Notes = "Cancelled Room",
                    CreatedBy = _currentUser.UserName,
                    CreatedTimestamp = DateTime.Now
                });
            }
            else if (req.Payment > 0)
            {
                _context.TblTransactions.Add(new TblTransaction
                {
                    TransactionCategoryId = (int)TransactionCategory.RoomBooking,
                    DonarBookingId = req.BookingId,
                    TrasactionTypeId = (int)TransactionType.Payment,
                    TransactionDate = DateTime.Now,
                    Amount = req.Payment,
                    PaymentTypeId = req.PaymentTypeId,
                    PaymentSubTypeId = req.PaymentSubTypeId,
                    ReferenceNumber = req.TransactionNo,
                    Notes = "Additional payment on Cancelled Room",
                    CreatedBy = _currentUser.UserName,
                    CreatedTimestamp = DateTime.Now
                });
            }

            await _context.SaveChangesAsync();
            await tx.CommitAsync();

            _logger.LogInformation(
                "SETTLEMENT CANCEL booking={BookingId} rooms={Rooms} refund={Refund:F2} pay={Pay:F2} user={User}",
                req.BookingId, req.SelectedRoomIds.Count, req.Refund, req.Payment, _currentUser.UserName);

            decimal advance = booking.Advance ?? 0;
            decimal payable = advance - req.Refund + req.Payment;
            string settleLine = req.Refund > 0 ? $"Refund ₹{req.Refund:N2}."
                : req.Payment > 0 ? $"Additional amount collected ₹{req.Payment:N2}."
                : "Fully settled.";
            _ = _whatsApp.NotifyAsync(booking.PhoneNo,
                $"Hi {booking.Name}, booking {req.BookingId} cancelled. Amount ₹{payable:N2}, Advance received ₹{advance:N2}, {settleLine}");

            return new OperationResult(true, "Success");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during complete cancellation of room");
            return new OperationResult(false, "Error adding the room: " + ex.Message);
        }
    }

    public async Task<OperationResult> AddAdditionalRoomAsync(AddAdditionalRoomRequest req)
    {
        _logger.LogInformation("AddAdditionalRoom → BookingId={BookingId} Rooms={Rooms}", req.BookingId, req.SelectedRoomIds.Count);
        try
        {
            using var tx = await _context.Database.BeginTransactionAsync();
            try
            {
                _context.TblAddonRooms.Add(new TblAddonRoom
                {
                    BookingId = req.BookingId,
                    AddonBookingDate = req.BookingDate,
                    AddAdvance = req.AddAdvance,
                    TotalAdvance = req.TotalAdvance,
                    TotalAmount = req.UpdateTotalAmount,
                    Createdby = _currentUser.UserName,
                    CreateTimestmap = DateTime.Now
                });

                var booking = await _context.TblBookingDetails.FirstOrDefaultAsync(x => x.BookingId == req.BookingId);
                if (booking != null)
                {
                    booking.TotalAmount = req.UpdateTotalAmount;
                    booking.Advance = req.TotalAdvance;
                }

                _context.TblTransactions.Add(new TblTransaction
                {
                    TransactionCategoryId = (int)TransactionCategory.RoomBooking,
                    DonarBookingId = req.BookingId,
                    TrasactionTypeId = (int)TransactionType.Advance,
                    TransactionDate = DateTime.Now,
                    Amount = req.AddAdvance,
                    PaymentTypeId = req.PaymentTypeId,
                    PaymentSubTypeId = req.PaymentSubTypeId,
                    ReferenceNumber = req.TransactionNo,
                    Notes = "Additional advance for addon room",
                    CreatedBy = _currentUser.UserName,
                    CreatedTimestamp = DateTime.Now
                });

                var ledger = await _context.TblBookingLedgers.FirstOrDefaultAsync(x => x.BookingId == req.BookingId);
                if (ledger != null)
                {
                    ledger.IntendedTotalAmount = req.UpdateTotalAmount;
                    ledger.Advance = req.TotalAdvance;
                }

                var addedRoomNos = new List<string?>();
                foreach (var roomId in req.SelectedRoomIds)
                {
                    var room = await _context.TblRooms.FirstOrDefaultAsync(x => x.RoomId == roomId);
                    if (room != null)
                    {
                        addedRoomNos.Add(room.RoomNo);
                        room.RoomStatusId = (int)RoomStatus.Occupied;
                        room.BookingId = req.BookingId;

                        _context.TblBookingRooms.Add(new TblBookingRoom
                        {
                            BookingId = req.BookingId,
                            BookingDate = req.BookingDate,
                            BookingStatusId = (int)BookingStatus.CheckIn,
                            RoomId = roomId,
                            GroupBooking = "Y",
                            RoomAmount = room.Cost,
                            CreatedBy = _currentUser.UserName,
                            CreatedTimestamp = DateTime.Now
                        });
                    }
                }

                await _context.SaveChangesAsync();
                await tx.CommitAsync();

                _ = _whatsApp.NotifyAsync(booking?.PhoneNo,
                    $"Hi {booking?.Name}, room {string.Join(", ", addedRoomNos)} added to booking {req.BookingId}. " +
                    $"New total ₹{req.UpdateTotalAmount:N2}, Additional advance paid ₹{req.AddAdvance:N2}, Total advance now ₹{req.TotalAdvance:N2}.");

                return new OperationResult(true, "Success");
            }
            catch (Exception ex)
            {
                await tx.RollbackAsync();
                _logger.LogError(ex, "Error adding additional room (inner)");
                return new OperationResult(false, "Error adding the room: " + ex.Message);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error adding additional room");
            return new OperationResult(false, "Error adding the room: " + ex.Message);
        }
    }

    public async Task SaveBookingDocumentAsync(int bookingId, string documentType, string? fileName, string? contentType, byte[] fileData)
    {
        _context.TblBookingDocuments.Add(new TblBookingDocument
        {
            BookingId = bookingId,
            DocumentType = documentType,
            FileName = fileName,
            ContentType = contentType,
            FileData = fileData,
            CreatedBy = _currentUser.UserName,
            CreatedTimestamp = DateTime.Now
        });
        await _context.SaveChangesAsync();
    }
}

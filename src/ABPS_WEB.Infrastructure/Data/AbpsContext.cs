using System;
using System.Collections.Generic;
using ABPS_WEB.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace ABPS_WEB.Infrastructure.Data;

public partial class AbpsContext : DbContext
{
    public AbpsContext()
    {
    }

    public AbpsContext(DbContextOptions<AbpsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<DumpAnnad> DumpAnnads { get; set; }

    public virtual DbSet<DumpDona> DumpDonas { get; set; }

    public virtual DbSet<DumpDonash> DumpDonashs { get; set; }

    public virtual DbSet<DumpDonationTypeMapping> DumpDonationTypeMappings { get; set; }

    public virtual DbSet<DumpDonlist> DumpDonlists { get; set; }

    public virtual DbSet<DumpMember> DumpMembers { get; set; }

    public virtual DbSet<DumpOccasionMapping> DumpOccasionMappings { get; set; }

    public virtual DbSet<DumpTblDonar> DumpTblDonars { get; set; }

    public virtual DbSet<DumpTblDonation> DumpTblDonations { get; set; }

    public virtual DbSet<DumpTblDonationOccation> DumpTblDonationOccations { get; set; }

    public virtual DbSet<DumpTeluguMasamMap> DumpTeluguMasamMaps { get; set; }

    public virtual DbSet<DumpThidhiMap> DumpThidhiMaps { get; set; }

    public virtual DbSet<PakshamMap> PakshamMaps { get; set; }

    public virtual DbSet<TblAddonRoom> TblAddonRooms { get; set; }

    public virtual DbSet<TblAdvanceBooking> TblAdvanceBookings { get; set; }

    public virtual DbSet<TblBoardMember> TblBoardMembers { get; set; }

    public virtual DbSet<TblBookingCategory> TblBookingCategories { get; set; }

    public virtual DbSet<TblBookingDetail> TblBookingDetails { get; set; }

    public virtual DbSet<TblBookingDocument> TblBookingDocuments { get; set; }

    public virtual DbSet<TblBookingLedger> TblBookingLedgers { get; set; }

    public virtual DbSet<TblBookingRoom> TblBookingRooms { get; set; }

    public virtual DbSet<TblBookingStatus> TblBookingStatuses { get; set; }

    public virtual DbSet<TblCancelledRoom> TblCancelledRooms { get; set; }

    public virtual DbSet<TblCountry> TblCountries { get; set; }

    public virtual DbSet<TblDistrict> TblDistricts { get; set; }

    public virtual DbSet<TblDonar> TblDonars { get; set; }

    public virtual DbSet<TblDonarTypeMaster> TblDonarTypeMasters { get; set; }

    public virtual DbSet<TblDonation> TblDonations { get; set; }

    public virtual DbSet<TblDonationOccation> TblDonationOccations { get; set; }

    public virtual DbSet<TblErrorLog> TblErrorLogs { get; set; }

    public virtual DbSet<TblExtendroom> TblExtendrooms { get; set; }

    public virtual DbSet<TblFloorMaster> TblFloorMasters { get; set; }

    public virtual DbSet<TblOccassionMaster> TblOccassionMasters { get; set; }

    public virtual DbSet<TblOperator> TblOperators { get; set; }

    public virtual DbSet<TblPaymentSubTypeMaster> TblPaymentSubTypeMasters { get; set; }

    public virtual DbSet<TblPaymentTypeMaster> TblPaymentTypeMasters { get; set; }

    public virtual DbSet<TblRoom> TblRooms { get; set; }

    public virtual DbSet<TblRoomMasterOld> TblRoomMasterOlds { get; set; }

    public virtual DbSet<TblRoomStatus> TblRoomStatuses { get; set; }

    public virtual DbSet<TblRoomType> TblRoomTypes { get; set; }

    public virtual DbSet<TblRoomTypeMasterOld> TblRoomTypeMasterOlds { get; set; }

    public virtual DbSet<TblShiftRoom> TblShiftRooms { get; set; }

    public virtual DbSet<TblSmstemplate> TblSmstemplates { get; set; }

    public virtual DbSet<TblSmstransaction> TblSmstransactions { get; set; }

    public virtual DbSet<TblState> TblStates { get; set; }

    public virtual DbSet<TblTeluguMasam> TblTeluguMasams { get; set; }

    public virtual DbSet<TblTeluguPaksham> TblTeluguPakshams { get; set; }

    public virtual DbSet<TblTeluguThidi> TblTeluguThidis { get; set; }

    public virtual DbSet<TblTransaction> TblTransactions { get; set; }

    public virtual DbSet<TblTransactionRoomDonation> TblTransactionRoomDonations { get; set; }

    public virtual DbSet<TblTrasactionCategoryMaster> TblTrasactionCategoryMasters { get; set; }

    public virtual DbSet<TblTrasactionTypeMaster> TblTrasactionTypeMasters { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblUserRole> TblUserRoles { get; set; }

    public virtual DbSet<TblVacateRoom> TblVacateRooms { get; set; }

    // Connection string is supplied via ASP.NET Core DI (Program.cs: AddDbContext<AbpsContext>(...)
    // reading appsettings.json), not System.Configuration/App.config as the WPF app did — no
    // OnConfiguring override needed here since options always arrive through the constructor.
    // NOTE: EnableRetryOnFailure cannot be added yet — controllers use Database.BeginTransaction(),
    // which the retrying execution strategy rejects. Requires wrapping transactions in
    // CreateExecutionStrategy().Execute(...) first (see docs/APPLICATION_REVIEW.md item 36,
    // carried over from ABPS_WPF).

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<DumpAnnad>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dump_ANNAD");

            entity.Property(e => e.Amt).HasColumnName("AMT");
            entity.Property(e => e.DateType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Days).HasColumnName("DAYS");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.NewBondNo).IsUnicode(false);
            entity.Property(e => e.OccassionId).HasColumnName("OccassionID");
            entity.Property(e => e.ParseStatus)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Rawdata)
                .IsUnicode(false)
                .HasColumnName("rawdata");
            entity.Property(e => e.SsmaTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SSMA_TimeStamp");
            entity.Property(e => e.TeluguMasaId).HasColumnName("TeluguMasaID");
            entity.Property(e => e.TeluguMasam)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TeluguPaksham)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.TeluguThidhi)
                .HasMaxLength(30)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DumpDona>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dump_DONA");

            entity.Property(e => e.Addressdon).HasColumnName("ADDRESSDON");
            entity.Property(e => e.Alotdays)
                .HasDefaultValue((short)0)
                .HasColumnName("ALOTDAYS");
            entity.Property(e => e.Amtdon)
                .HasDefaultValue(0)
                .HasComment("RECEIVED")
                .HasColumnName("AMTDON");
            entity.Property(e => e.Amtpaid)
                .HasDefaultValue(0)
                .HasComment("PAYMENT")
                .HasColumnName("AMTPAID");
            entity.Property(e => e.Bdt)
                .HasMaxLength(255)
                .HasDefaultValue("-")
                .HasColumnName("bdt");
            entity.Property(e => e.Cash)
                .HasMaxLength(20)
                .HasDefaultValue("-")
                .HasColumnName("cash");
            entity.Property(e => e.Chdrno)
                .HasMaxLength(100)
                .HasDefaultValue("-")
                .HasColumnName("CHDRNO");
            entity.Property(e => e.Code)
                .HasMaxLength(2)
                .HasColumnName("CODE");
            entity.Property(e => e.Dateofann)
                .HasMaxLength(100)
                .HasColumnName("DATEOFANN");
            entity.Property(e => e.Dateofdon)
                .HasPrecision(0)
                .HasDefaultValue(new DateTime(2014, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified))
                .HasColumnName("DATEOFDON");
            entity.Property(e => e.Datepaid)
                .HasPrecision(0)
                .HasColumnName("DATEPAID");
            entity.Property(e => e.Details)
                .HasMaxLength(150)
                .HasColumnName("DETAILS");
            entity.Property(e => e.DonarTypeId).HasColumnName("DonarTypeID");
            entity.Property(e => e.Donty)
                .HasMaxLength(50)
                .HasColumnName("DONTY");
            entity.Property(e => e.Gothram)
                .HasMaxLength(100)
                .HasColumnName("GOTHRAM");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Lastvis)
                .HasMaxLength(20)
                .HasColumnName("LASTVIS");
            entity.Property(e => e.Mode)
                .HasMaxLength(2)
                .HasColumnName("MODE");
            entity.Property(e => e.Namedon).HasColumnName("NAMEDON");
            entity.Property(e => e.NewBondNo).IsUnicode(false);
            entity.Property(e => e.Occ)
                .HasMaxLength(255)
                .HasDefaultValue("-")
                .HasColumnName("occ");
            entity.Property(e => e.Pan)
                .HasMaxLength(50)
                .HasDefaultValue("-")
                .HasColumnName("PAN");
            entity.Property(e => e.Part)
                .HasMaxLength(2)
                .HasDefaultValue("F")
                .HasColumnName("PART");
            entity.Property(e => e.Pfno)
                .HasDefaultValue(0)
                .HasColumnName("PFNO");
            entity.Property(e => e.Place).HasColumnName("PLACE");
            entity.Property(e => e.Rupees)
                .HasMaxLength(255)
                .HasDefaultValue("-")
                .HasColumnName("rupees");
            entity.Property(e => e.SsmaTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SSMA_TimeStamp");
            entity.Property(e => e.Tdays)
                .HasDefaultValue((short)0)
                .HasColumnName("TDAYS");
            entity.Property(e => e.Userd)
                .HasMaxLength(10)
                .HasColumnName("USERD");
            entity.Property(e => e.Vcno)
                .HasMaxLength(20)
                .HasComment("VOUCHER NO")
                .HasColumnName("VCNO");
            entity.Property(e => e.Wsname)
                .HasMaxLength(150)
                .HasColumnName("WSNAME");
        });

        modelBuilder.Entity<DumpDonash>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dump_DONASHS");

            entity.Property(e => e.Addressdon)
                .HasMaxLength(255)
                .HasColumnName("ADDRESSDON");
            entity.Property(e => e.Amtdon).HasColumnName("AMTDON");
            entity.Property(e => e.Dateofann)
                .HasMaxLength(255)
                .HasColumnName("DATEOFANN");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Id1)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID1");
            entity.Property(e => e.Namedon)
                .HasMaxLength(255)
                .HasColumnName("NAMEDON");
            entity.Property(e => e.Phoneno)
                .HasMaxLength(255)
                .HasDefaultValueSql("((0))")
                .HasColumnName("PHONENO");
            entity.Property(e => e.Sda)
                .HasMaxLength(255)
                .HasColumnName("SDA");
            entity.Property(e => e.Sno)
                .HasMaxLength(255)
                .HasDefaultValueSql("((0))")
                .HasColumnName("SNO");
            entity.Property(e => e.SsmaTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SSMA_TimeStamp");
            entity.Property(e => e.T1).HasMaxLength(255);
            entity.Property(e => e.T2).HasMaxLength(255);
        });

        modelBuilder.Entity<DumpDonationTypeMapping>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dumpDonationTypeMapping");

            entity.Property(e => e.DonationText)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DumpDonlist>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dump_DONLIST");

            entity.Property(e => e.Amt).HasColumnName("AMT");
            entity.Property(e => e.Days).HasColumnName("DAYS");
            entity.Property(e => e.Doname)
                .HasMaxLength(255)
                .HasColumnName("DONAME");
            entity.Property(e => e.Dtype)
                .HasMaxLength(20)
                .HasColumnName("DTYPE");
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Remarks)
                .HasMaxLength(255)
                .HasColumnName("REMARKS");
            entity.Property(e => e.SsmaTimeStamp)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SSMA_TimeStamp");
        });

        modelBuilder.Entity<DumpMember>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dump_members");

            entity.Property(e => e.Addressdon)
                .HasMaxLength(255)
                .HasColumnName("ADDRESSDON");
            entity.Property(e => e.Dateofann)
                .HasMaxLength(255)
                .HasColumnName("DATEOFANN");
            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("ID");
            entity.Property(e => e.Id1).HasColumnName("ID1");
            entity.Property(e => e.Namedon)
                .HasMaxLength(255)
                .HasColumnName("NAMEDON");
            entity.Property(e => e.Phoneno)
                .HasMaxLength(255)
                .HasColumnName("PHONENO");
        });

        modelBuilder.Entity<DumpOccasionMapping>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dumpOccasionMapping");

            entity.Property(e => e.OccasionText)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DumpTblDonar>(entity =>
        {
            entity.HasKey(e => e.DonarId);

            entity.ToTable("dump_tbl_Donars");

            entity.Property(e => e.DonarId).HasColumnName("DonarID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
            entity.Property(e => e.DonarTypeId).HasColumnName("DonarTypeID");
            entity.Property(e => e.EmailId).HasColumnName("EmailID");
            entity.Property(e => e.NewBondNo).IsUnicode(false);
            entity.Property(e => e.Pan).HasColumnName("PAN");
            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.TeluguName).HasColumnName("Telugu_Name");
        });

        modelBuilder.Entity<DumpTblDonation>(entity =>
        {
            entity.HasKey(e => e.DonationId);

            entity.ToTable("dump_tbl_Donations");

            entity.Property(e => e.DonationId).HasColumnName("DonationID");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.DonarId).HasColumnName("DonarID");
            entity.Property(e => e.DonationAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DonationDate).HasColumnType("datetime");
            entity.Property(e => e.DonationTypeId).HasColumnName("DonationTypeID");
            entity.Property(e => e.PaymentSubTypeId).HasColumnName("PaymentSubTypeID");
            entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");
        });

        modelBuilder.Entity<DumpTblDonationOccation>(entity =>
        {
            entity.HasKey(e => e.DonationId);

            entity.ToTable("dump_tbl_Donation_Occation");

            entity.Property(e => e.DonationId)
                .ValueGeneratedNever()
                .HasColumnName("DonationID");
            entity.Property(e => e.AnandaanamDate).HasColumnName("Anandaanam_Date");
            entity.Property(e => e.AnandaanamDateType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("D")
                .HasColumnName("Anandaanam_DateType");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.DateEnglishTelugu)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("E")
                .HasColumnName("Date_English_Telugu");
            entity.Property(e => e.DonarId).HasColumnName("DonarID");
            entity.Property(e => e.DonationOccationId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Donation_Occation_ID");
            entity.Property(e => e.OcassionId).HasColumnName("OcassionID");
            entity.Property(e => e.TMasamId).HasColumnName("T_MasamID");
            entity.Property(e => e.TPaskshamId).HasColumnName("T_PaskshamID");
            entity.Property(e => e.TThiduluId).HasColumnName("T_ThiduluID");
            entity.Property(e => e.TeluguIntheNameof).HasColumnName("Telugu_IntheNameof");
        });

        modelBuilder.Entity<DumpTeluguMasamMap>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dumpTeluguMasamMap");

            entity.Property(e => e.MasamCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Variant)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<DumpThidhiMap>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("dumpThidhiMap");

            entity.Property(e => e.ThidhiCode)
                .HasMaxLength(30)
                .IsUnicode(false);
            entity.Property(e => e.Variant)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<PakshamMap>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("PakshamMap");

            entity.Property(e => e.PakshamCode)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.Variant)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblAddonRoom>(entity =>
        {
            entity.HasKey(e => e.AddonRoomId);

            entity.ToTable("tbl_AddonRoom");

            entity.HasIndex(e => e.BookingId, "IX_tbl_AddonRoom");

            entity.Property(e => e.AddonRoomId).HasColumnName("AddonRoomID");
            entity.Property(e => e.AddAdvance)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Add_Advance");
            entity.Property(e => e.AddonBookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("AddonBooking_Date");
            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.CreateTimestmap)
                .HasDefaultValueSql("((0))")
                .HasColumnType("datetime")
                .HasColumnName("Create_Timestmap");
            entity.Property(e => e.Createdby).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.TotalAdvance)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Total_Advance");
            entity.Property(e => e.TotalAmount)
                .HasColumnType("decimal(18, 0)")
                .HasColumnName("Total_Amount");
        });

        modelBuilder.Entity<TblAdvanceBooking>(entity =>
        {
            entity.HasKey(e => e.AdvanceBookingId);

            entity.ToTable("tbl_AdvanceBooking");

            entity.HasIndex(e => new { e.RequestedCheckInDate, e.StatusId }, "IX_AdvanceBooking_DueDate");
            entity.HasIndex(e => e.PhoneNo, "IX_AdvanceBooking_Phone");

            entity.Property(e => e.AdvanceBookingId).HasColumnName("Advance_Booking_ID");
            entity.Property(e => e.GuestName).HasMaxLength(200).HasColumnName("Guest_Name");
            entity.Property(e => e.PhoneNo).HasMaxLength(20).HasColumnName("Phone_No");
            entity.Property(e => e.AadharNo).HasMaxLength(20).HasColumnName("Aadhar_No");
            entity.Property(e => e.Address).HasMaxLength(500);
            entity.Property(e => e.NoPeople).HasColumnName("No_People");
            entity.Property(e => e.NoRoomsRequested).HasColumnName("No_Rooms_Requested");
            entity.Property(e => e.RoomCategoryRtId).HasColumnName("Room_Category_RT_ID");
            entity.Property(e => e.CatId).HasColumnName("Cat_ID");
            entity.Property(e => e.BondNo).HasMaxLength(50).HasColumnName("Bond_No");
            entity.Property(e => e.RequestedCheckInDate).HasColumnType("datetime").HasColumnName("Requested_CheckIn_Date");
            entity.Property(e => e.RequestedNights).HasDefaultValue(1).HasColumnName("Requested_Nights");
            entity.Property(e => e.AdvanceDepositAmount).HasColumnType("decimal(18, 2)").HasColumnName("Advance_Deposit_Amount");
            entity.Property(e => e.PaymentTypeId).HasColumnName("Payment_Type_ID");
            entity.Property(e => e.PaymentSubTypeId).HasColumnName("Payment_SubType_ID");
            entity.Property(e => e.ReferenceNumber).HasMaxLength(100).HasColumnName("Reference_Number");
            entity.Property(e => e.Notes).HasMaxLength(1000);
            entity.Property(e => e.StatusId).HasDefaultValue(1).HasColumnName("Status_ID");
            entity.Property(e => e.AssignedBookingId).HasColumnName("Assigned_Booking_ID");
            entity.Property(e => e.AssignedBy).HasMaxLength(100).HasColumnName("Assigned_By");
            entity.Property(e => e.AssignedTimestamp).HasColumnType("datetime").HasColumnName("Assigned_Timestamp");
            entity.Property(e => e.CancelledReason).HasMaxLength(500).HasColumnName("Cancelled_Reason");
            entity.Property(e => e.CreatedBy).HasMaxLength(100).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.ModifiedBy).HasMaxLength(100).HasColumnName("Modified_By");
            entity.Property(e => e.ModifiedTimestamp).HasColumnType("datetime").HasColumnName("Modified_Timestamp");
        });

        modelBuilder.Entity<TblBoardMember>(entity =>
        {
            entity.HasKey(e => e.MemberId);

            entity.ToTable("tbl_BoardMembers");

            entity.Property(e => e.MemberId).HasColumnName("MemberID");
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.Email).IsUnicode(false);
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.MemberName).IsUnicode(false);
            entity.Property(e => e.MemberTname).HasColumnName("MemberTName");
            entity.Property(e => e.MemberType).IsUnicode(false);
            entity.Property(e => e.Phone).IsUnicode(false);
        });

        modelBuilder.Entity<TblBookingCategory>(entity =>
        {
            entity.HasKey(e => e.CatId).HasName("PK_tbl_Category");

            entity.ToTable("tbl_BookingCategory");

            entity.Property(e => e.CatId).HasColumnName("CatID");
            entity.Property(e => e.Category).IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
        });

        modelBuilder.Entity<TblBookingDetail>(entity =>
        {
            entity.HasKey(e => e.BookingId);

            entity.ToTable("tbl_BookingDetails");

            // Booking Report / Occupancy Report / dashboard queries all filter on this
            // date+status pair with no supporting index, which forced a full table scan as the
            // table grew and eventually timed out (see ReportController.GetBookingReport).
            entity.HasIndex(e => new { e.BookingDate, e.BookingStatusId }, "IX_tbl_BookingDetails_BookingDate");

            // BondNo is not indexed: it's an unbounded nvarchar(MAX) column, which SQL Server
            // rejects as an index key ("Column 'BondNo' is of a type that is invalid for use as
            // a key column in an index" — confirmed against the live schema). Indexing it would
            // require first narrowing the column to a bounded length (e.g. nvarchar(50)), which
            // is a data-affecting schema change, not just an additive index.
            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.AadharNo).HasColumnName("Aadhar_No");
            entity.Property(e => e.AddonRoom)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("N");
            entity.Property(e => e.Address).IsUnicode(false);
            entity.Property(e => e.Advance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookingDate)
                .HasColumnType("datetime")
                .HasColumnName("Booking_Date");
            entity.Property(e => e.BookingStatusId)
                .HasDefaultValue(1)
                .HasColumnName("Booking_Status_ID");
            entity.Property(e => e.CatId).HasColumnName("CatID");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.DonarId).HasColumnName("DonarID");
            entity.Property(e => e.Dtid).HasColumnName("DTID");
            entity.Property(e => e.NoPeople).HasColumnName("No_People");
            entity.Property(e => e.PhoneNo).HasColumnName("Phone_No");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblBookingDocument>(entity =>
        {
            entity.HasKey(e => e.BookingDocumentId);

            entity.ToTable("tbl_BookingDocuments");

            entity.HasIndex(e => e.BookingId, "IX_tbl_BookingDocuments_BookingId");

            entity.Property(e => e.BookingDocumentId).HasColumnName("BookingDocumentId");
            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.DocumentType)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.FileName)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.ContentType)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
        });

        modelBuilder.Entity<TblBookingLedger>(entity =>
        {
            entity.HasKey(e => e.BookingLedgerId);

            entity.ToTable("tbl_BookingLedger");

            // No index existed on this table at all besides the PK — Booking_Id is the join
            // column used by Occupancy Report, Expiring Bookings Report, the donor bond
            // "days used" calc, and the Dashboard's 60-second auto-refresh.
            entity.HasIndex(e => e.BookingId, "IX_tbl_BookingLedger_BookingId");
            entity.HasIndex(e => e.CheckinDateTime, "IX_tbl_BookingLedger_CheckinDateTime");
            entity.HasIndex(e => e.CheckoutDateTime, "IX_tbl_BookingLedger_CheckoutDateTime");

            entity.Property(e => e.BookingLedgerId).HasColumnName("BookingLedger_ID");
            entity.Property(e => e.ActualRefundAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Actual_RefundAmount");
            entity.Property(e => e.ActualTotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Actual_TotalAmount");
            entity.Property(e => e.Advance).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BookingId).HasColumnName("Booking_Id");
            entity.Property(e => e.CheckinByuser)
                .IsUnicode(false)
                .HasColumnName("Checkin_byuser");
            entity.Property(e => e.CheckinDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Checkin_DateTime");
            entity.Property(e => e.CheckinInsertDatetime)
                .HasColumnType("datetime")
                .HasColumnName("Checkin_insert_datetime");
            entity.Property(e => e.CheckoutByuser)
                .IsUnicode(false)
                .HasColumnName("Checkout_byuser");
            entity.Property(e => e.CheckoutDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Checkout_DateTime");
            entity.Property(e => e.CheckoutInsertDatetime)
                .HasColumnType("datetime")
                .HasColumnName("Checkout_insert_datetime");
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.IntendedRefundAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Intended_RefundAmount");
            entity.Property(e => e.IntendedTotalAmount)
                .HasColumnType("decimal(18, 2)")
                .HasColumnName("Intended_TotalAmount");
            entity.Property(e => e.ModifiedBy)
                .IsUnicode(false)
                .HasColumnName("Modified_By");
            entity.Property(e => e.ModifiedTimestamp)
                .HasColumnType("datetime")
                .HasColumnName("Modified_Timestamp");
        });

        modelBuilder.Entity<TblBookingRoom>(entity =>
        {
            entity.HasKey(e => e.BookingRoomId);

            entity.ToTable("tbl_Booking_Room");

            entity.HasIndex(e => e.CheckInDate, "IX_tbl_Booking_Room");

            entity.HasIndex(e => e.RoomId, "IX_tbl_Booking_Room_1");

            entity.HasIndex(e => e.CheckOutDate, "IX_tbl_Booking_Room_2");

            // Booking_id is the join column back to tbl_BookingDetails in every report that
            // needs room numbers per booking (Booking Report, Occupancy Report, Audit Report) —
            // there was no index on it at all, forcing a scan of this table for every booking row.
            entity.HasIndex(e => e.BookingId, "IX_tbl_Booking_Room_BookingId");

            entity.Property(e => e.BookingRoomId).HasColumnName("Booking_Room_ID");
            entity.Property(e => e.BookingDate)
                .HasColumnType("datetime")
                .HasColumnName("Booking_Date");
            entity.Property(e => e.BookingId).HasColumnName("Booking_id");
            entity.Property(e => e.BookingStatusId)
                .HasDefaultValue(0)
                .HasColumnName("BookingStatusID");
            entity.Property(e => e.CancellationType)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasDefaultValue("None");
            entity.Property(e => e.CheckInDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CheckIn_Date");
            entity.Property(e => e.CheckOutDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("CheckOut_Date");
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.GroupBooking)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.ModifiedBy)
                .IsUnicode(false)
                .HasColumnName("Modified_By");
            entity.Property(e => e.ModifiedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Modified_Timestamp");
            entity.Property(e => e.RoomAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RoomId).HasColumnName("Room_ID");
        });

        modelBuilder.Entity<TblBookingStatus>(entity =>
        {
            entity.HasKey(e => e.BookingStatusId);

            entity.ToTable("tbl_BookingStatus");

            entity.Property(e => e.BookingStatusId).HasColumnName("BookingStatusID");
            entity.Property(e => e.BookingStatusDesc)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.Notes)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.StatusCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Status_Code");
        });

        modelBuilder.Entity<TblCancelledRoom>(entity =>
        {
            entity.HasKey(e => e.CancelledRoomId);

            entity.ToTable("tbl_Cancelled_Room");

            entity.Property(e => e.CancelledRoomId).HasColumnName("Cancelled_Room_ID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_id");
            entity.Property(e => e.CancelationDate)
                .HasColumnType("datetime")
                .HasColumnName("Cancelation_Date");
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
        });

        modelBuilder.Entity<TblCountry>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_Country");

            entity.Property(e => e.CountryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("CountryID");
            entity.Property(e => e.CountryName).IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
        });

        modelBuilder.Entity<TblDistrict>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_Districts");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.DistrictId)
                .ValueGeneratedOnAdd()
                .HasColumnName("DistrictID");
            entity.Property(e => e.DistrictName).IsUnicode(false);
            entity.Property(e => e.StateId).HasColumnName("StateID");
        });

        modelBuilder.Entity<TblDonar>(entity =>
        {
            entity.HasKey(e => e.DonarId);

            entity.ToTable("tbl_Donars");

            entity.Property(e => e.DonarId).HasColumnName("DonarID");
            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.DistrictId).HasColumnName("DistrictID");
            entity.Property(e => e.DonarTypeId).HasColumnName("DonarTypeID");
            entity.Property(e => e.EmailId).HasColumnName("EmailID");
            entity.Property(e => e.Pan).HasColumnName("PAN");
            entity.Property(e => e.StateId).HasColumnName("StateID");
            entity.Property(e => e.TeluguName).HasColumnName("Telugu_Name");
        });

        modelBuilder.Entity<TblDonarTypeMaster>(entity =>
        {
            entity.HasKey(e => e.Dtid).HasName("PK_tbl_DonarType");

            entity.ToTable("tbl_DonarType_Master");

            entity.Property(e => e.Dtid).HasColumnName("DTID");
            entity.Property(e => e.Amount).HasColumnName("AMOUNT");
            entity.Property(e => e.Code)
                .HasMaxLength(2)
                .HasColumnName("CODE");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.Days).HasColumnName("DAYS");
            entity.Property(e => e.Donartype).HasColumnName("DONARTYPE");
        });

        modelBuilder.Entity<TblDonation>(entity =>
        {
            entity.HasKey(e => e.DonationId);

            entity.ToTable("tbl_Donations");

            // No index existed here besides the PK. DonationDate is the Donation Report's date
            // filter; DonarId is the join column GetDonarByBondNo/GetDonationReport use.
            // BondNo is NOT indexed here (same as tbl_BookingDetails.BondNo above) — it's an
            // unbounded nvarchar(MAX) column, which SQL Server rejects as an index key.
            entity.HasIndex(e => e.DonationDate, "IX_tbl_Donations_DonationDate");
            entity.HasIndex(e => e.DonarId, "IX_tbl_Donations_DonarId");

            entity.Property(e => e.DonationId).HasColumnName("DonationID");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.DonarId).HasColumnName("DonarID");
            entity.Property(e => e.DonationAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.DonationDate).HasColumnType("datetime");
            entity.Property(e => e.DonationTypeId).HasColumnName("DonationTypeID");
            entity.Property(e => e.PaymentSubTypeId).HasColumnName("PaymentSubTypeID");
            entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");
        });

        modelBuilder.Entity<TblDonationOccation>(entity =>
        {
            entity.HasKey(e => e.DonationId);

            entity.ToTable("tbl_Donation_Occation");

            entity.Property(e => e.DonationId)
                .ValueGeneratedNever()
                .HasColumnName("DonationID");
            entity.Property(e => e.AnandaanamDate).HasColumnName("Anandaanam_Date");
            entity.Property(e => e.AnandaanamDateType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("D")
                .HasColumnName("Anandaanam_DateType");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.DateEnglishTelugu)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("E")
                .HasColumnName("Date_English_Telugu");
            entity.Property(e => e.DonarId).HasColumnName("DonarID");
            entity.Property(e => e.DonationOccationId)
                .ValueGeneratedOnAdd()
                .HasColumnName("Donation_Occation_ID");
            entity.Property(e => e.OcassionId).HasColumnName("OcassionID");
            entity.Property(e => e.TMasamId).HasColumnName("T_MasamID");
            entity.Property(e => e.TPaskshamId).HasColumnName("T_PaskshamID");
            entity.Property(e => e.TThiduluId).HasColumnName("T_ThiduluID");
            entity.Property(e => e.TeluguIntheNameof).HasColumnName("Telugu_IntheNameof");
        });

        modelBuilder.Entity<TblErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorLogId).HasName("PK__tbl_Erro__D65247E2AA82BC5A");

            entity.ToTable("tbl_ErrorLogs");

            entity.Property(e => e.ErrorLogId).HasColumnName("ErrorLogID");
            entity.Property(e => e.LogDate).HasColumnType("datetime");
            entity.Property(e => e.LogLevel).HasMaxLength(50);
            entity.Property(e => e.Logger).HasMaxLength(500);
        });

        modelBuilder.Entity<TblExtendroom>(entity =>
        {
            entity.HasKey(e => e.ExtendRoomId);

            entity.ToTable("tbl_Extendroom");

            entity.Property(e => e.ExtendRoomId).HasColumnName("ExtendRoom_ID");
            entity.Property(e => e.Bookingid).HasColumnName("bookingid");
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_date");
            entity.Property(e => e.Extendeddays).HasColumnName("extendeddays");
            entity.Property(e => e.Newcheckout)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("newcheckout");
            entity.Property(e => e.Precheckout)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("precheckout");
            entity.Property(e => e.Prevnoofdays).HasColumnName("prevnoofdays");
            entity.Property(e => e.Totaldays).HasColumnName("totaldays");
        });

        modelBuilder.Entity<TblFloorMaster>(entity =>
        {
            entity.HasKey(e => e.FloorId);

            entity.ToTable("tbl_Floor_Master");

            entity.Property(e => e.FloorId).HasColumnName("Floor_ID");
            entity.Property(e => e.Active).HasMaxLength(1);
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.FloorDescription).HasColumnName("Floor_Description");
        });

        modelBuilder.Entity<TblOccassionMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_Occassion_Master");

            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.OccassionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("OccassionID");
            entity.Property(e => e.OccassionName).IsUnicode(false);
        });

        modelBuilder.Entity<TblOperator>(entity =>
        {
            entity.HasKey(e => e.OperatorId);

            entity.ToTable("tbl_Operators");

            entity.Property(e => e.OperatorId).HasColumnName("OperatorID");
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.LastLoginDateTime).HasColumnType("datetime");
            entity.Property(e => e.OperatorName).IsUnicode(false);
            entity.Property(e => e.Password).IsUnicode(false);
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblPaymentSubTypeMaster>(entity =>
        {
            entity.HasKey(e => e.PaymentSubTypeId);

            entity.ToTable("tbl_PaymentSubTypeMaster");

            entity.Property(e => e.PaymentSubTypeId).HasColumnName("paymentSubTypeID");
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.PaymentSubType).IsUnicode(false);
            entity.Property(e => e.PaymentTypeId).HasColumnName("paymentTypeID");
        });

        modelBuilder.Entity<TblPaymentTypeMaster>(entity =>
        {
            entity.HasKey(e => e.PaymentTypeId);

            entity.ToTable("tbl_PaymentTypeMaster");

            entity.Property(e => e.PaymentTypeId).HasColumnName("paymentTypeID");
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.PaymentType).IsUnicode(false);
        });

        modelBuilder.Entity<TblRoom>(entity =>
        {
            entity.HasKey(e => e.RoomId);

            entity.ToTable("tbl_Room");

            entity.Property(e => e.RoomId).HasColumnName("Room_ID");
            entity.Property(e => e.AdvanceAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.Allowed).HasDefaultValue(1);
            entity.Property(e => e.Beds).HasDefaultValue(1);
            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.Cost).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.FloorId).HasColumnName("Floor_ID");
            entity.Property(e => e.RoomNo).HasColumnName("Room_No");
            entity.Property(e => e.RoomStatusId)
                .HasDefaultValue(1)
                .HasColumnName("Room_Status_ID");
            entity.Property(e => e.RtId).HasColumnName("RT_ID");
        });

        modelBuilder.Entity<TblRoomMasterOld>(entity =>
        {
            entity.HasKey(e => e.RoomId).HasName("PK_tbl_Room_Master_1");

            entity.ToTable("tbl_Room_Master_old");

            entity.Property(e => e.RoomId).HasColumnName("Room_ID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_ID");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.FloorId).HasColumnName("Floor_ID");
            entity.Property(e => e.RoomNo).HasColumnName("Room_No");
            entity.Property(e => e.RoomStatusId)
                .HasDefaultValue(1)
                .HasColumnName("Room_Status_ID");
            entity.Property(e => e.RtId).HasColumnName("RT_ID");
        });

        modelBuilder.Entity<TblRoomStatus>(entity =>
        {
            entity.HasKey(e => e.RoomStatusId);

            entity.ToTable("tbl_Room_Status");

            entity.Property(e => e.RoomStatusId).HasColumnName("Room_Status_ID");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(50)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.Notes).HasMaxLength(100);
            entity.Property(e => e.RoomStatusDesc)
                .HasMaxLength(50)
                .HasColumnName("Room_Status_Desc");
            entity.Property(e => e.StatusCode)
                .HasMaxLength(10)
                .IsUnicode(false)
                .HasColumnName("Status_Code");
        });

        modelBuilder.Entity<TblRoomType>(entity =>
        {
            entity.HasKey(e => e.RtId);

            entity.ToTable("tbl_Room_Type");

            entity.Property(e => e.RtId).HasColumnName("RT_ID");
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("Y")
                .IsFixedLength()
                .HasColumnName("is_active");
            entity.Property(e => e.RoomCategory).HasColumnName("Room_Category");
            entity.Property(e => e.RtCreatedBy).HasColumnName("RT_Created_By");
            entity.Property(e => e.RtCreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("RT_Created_Timestamp");
            entity.Property(e => e.RtDesc).HasColumnName("RT_Desc");
        });

        modelBuilder.Entity<TblRoomTypeMasterOld>(entity =>
        {
            entity.HasKey(e => e.RtId).HasName("PK_tbl_Room_Master");

            entity.ToTable("tbl_Room_Type_Master_old");

            entity.Property(e => e.RtId).HasColumnName("RT_ID");
            entity.Property(e => e.RoomCategory)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasDefaultValue("Normal")
                .HasColumnName("Room_Category");
            entity.Property(e => e.RtAc)
                .HasMaxLength(1)
                .HasColumnName("RT_AC");
            entity.Property(e => e.RtBed).HasColumnName("RT_Bed");
            entity.Property(e => e.RtCreatedBy).HasColumnName("RT_Created_By");
            entity.Property(e => e.RtCreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("RT_Created_Timestamp");
            entity.Property(e => e.RtDesc).HasColumnName("RT_Desc");
            entity.Property(e => e.RtPrice).HasColumnName("RT_Price");
        });

        modelBuilder.Entity<TblShiftRoom>(entity =>
        {
            entity.HasKey(e => e.ShiftRoomId);

            entity.ToTable("tbl_Shift_Room");

            entity.Property(e => e.ShiftRoomId).HasColumnName("ShiftRoomID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_id");
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.NewRoomId).HasColumnName("New_RoomID");
            entity.Property(e => e.OldRoomId).HasColumnName("Old_RoomID");
        });

        modelBuilder.Entity<TblSmstemplate>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_SMSTemplate");

            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.OccassionId).HasColumnName("OccassionID");
            entity.Property(e => e.Smscontent)
                .IsUnicode(false)
                .HasColumnName("SMSContent");
            entity.Property(e => e.SmstemplateId)
                .ValueGeneratedOnAdd()
                .HasColumnName("SMSTemplateID");
        });

        modelBuilder.Entity<TblSmstransaction>(entity =>
        {
            entity.HasKey(e => e.Smsid);

            entity.ToTable("tbl_SMSTransaction");

            entity.Property(e => e.Smsid).HasColumnName("SMSID");
            entity.Property(e => e.BondNo).IsUnicode(false);
            entity.Property(e => e.Content).IsUnicode(false);
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.DateType)
                .HasMaxLength(1)
                .IsUnicode(false)
                .HasDefaultValue("D");
            entity.Property(e => e.DonarId).HasColumnName("DonarID");
            entity.Property(e => e.OccasionId).HasColumnName("OccasionID");
            entity.Property(e => e.PhoneNo)
                .IsUnicode(false)
                .HasColumnName("Phone_no");
            entity.Property(e => e.Smsdate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnName("SMSDate");
            entity.Property(e => e.TMasamId).HasColumnName("T_MasamID");
            entity.Property(e => e.TPaskshamId).HasColumnName("T_PaskshamID");
            entity.Property(e => e.TThiduluId).HasColumnName("T_ThiduluID");
        });

        modelBuilder.Entity<TblState>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_States");

            entity.Property(e => e.CountryId).HasColumnName("CountryID");
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.StateId)
                .ValueGeneratedOnAdd()
                .HasColumnName("StateID");
            entity.Property(e => e.StateName).IsUnicode(false);
        });

        modelBuilder.Entity<TblTeluguMasam>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_TeluguMasam");

            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.TMasamId)
                .ValueGeneratedOnAdd()
                .HasColumnName("T_MasamID");
            entity.Property(e => e.TMasamName)
                .IsUnicode(false)
                .HasColumnName("T_MasamName");
            entity.Property(e => e.TMasamNameEnglish)
                .IsUnicode(false)
                .HasColumnName("T_MasamName_English");
        });

        modelBuilder.Entity<TblTeluguPaksham>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_TeluguPaksham");

            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.TPakshamId)
                .ValueGeneratedOnAdd()
                .HasColumnName("T_PakshamID");
            entity.Property(e => e.TPakshamName)
                .IsUnicode(false)
                .HasColumnName("T_PakshamName");
            entity.Property(e => e.TPakshamNameEnglish)
                .IsUnicode(false)
                .HasColumnName("T_PakshamName_English");
        });

        modelBuilder.Entity<TblTeluguThidi>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_TeluguThidi");

            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.TThidiId)
                .ValueGeneratedOnAdd()
                .HasColumnName("T_ThidiID");
            entity.Property(e => e.TThidiName)
                .IsUnicode(false)
                .HasColumnName("T_ThidiName");
            entity.Property(e => e.TThidiNameEnglish)
                .IsUnicode(false)
                .HasColumnName("T_ThidiName_English");
        });

        modelBuilder.Entity<TblTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.ToTable("tbl_Transaction");

            // No index existed here at all — TransactionDate is filtered by 5 different report
            // methods (Transaction Report, Day/Month Summary x2 each, User Summary), and this
            // table grows with every advance/payment/refund/donation transaction ever recorded.
            entity.HasIndex(e => e.TransactionDate, "IX_tbl_Transaction_TransactionDate");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.BondNo).IsUnicode(false);
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.DonarBookingId).HasColumnName("Donar_Booking_id");
            entity.Property(e => e.PaymentSubTypeId).HasColumnName("PaymentSubTypeID");
            entity.Property(e => e.PaymentTypeId).HasColumnName("PaymentTypeID");
            entity.Property(e => e.TransactionCategoryId).HasColumnName("TransactionCategoryID");
            entity.Property(e => e.TransactionDate).HasColumnType("datetime");
            entity.Property(e => e.TrasactionTypeId).HasColumnName("TrasactionTypeID");
        });

        modelBuilder.Entity<TblTransactionRoomDonation>(entity =>
        {
            entity.HasKey(e => e.TransactionId);

            entity.ToTable("tbl_Transaction_RoomDonation");

            entity.HasIndex(e => e.BookingId, "IX_tbl_Transaction_RoomDonation");

            entity.HasIndex(e => e.CheckoutDate, "IX_tbl_Transaction_RoomDonation_1");

            // GetAuditTransactionReport filters on CreatedTimestamp, not CheckoutDate — neither
            // existing index above covers it.
            entity.HasIndex(e => e.CreatedTimestamp, "IX_tbl_Transaction_RoomDonation_CreatedTimestamp");

            entity.Property(e => e.TransactionId).HasColumnName("TransactionID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_id");
            entity.Property(e => e.CheckoutDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedBy).HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.DonationAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.RoomAmount).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(18, 2)");
        });

        modelBuilder.Entity<TblTrasactionCategoryMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_TrasactionCategory_Master");

            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.TransactionCategory).IsUnicode(false);
            entity.Property(e => e.TrasactionCategoryId)
                .ValueGeneratedOnAdd()
                .HasColumnName("TrasactionCategoryID");
        });

        modelBuilder.Entity<TblTrasactionTypeMaster>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("tbl_TrasactionType_Master");

            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.TransactionType).IsUnicode(false);
            entity.Property(e => e.TrasactionTypeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("TrasactionTypeID");
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserName).HasName("PK__tbl_User__1788CCACCE2CA86A");

            entity.ToTable("tbl_User");

            entity.HasIndex(e => e.Email, "UQ__tbl_User__A9D1053447393A4F").IsUnique();

            entity.Property(e => e.UserName).HasMaxLength(100);
            entity.Property(e => e.CreatedBy).HasMaxLength(50);
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Email).HasMaxLength(150);
            entity.Property(e => e.IsActive)
                .HasMaxLength(1)
                .HasDefaultValue("Y");
            entity.Property(e => e.MobileNumber).HasMaxLength(20);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.UpdatedBy).HasMaxLength(50);
            entity.Property(e => e.UpdatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserID");
        });

        modelBuilder.Entity<TblUserRole>(entity =>
        {
            entity.HasKey(e => e.RoleId);

            entity.ToTable("tbl_User_Role");

            entity.Property(e => e.RoleId).HasColumnName("RoleID");
            entity.Property(e => e.CreatedBy).HasColumnName("created_by");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("created_timestamp");
        });

        modelBuilder.Entity<TblVacateRoom>(entity =>
        {
            entity.HasKey(e => e.VacateRoomId);

            entity.ToTable("tbl_Vacate_Room");

            entity.Property(e => e.VacateRoomId).HasColumnName("Vacate_Room_ID");
            entity.Property(e => e.BookingId).HasColumnName("Booking_id");
            entity.Property(e => e.CheckoutDateTime)
                .HasColumnType("datetime")
                .HasColumnName("Checkout_DateTime");
            entity.Property(e => e.CreatedBy)
                .IsUnicode(false)
                .HasColumnName("Created_By");
            entity.Property(e => e.CreatedTimestamp)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime")
                .HasColumnName("Created_Timestamp");
            entity.Property(e => e.RoomId).HasColumnName("RoomID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

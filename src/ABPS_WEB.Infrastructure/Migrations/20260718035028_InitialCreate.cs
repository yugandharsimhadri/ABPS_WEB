using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ABPS_WEB.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dump_ANNAD",
                columns: table => new
                {
                    ID = table.Column<double>(type: "float", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AMT = table.Column<double>(type: "float", nullable: true),
                    DAYS = table.Column<double>(type: "float", nullable: true),
                    SSMA_TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    OccassionID = table.Column<int>(type: "int", nullable: true),
                    NewBondNo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    DateType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    TeluguMasam = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    TeluguPaksham = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    TeluguThidhi = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    TeluguMasaID = table.Column<int>(type: "int", nullable: true),
                    TeluguPakshamid = table.Column<int>(type: "int", nullable: true),
                    TeluguThidhiid = table.Column<int>(type: "int", nullable: true),
                    EnglishDay = table.Column<int>(type: "int", nullable: true),
                    EnglishMonth = table.Column<int>(type: "int", nullable: true),
                    EnglishYear = table.Column<int>(type: "int", nullable: true),
                    ParseStatus = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true),
                    rawdata = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "dump_DONA",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAMEDON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WSNAME = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    DONTY = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    GOTHRAM = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    ADDRESSDON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AMTDON = table.Column<int>(type: "int", nullable: true, defaultValue: 0, comment: "RECEIVED"),
                    AMTPAID = table.Column<int>(type: "int", nullable: true, defaultValue: 0, comment: "PAYMENT"),
                    DATEOFDON = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true, defaultValue: new DateTime(2014, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified)),
                    DATEPAID = table.Column<DateTime>(type: "datetime2(0)", precision: 0, nullable: true),
                    DATEOFANN = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    USERD = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    CHDRNO = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true, defaultValue: "-"),
                    PFNO = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    MODE = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    PLACE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    VCNO = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, comment: "VOUCHER NO"),
                    DETAILS = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    ALOTDAYS = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)0),
                    LASTVIS = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    CODE = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    PART = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true, defaultValue: "F"),
                    TDAYS = table.Column<short>(type: "smallint", nullable: true, defaultValue: (short)0),
                    rupees = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "-"),
                    cash = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true, defaultValue: "-"),
                    occ = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "-"),
                    bdt = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValue: "-"),
                    PAN = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true, defaultValue: "-"),
                    SSMA_TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false),
                    DonarTypeID = table.Column<int>(type: "int", nullable: true),
                    NewBondNo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "dump_DONASHS",
                columns: table => new
                {
                    ID = table.Column<double>(type: "float", nullable: true),
                    NAMEDON = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    AMTDON = table.Column<double>(type: "float", nullable: true),
                    DATEOFANN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ADDRESSDON = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PHONENO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "((0))"),
                    SNO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true, defaultValueSql: "((0))"),
                    ID1 = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    T1 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    T2 = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SDA = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    SSMA_TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "dump_DONLIST",
                columns: table => new
                {
                    DONAME = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DTYPE = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    AMT = table.Column<int>(type: "int", nullable: true),
                    REMARKS = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ID = table.Column<int>(type: "int", nullable: true),
                    DAYS = table.Column<double>(type: "float", nullable: true),
                    SSMA_TimeStamp = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "dump_members",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAMEDON = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    DATEOFANN = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ADDRESSDON = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    PHONENO = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "dump_tbl_Donars",
                columns: table => new
                {
                    DonarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonarTypeID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telugu_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gotram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aadhaar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictID = table.Column<int>(type: "int", nullable: false),
                    StateID = table.Column<int>(type: "int", nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    NewBondNo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dump_tbl_Donars", x => x.DonarID);
                });

            migrationBuilder.CreateTable(
                name: "dump_tbl_Donation_Occation",
                columns: table => new
                {
                    DonationID = table.Column<int>(type: "int", nullable: false),
                    Donation_Occation_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonarID = table.Column<int>(type: "int", nullable: false),
                    OcassionID = table.Column<int>(type: "int", nullable: true),
                    OcassionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntheNameof = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telugu_IntheNameof = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Anandaanam_DateType = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false, defaultValue: "D"),
                    Anandaanam_Date = table.Column<DateOnly>(type: "date", nullable: true),
                    T_MasamID = table.Column<int>(type: "int", nullable: true),
                    T_PaskshamID = table.Column<int>(type: "int", nullable: true),
                    T_ThiduluID = table.Column<int>(type: "int", nullable: true),
                    BondNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomDays = table.Column<int>(type: "int", nullable: true),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Date_English_Telugu = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true, defaultValue: "E")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dump_tbl_Donation_Occation", x => x.DonationID);
                });

            migrationBuilder.CreateTable(
                name: "dump_tbl_Donations",
                columns: table => new
                {
                    DonationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonarID = table.Column<int>(type: "int", nullable: false),
                    DonationTypeID = table.Column<int>(type: "int", nullable: false),
                    DonationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentTypeID = table.Column<int>(type: "int", nullable: true),
                    PaymentSubTypeID = table.Column<int>(type: "int", nullable: true),
                    TransactionNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BondNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomDays = table.Column<int>(type: "int", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dump_tbl_Donations", x => x.DonationID);
                });

            migrationBuilder.CreateTable(
                name: "dumpDonationTypeMapping",
                columns: table => new
                {
                    DonationText = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    DonationTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "dumpOccasionMapping",
                columns: table => new
                {
                    OccasionText = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    OccasionTypeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "dumpTeluguMasamMap",
                columns: table => new
                {
                    Variant = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    MasamCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "dumpThidhiMap",
                columns: table => new
                {
                    Variant = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    ThidhiCode = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "PakshamMap",
                columns: table => new
                {
                    Variant = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    PakshamCode = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: true)
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_AddonRoom",
                columns: table => new
                {
                    AddonRoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddonBooking_Date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Booking_ID = table.Column<int>(type: "int", nullable: false),
                    Add_Advance = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Total_Advance = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Total_Amount = table.Column<decimal>(type: "decimal(18,0)", nullable: false),
                    Createdby = table.Column<string>(type: "nvarchar(max)", nullable: true, defaultValueSql: "(getdate())"),
                    Create_Timestmap = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "((0))")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AddonRoom", x => x.AddonRoomID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_AdvanceBooking",
                columns: table => new
                {
                    Advance_Booking_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Guest_Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Phone_No = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Aadhar_No = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    No_People = table.Column<int>(type: "int", nullable: false),
                    No_Rooms_Requested = table.Column<int>(type: "int", nullable: false),
                    Room_Category_RT_ID = table.Column<int>(type: "int", nullable: true),
                    Cat_ID = table.Column<int>(type: "int", nullable: true),
                    Bond_No = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Requested_CheckIn_Date = table.Column<DateTime>(type: "datetime", nullable: false),
                    Requested_Nights = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Advance_Deposit_Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Payment_Type_ID = table.Column<int>(type: "int", nullable: true),
                    Payment_SubType_ID = table.Column<int>(type: "int", nullable: true),
                    Reference_Number = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Status_ID = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Assigned_Booking_ID = table.Column<int>(type: "int", nullable: true),
                    Assigned_By = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Assigned_Timestamp = table.Column<DateTime>(type: "datetime", nullable: true),
                    Cancelled_Reason = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Created_By = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Modified_By = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Modified_Timestamp = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_AdvanceBooking", x => x.Advance_Booking_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BoardMembers",
                columns: table => new
                {
                    MemberID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    MemberTName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MemberType = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Phone = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Email = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    IsActive = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    EffectiveFrom = table.Column<DateOnly>(type: "date", nullable: true),
                    EffectiveTo = table.Column<DateOnly>(type: "date", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BoardMembers", x => x.MemberID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Booking_Room",
                columns: table => new
                {
                    Booking_Room_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Booking_id = table.Column<int>(type: "int", nullable: true),
                    Booking_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Room_ID = table.Column<int>(type: "int", nullable: false),
                    GroupBooking = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    RoomAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CheckIn_Date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CheckOut_Date = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    BookingStatusID = table.Column<int>(type: "int", nullable: true, defaultValue: 0),
                    Modified_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Modified_Timestamp = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())"),
                    CancellationType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "None")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Booking_Room", x => x.Booking_Room_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BookingCategory",
                columns: table => new
                {
                    CatID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Category", x => x.CatID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BookingDetails",
                columns: table => new
                {
                    Booking_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Booking_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aadhar_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    No_People = table.Column<int>(type: "int", nullable: true),
                    CatID = table.Column<int>(type: "int", nullable: false),
                    Details = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DTID = table.Column<int>(type: "int", nullable: false),
                    DonarID = table.Column<int>(type: "int", nullable: false),
                    BondNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfRooms = table.Column<int>(type: "int", nullable: true),
                    Advance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    GroupBooking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AdvanceBooking = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Booking_Status_ID = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    AddonRoom = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false, defaultValue: "N"),
                    Address = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BookingDetails", x => x.Booking_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BookingDocuments",
                columns: table => new
                {
                    BookingDocumentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Booking_ID = table.Column<int>(type: "int", nullable: false),
                    DocumentType = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    FileName = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: true),
                    ContentType = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Created_By = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BookingDocuments", x => x.BookingDocumentId);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BookingLedger",
                columns: table => new
                {
                    BookingLedger_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Booking_Id = table.Column<int>(type: "int", nullable: true),
                    NoOfDays = table.Column<int>(type: "int", nullable: true),
                    Checkin_DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Checkout_DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Advance = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Intended_TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Intended_RefundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Actual_TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Actual_RefundAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Checkin_byuser = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Checkin_insert_datetime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Checkout_byuser = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Checkout_insert_datetime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Modified_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Modified_Timestamp = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BookingLedger", x => x.BookingLedger_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_BookingStatus",
                columns: table => new
                {
                    BookingStatusID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status_Code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    BookingStatusDesc = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: true),
                    Created_By = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_BookingStatus", x => x.BookingStatusID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Cancelled_Room",
                columns: table => new
                {
                    Cancelled_Room_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Booking_id = table.Column<int>(type: "int", nullable: true),
                    RoomId = table.Column<int>(type: "int", nullable: false),
                    Cancelation_Date = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Cancelled_Room", x => x.Cancelled_Room_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Country",
                columns: table => new
                {
                    CountryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_Districts",
                columns: table => new
                {
                    DistrictID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateID = table.Column<int>(type: "int", nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    DistrictName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_Donars",
                columns: table => new
                {
                    DonarID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonarTypeID = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telugu_Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gotram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailID = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PAN = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Aadhaar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DistrictID = table.Column<int>(type: "int", nullable: false),
                    StateID = table.Column<int>(type: "int", nullable: false),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Donars", x => x.DonarID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_DonarType_Master",
                columns: table => new
                {
                    DTID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DONARTYPE = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DAYS = table.Column<int>(type: "int", nullable: true),
                    CODE = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: true),
                    AMOUNT = table.Column<int>(type: "int", nullable: true),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_DonarType", x => x.DTID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Donation_Occation",
                columns: table => new
                {
                    DonationID = table.Column<int>(type: "int", nullable: false),
                    Donation_Occation_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonarID = table.Column<int>(type: "int", nullable: false),
                    OcassionID = table.Column<int>(type: "int", nullable: true),
                    OcassionName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IntheNameof = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telugu_IntheNameof = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Anandaanam_DateType = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false, defaultValue: "D"),
                    Anandaanam_Date = table.Column<DateOnly>(type: "date", nullable: true),
                    T_MasamID = table.Column<int>(type: "int", nullable: true),
                    T_PaskshamID = table.Column<int>(type: "int", nullable: true),
                    T_ThiduluID = table.Column<int>(type: "int", nullable: true),
                    BondNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomDays = table.Column<int>(type: "int", nullable: true),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Date_English_Telugu = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true, defaultValue: "E")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Donation_Occation", x => x.DonationID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Donations",
                columns: table => new
                {
                    DonationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonarID = table.Column<int>(type: "int", nullable: false),
                    DonationTypeID = table.Column<int>(type: "int", nullable: false),
                    DonationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DonationDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    PaymentTypeID = table.Column<int>(type: "int", nullable: true),
                    PaymentSubTypeID = table.Column<int>(type: "int", nullable: true),
                    TransactionNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TransactionDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BondNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RoomDays = table.Column<int>(type: "int", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Donations", x => x.DonationID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_ErrorLogs",
                columns: table => new
                {
                    ErrorLogID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LogDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    LogLevel = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Logger = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Exception = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_Erro__D65247E2AA82BC5A", x => x.ErrorLogID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Extendroom",
                columns: table => new
                {
                    ExtendRoom_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bookingid = table.Column<int>(type: "int", nullable: false),
                    prevnoofdays = table.Column<int>(type: "int", nullable: false),
                    precheckout = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    extendeddays = table.Column<int>(type: "int", nullable: false),
                    newcheckout = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    totaldays = table.Column<int>(type: "int", nullable: false),
                    created_date = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    created_by = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Extendroom", x => x.ExtendRoom_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Floor_Master",
                columns: table => new
                {
                    Floor_ID = table.Column<short>(type: "smallint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Floor_Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Active = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Floor_Master", x => x.Floor_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Occassion_Master",
                columns: table => new
                {
                    OccassionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OccassionName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_Operators",
                columns: table => new
                {
                    OperatorID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperatorName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Password = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Status = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: true),
                    LastLoginDateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Operators", x => x.OperatorID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PaymentSubTypeMaster",
                columns: table => new
                {
                    paymentSubTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentSubType = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    paymentTypeID = table.Column<int>(type: "int", nullable: false),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PaymentSubTypeMaster", x => x.paymentSubTypeID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_PaymentTypeMaster",
                columns: table => new
                {
                    paymentTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentType = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_PaymentTypeMaster", x => x.paymentTypeID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Room",
                columns: table => new
                {
                    Room_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RT_ID = table.Column<int>(type: "int", nullable: true),
                    Floor_ID = table.Column<int>(type: "int", nullable: true),
                    Room_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cost = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Beds = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Allowed = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Room_Status_ID = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Booking_ID = table.Column<int>(type: "int", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    AdvanceAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Room", x => x.Room_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Room_Master_old",
                columns: table => new
                {
                    Room_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Room_No = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RT_ID = table.Column<int>(type: "int", nullable: true),
                    Floor_ID = table.Column<int>(type: "int", nullable: true),
                    Room_Status_ID = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    Booking_ID = table.Column<int>(type: "int", nullable: false),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Room_Master_1", x => x.Room_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Room_Status",
                columns: table => new
                {
                    Room_Status_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status_Code = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: false),
                    Room_Status_Desc = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Created_By = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Room_Status", x => x.Room_Status_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Room_Type",
                columns: table => new
                {
                    RT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RT_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Room_Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RT_Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RT_Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    is_active = table.Column<string>(type: "char(1)", unicode: false, fixedLength: true, maxLength: 1, nullable: false, defaultValue: "Y")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Room_Type", x => x.RT_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Room_Type_Master_old",
                columns: table => new
                {
                    RT_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RT_Desc = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RT_Price = table.Column<int>(type: "int", nullable: true),
                    RT_Bed = table.Column<int>(type: "int", nullable: true),
                    RT_AC = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true),
                    RT_Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RT_Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Room_Category = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false, defaultValue: "Normal")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Room_Master", x => x.RT_ID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Shift_Room",
                columns: table => new
                {
                    ShiftRoomID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Booking_id = table.Column<int>(type: "int", nullable: true),
                    Old_RoomID = table.Column<int>(type: "int", nullable: false),
                    New_RoomID = table.Column<int>(type: "int", nullable: false),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Shift_Room", x => x.ShiftRoomID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_SMSTemplate",
                columns: table => new
                {
                    SMSTemplateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SMSContent = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    OccassionID = table.Column<int>(type: "int", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_SMSTransaction",
                columns: table => new
                {
                    SMSID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonarID = table.Column<int>(type: "int", nullable: true),
                    BondNo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    OccasionID = table.Column<int>(type: "int", nullable: true),
                    Content = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Phone_no = table.Column<string>(type: "varchar(max)", unicode: false, nullable: false),
                    DateType = table.Column<string>(type: "varchar(1)", unicode: false, maxLength: 1, nullable: false, defaultValue: "D"),
                    SMSDate = table.Column<DateOnly>(type: "date", nullable: false, defaultValueSql: "(getdate())"),
                    T_MasamID = table.Column<int>(type: "int", nullable: true),
                    T_PaskshamID = table.Column<int>(type: "int", nullable: true),
                    T_ThiduluID = table.Column<int>(type: "int", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_SMSTransaction", x => x.SMSID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_States",
                columns: table => new
                {
                    StateID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CountryID = table.Column<int>(type: "int", nullable: false),
                    StateName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_TeluguMasam",
                columns: table => new
                {
                    T_MasamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    T_MasamName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    T_MasamName_English = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_TeluguPaksham",
                columns: table => new
                {
                    T_PakshamID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    T_PakshamName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    T_PakshamName_English = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_TeluguThidi",
                columns: table => new
                {
                    T_ThidiID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    T_ThidiName = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    T_ThidiName_English = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_Transaction",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionCategoryID = table.Column<int>(type: "int", nullable: true),
                    Donar_Booking_id = table.Column<int>(type: "int", nullable: true),
                    TrasactionTypeID = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    PaymentTypeID = table.Column<int>(type: "int", nullable: true),
                    PaymentSubTypeID = table.Column<int>(type: "int", nullable: true),
                    ReferenceNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    BondNo = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Transaction", x => x.TransactionID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Transaction_RoomDonation",
                columns: table => new
                {
                    TransactionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Booking_id = table.Column<int>(type: "int", nullable: true),
                    CheckoutDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    NoofDays = table.Column<int>(type: "int", nullable: true),
                    TotalAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    RoomAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    DonationAmount = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_By = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Transaction_RoomDonation", x => x.TransactionID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_TrasactionCategory_Master",
                columns: table => new
                {
                    TrasactionCategoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionCategory = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_TrasactionType_Master",
                columns: table => new
                {
                    TrasactionTypeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                });

            migrationBuilder.CreateTable(
                name: "tbl_User",
                columns: table => new
                {
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    MobileNumber = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: true),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<string>(type: "nvarchar(1)", maxLength: 1, nullable: true, defaultValue: "Y"),
                    CreatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedTimestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    UpdatedBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdatedTimestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__tbl_User__1788CCACCE2CA86A", x => x.UserName);
                });

            migrationBuilder.CreateTable(
                name: "tbl_User_Role",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_by = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    created_timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_User_Role", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "tbl_Vacate_Room",
                columns: table => new
                {
                    Vacate_Room_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Booking_id = table.Column<int>(type: "int", nullable: true),
                    RoomID = table.Column<int>(type: "int", nullable: false),
                    Checkout_DateTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    Created_By = table.Column<string>(type: "varchar(max)", unicode: false, nullable: true),
                    Created_Timestamp = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tbl_Vacate_Room", x => x.Vacate_Room_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_AddonRoom",
                table: "tbl_AddonRoom",
                column: "Booking_ID");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceBooking_DueDate",
                table: "tbl_AdvanceBooking",
                columns: new[] { "Requested_CheckIn_Date", "Status_ID" });

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceBooking_Phone",
                table: "tbl_AdvanceBooking",
                column: "Phone_No");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Booking_Room",
                table: "tbl_Booking_Room",
                column: "CheckIn_Date");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Booking_Room_1",
                table: "tbl_Booking_Room",
                column: "Room_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Booking_Room_2",
                table: "tbl_Booking_Room",
                column: "CheckOut_Date");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Booking_Room_BookingId",
                table: "tbl_Booking_Room",
                column: "Booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BookingDetails_BookingDate",
                table: "tbl_BookingDetails",
                columns: new[] { "Booking_Date", "Booking_Status_ID" });

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BookingDocuments_BookingId",
                table: "tbl_BookingDocuments",
                column: "Booking_ID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BookingLedger_BookingId",
                table: "tbl_BookingLedger",
                column: "Booking_Id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BookingLedger_CheckinDateTime",
                table: "tbl_BookingLedger",
                column: "Checkin_DateTime");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_BookingLedger_CheckoutDateTime",
                table: "tbl_BookingLedger",
                column: "Checkout_DateTime");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Donations_DonarId",
                table: "tbl_Donations",
                column: "DonarID");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Donations_DonationDate",
                table: "tbl_Donations",
                column: "DonationDate");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Transaction_TransactionDate",
                table: "tbl_Transaction",
                column: "TransactionDate");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Transaction_RoomDonation",
                table: "tbl_Transaction_RoomDonation",
                column: "Booking_id");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Transaction_RoomDonation_1",
                table: "tbl_Transaction_RoomDonation",
                column: "CheckoutDate");

            migrationBuilder.CreateIndex(
                name: "IX_tbl_Transaction_RoomDonation_CreatedTimestamp",
                table: "tbl_Transaction_RoomDonation",
                column: "Created_Timestamp");

            migrationBuilder.CreateIndex(
                name: "UQ__tbl_User__A9D1053447393A4F",
                table: "tbl_User",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "dump_ANNAD");

            migrationBuilder.DropTable(
                name: "dump_DONA");

            migrationBuilder.DropTable(
                name: "dump_DONASHS");

            migrationBuilder.DropTable(
                name: "dump_DONLIST");

            migrationBuilder.DropTable(
                name: "dump_members");

            migrationBuilder.DropTable(
                name: "dump_tbl_Donars");

            migrationBuilder.DropTable(
                name: "dump_tbl_Donation_Occation");

            migrationBuilder.DropTable(
                name: "dump_tbl_Donations");

            migrationBuilder.DropTable(
                name: "dumpDonationTypeMapping");

            migrationBuilder.DropTable(
                name: "dumpOccasionMapping");

            migrationBuilder.DropTable(
                name: "dumpTeluguMasamMap");

            migrationBuilder.DropTable(
                name: "dumpThidhiMap");

            migrationBuilder.DropTable(
                name: "PakshamMap");

            migrationBuilder.DropTable(
                name: "tbl_AddonRoom");

            migrationBuilder.DropTable(
                name: "tbl_AdvanceBooking");

            migrationBuilder.DropTable(
                name: "tbl_BoardMembers");

            migrationBuilder.DropTable(
                name: "tbl_Booking_Room");

            migrationBuilder.DropTable(
                name: "tbl_BookingCategory");

            migrationBuilder.DropTable(
                name: "tbl_BookingDetails");

            migrationBuilder.DropTable(
                name: "tbl_BookingDocuments");

            migrationBuilder.DropTable(
                name: "tbl_BookingLedger");

            migrationBuilder.DropTable(
                name: "tbl_BookingStatus");

            migrationBuilder.DropTable(
                name: "tbl_Cancelled_Room");

            migrationBuilder.DropTable(
                name: "tbl_Country");

            migrationBuilder.DropTable(
                name: "tbl_Districts");

            migrationBuilder.DropTable(
                name: "tbl_Donars");

            migrationBuilder.DropTable(
                name: "tbl_DonarType_Master");

            migrationBuilder.DropTable(
                name: "tbl_Donation_Occation");

            migrationBuilder.DropTable(
                name: "tbl_Donations");

            migrationBuilder.DropTable(
                name: "tbl_ErrorLogs");

            migrationBuilder.DropTable(
                name: "tbl_Extendroom");

            migrationBuilder.DropTable(
                name: "tbl_Floor_Master");

            migrationBuilder.DropTable(
                name: "tbl_Occassion_Master");

            migrationBuilder.DropTable(
                name: "tbl_Operators");

            migrationBuilder.DropTable(
                name: "tbl_PaymentSubTypeMaster");

            migrationBuilder.DropTable(
                name: "tbl_PaymentTypeMaster");

            migrationBuilder.DropTable(
                name: "tbl_Room");

            migrationBuilder.DropTable(
                name: "tbl_Room_Master_old");

            migrationBuilder.DropTable(
                name: "tbl_Room_Status");

            migrationBuilder.DropTable(
                name: "tbl_Room_Type");

            migrationBuilder.DropTable(
                name: "tbl_Room_Type_Master_old");

            migrationBuilder.DropTable(
                name: "tbl_Shift_Room");

            migrationBuilder.DropTable(
                name: "tbl_SMSTemplate");

            migrationBuilder.DropTable(
                name: "tbl_SMSTransaction");

            migrationBuilder.DropTable(
                name: "tbl_States");

            migrationBuilder.DropTable(
                name: "tbl_TeluguMasam");

            migrationBuilder.DropTable(
                name: "tbl_TeluguPaksham");

            migrationBuilder.DropTable(
                name: "tbl_TeluguThidi");

            migrationBuilder.DropTable(
                name: "tbl_Transaction");

            migrationBuilder.DropTable(
                name: "tbl_Transaction_RoomDonation");

            migrationBuilder.DropTable(
                name: "tbl_TrasactionCategory_Master");

            migrationBuilder.DropTable(
                name: "tbl_TrasactionType_Master");

            migrationBuilder.DropTable(
                name: "tbl_User");

            migrationBuilder.DropTable(
                name: "tbl_User_Role");

            migrationBuilder.DropTable(
                name: "tbl_Vacate_Room");
        }
    }
}

using ABPS_WEB.Api.Auth;
using ABPS_WEB.Domain.Admin;
using ABPS_WEB.Domain.Auth;
using ABPS_WEB.Domain.AdvanceBooking;
using ABPS_WEB.Domain.Donations;
using ABPS_WEB.Domain.Greetings;
using ABPS_WEB.Domain.Lookups;
using ABPS_WEB.Domain.Notifications;
using ABPS_WEB.Domain.Reports;
using ABPS_WEB.Domain.RoomOps;
using ABPS_WEB.Infrastructure.Admin;
using ABPS_WEB.Infrastructure.AdvanceBooking;
using ABPS_WEB.Infrastructure.Auth;
using ABPS_WEB.Infrastructure.Data;
using ABPS_WEB.Infrastructure.Donations;
using ABPS_WEB.Infrastructure.Greetings;
using ABPS_WEB.Infrastructure.Lookups;
using ABPS_WEB.Infrastructure.Notifications;
using ABPS_WEB.Infrastructure.Reports;
using ABPS_WEB.Infrastructure.RoomOps;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<AbpsContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("AbpsContext"),
        sql => sql.CommandTimeout(30)));

builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IRoomService, RoomService>();
builder.Services.AddScoped<IDonationService, DonationService>();
builder.Services.AddScoped<ILookupService, LookupService>();
builder.Services.AddScoped<IAdvanceBookingService, AdvanceBookingService>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IAdminService, AdminService>();
builder.Services.AddScoped<IGreetingsService, GreetingsService>();
builder.Services.AddHttpClient<IWhatsAppClient, WhatsAppClient>();
builder.Services.AddScoped<IWhatsAppNotifier, WhatsAppNotifier>();
builder.Services.AddSingleton<LoginLockoutTracker>();
builder.Services.AddSingleton<JwtTokenService>();

var jwtKey = builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(string.IsNullOrEmpty(jwtKey) ? "unset-placeholder-key-set-jwt-key-before-real-use" : jwtKey))
        };
    });
builder.Services.AddAuthorization();

var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>() ?? Array.Empty<string>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
        policy.WithOrigins(allowedOrigins).AllowAnyHeader().AllowAnyMethod());
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

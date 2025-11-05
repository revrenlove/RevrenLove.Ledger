using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using RevrenLove.Ledger.Api.Auth;
using RevrenLove.Ledger.Api.Configuration;
using RevrenLove.Ledger.Entities;
using RevrenLove.Ledger.Persistence;

var builder = WebApplication.CreateBuilder(args);

var appSettings = builder.Configuration.Get<AppSettings>()!;

builder
    .Services
    // TODO: JE - Change per environment...
    .AddSQLiteDbContext(appSettings.ConnectionStrings.Default)
    .AddIdentityApiEndpoints<LedgerUser>()
    .AddEntityFrameworkStores<LedgerDbContext>();

builder.Services.AddScoped<IJwtTokenService, JwtTokenService>();
builder.Services.AddSingleton<JwtSecurityTokenHandler>();

// Authentication: policy scheme that chooses cookie or bearer
builder.Services.AddAuthentication(options =>
{
    // Default scheme is a "selector" below
    // TODO: JE - Rename this AI Bullshit "Smart" scheme... and use a Config value or something...
    options.DefaultScheme = "SmartScheme";
})
.AddPolicyScheme("SmartScheme", "Selects between JWT and Cookie", options =>
{
    options.ForwardDefaultSelector = context =>
    {
        var scheme = IdentityConstants.ApplicationScheme;

        var authHeader = context.Request.Headers.Authorization.FirstOrDefault();
        if (!string.IsNullOrEmpty(authHeader))
        {
            scheme = JwtBearerDefaults.AuthenticationScheme;
        }

        return scheme;
    };
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidIssuer = appSettings.Jwt.Issuer,
        ValidateAudience = true,
        ValidAudience = appSettings.Jwt.Audience,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.Jwt.Key))
    };
})
.AddGoogle(options =>
{
    options.ClientId = appSettings.Authentication.Google.ClientId;
    options.ClientSecret = appSettings.Authentication.Google.ClientSecret;
});

builder.Services.AddAuthorization();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // TODO: JE - Add stuff for bearer tokens in swagger UI
});

builder.Services.AddOpenApi();

builder.Services
    // TODO: JE - This is for DEV shit only!!!!
    // Enable CORS for everything
    .AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
        {
            policy
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapIdentityApi<LedgerUser>();

app.MapControllers();

// TODO: JE - Add exception handling middleware...


app.Run();

// TODO: JE - I don't like this...
// public record LoginDto(string Email, string Password);
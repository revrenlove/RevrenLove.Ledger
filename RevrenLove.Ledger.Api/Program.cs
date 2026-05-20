using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Api;
using RevrenLove.Ledger.Entities;
using RevrenLove.Ledger.Persistence.SQLite;
using RevrenLove.Ledger.Persistence.SqlServer;

var builder = WebApplication.CreateBuilder(args);

#region Register Services

string? connectionString = builder.Configuration.GetConnectionString("Production");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("Connection string 'Production' not found in configuration or user secrets.");
}

builder.Services
    .AddAuthentication(IdentityConstants.ApplicationScheme)
    .AddIdentityCookies();

builder.Services
    .AddAuthorizationBuilder();

builder.Services
    // TODO: JE - Figure out all this CORS shit...
    // .AddLedgerApiCors(builder.Environment)
    .AddCors(options =>
    {
        options.AddPolicy("AllowFrontend",
            policy =>
                policy
                    .WithOrigins("http://localhost:5088", "https://localhost:7171", "https://localhost:7125")
                    .AllowCredentials()
                    .AllowAnyHeader()
                    .AllowAnyMethod());
    })
    .AddEndpointsApiExplorer()
    .AddSwaggerGen();

// Register DbContext first, before services that depend on it
builder.Services.AddRevrenLedgerSqlServerDbContext(connectionString);

builder.Services
    .AddIdentityCore<LedgerUser>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<LedgerSqlServerDbContext>()
    .AddApiEndpoints();

// Now register services that depend on the DbContext
builder.Services
    .AddLedgerServices()
    .AddSingleton<Mapper>();

Console.WriteLine(connectionString);

builder.Services.AddControllers();

#endregion

var app = builder.Build();

#region Web Application Configuration

app.UseHttpsRedirection();

app.UseCors("AllowFrontend");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<LedgerUser>();
app.UseSwagger();
app.UseSwaggerUI();

// TODO: JE - Include this in the SimplishAuth library...
// TODO: JE - Make sure this code is _exactly_ what we want...
// provide an endpoint to clear the cookie for logout
//
// For more information on the logout endpoint and anti-forgery, see:
// https://learn.microsoft.com/aspnet/core/blazor/security/webassembly/standalone-with-identity#antiforgery-support
app.MapPost("/logout", async (SignInManager<LedgerUser> signInManager, [FromBody] object empty) =>
{
    if (empty is not null)
    {
        await signInManager.SignOutAsync();

        return Results.Ok();
    }

    return Results.Unauthorized();
}).RequireAuthorization();

#endregion

app.Run();

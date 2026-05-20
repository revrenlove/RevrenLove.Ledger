using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Api;
using RevrenLove.Ledger.Entities;
using RevrenLove.Ledger.Persistence.SQLite;

var builder = WebApplication.CreateBuilder(args);

#region Register Services

var connectionString = builder.Environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("Development")!
    : builder.Configuration.GetConnectionString("Production")!;

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
    .AddSwaggerGen()
    .AddLedgerServices()
    .AddSingleton<Mapper>();

if (builder.Environment.IsDevelopment())
{
    builder.Services.AddRevrenLedgerSQLiteDbContext(connectionString);
}
else
{
    builder.Services.AddRevrenLedgerSqlServerDbContext(connectionString);
}

builder.Services
    .AddIdentityCore<LedgerUser>()
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<LedgerSQLiteDbContext>()
    .AddApiEndpoints();


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

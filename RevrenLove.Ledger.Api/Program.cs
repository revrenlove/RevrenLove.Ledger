using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RevrenLove.Ledger.Entities;
using RevrenLove.Ledger.Persistence.SQLite;

var builder = WebApplication.CreateBuilder(args);

#region Register Services

var connectionString = builder.Configuration.GetConnectionString("Default")!;

builder.Services
    .AddAuthorization()
    .AddLedgerApiCors(builder.Environment)
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddRevrenLedgerSQLiteDbContext(connectionString)
    .AddIdentityApiEndpoints<LedgerUser>()
    // TODO: JE - Figure out how to make this agnostic for when we switch db's per env
    .AddEntityFrameworkStores<LedgerSQLiteDbContext>();

builder.Services.AddControllers();

#endregion

var app = builder.Build();

#region Web Application Configuration

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

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

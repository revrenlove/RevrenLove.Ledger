using RevrenLove.Ledger.Entities;
using RevrenLove.Ledger.Persistence.SQLite;

var builder = WebApplication.CreateBuilder(args);

#region Register Services

var connectionString = builder.Configuration.GetConnectionString("Default")!;

builder.Services
    .AddAuthorization()
    .AddLedgerApiCors(builder.Environment)
    .AddRevrenLedgerSQLiteDbContext(connectionString)
    .AddIdentityApiEndpoints<LedgerUser>()
    // TODO: JE - Figure out how to make this agnostic for when we switch db's per env
    .AddEntityFrameworkStores<LedgerSQLiteDbContext>();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

#endregion

var app = builder.Build();

#region Web Application Configuration

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.MapControllers();
app.MapIdentityApi<LedgerUser>();

#endregion

app.Run();

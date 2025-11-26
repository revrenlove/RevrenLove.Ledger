using RevrenLove.Ledger.Entities;

var builder = WebApplication.CreateBuilder(args);

#region Register Services

var connectionString = builder.Configuration.GetConnectionString("Default")!;

builder.Services
    .AddAuthorization()
    .AddLedgerApiCors(builder.Environment)
    .AddRevrenLedgerSQLiteDbContext(connectionString)
    .AddIdentityApiEndpoints<LedgerUser>()
    .AddLedgerEntityFrameworkStores();

builder.Services.AddControllers();

builder.Services.AddOpenApi();

#endregion

var app = builder.Build();

#region Web Application Configuration

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseCors();

app.UseAuthorization();

app.MapControllers();
app.MapIdentityApi<LedgerUser>();

#endregion

app.Run();

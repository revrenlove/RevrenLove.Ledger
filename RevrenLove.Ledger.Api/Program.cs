using Microsoft.EntityFrameworkCore;
using RevrenLove.Ledger.Entities;
using RevrenLove.Ledger.Persistence;

// TODO: JE - Move this into the config...
var scopes = new[]
{
    "scp:openid",
    "scp:email",
    "scp:profile",
    "api"
};
var clientId = "RevrenLoveLedgerBlazorClient";

var builder = WebApplication.CreateBuilder(args);

// TODO: JE - Change this depending on environment (DEV/PROD)...
var connectionString = builder.Configuration.GetConnectionString("Default")!;

builder
    .Services
    // TODO: JE - Change this for PROD - This is DEV ONLY!!!!!
    .AddSQLiteDbContext(connectionString)
    .AddAuthorization()
    .AddIdentityApiEndpoints<LedgerUser>()
    .AddLedgerEntityFrameworkStores();

builder
    .Services
    .AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = builder.Configuration["Authentication:Google:ClientId"]!;
        options.ClientSecret = builder.Configuration["Authentication:Google:ClientSecret"]!;
    });

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
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapIdentityApi<LedgerUser>();

// TODO: JE - Add exception handling middleware...


app.Run();

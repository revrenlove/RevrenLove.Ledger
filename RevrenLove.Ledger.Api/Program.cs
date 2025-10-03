using RevrenLove.Ledger.Entities;

var builder = WebApplication.CreateBuilder(args);

builder
    .Services
    // TODO: JE - Change this for PROD - This is DEV ONLY!!!!!
    .AddSQLiteDbContext(builder.Configuration.GetConnectionString("Default")!)
    .AddAuthorization()
    .AddIdentityApiEndpoints<LedgerUser>()
    .AddLedgerEntityFrameworkStores();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // options.
});
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
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

// TODO: JE - Figure out if we need this...
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapIdentityApi<LedgerUser>();

app.UseCors();

// TODO: JE - Add exception handling middleware...

app.MapControllers();

app.Run();


// TODO: JE - Add an argument to the Program class to allow for different environments (e.g., development, production).
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets(typeof(Program).Assembly);
var connectionString = builder.Configuration.GetConnectionString("Default")!;

// TODOL JE - This is where you would add other database providers (e.g., InMemory, PostgreSQL, etc.) based on configuration.
builder
    .Services
    .AddSqlServerDbContext(connectionString)
    .AddLedgerServices();

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();

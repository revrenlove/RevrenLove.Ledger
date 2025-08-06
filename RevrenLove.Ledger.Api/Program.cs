var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets(typeof(Program).Assembly);

builder
    .Services
    .AddLedgerDatabase(builder.Configuration, builder.Environment.EnvironmentName)
    .AddLedgerServices()
    .AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

// TODO: JE - Add auth
// app.UseAuthorization();

app.MapControllers();

app.Run();

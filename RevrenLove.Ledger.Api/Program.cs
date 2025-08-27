using RevrenLove.Ledger.Api.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets(typeof(Program).Assembly);

builder
    .Services
    .AddLedgerDatabase(builder.Configuration, builder.Environment.EnvironmentName)
    .AddLedgerServices()
    .AddControllers()
    .Services
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

app.UseHttpsRedirection();

// TODO: JE - Add auth
// app.UseAuthorization();

// TODO: JE - This is for DEV shit only!!!!
// Enable CORS for everything
app.UseCors();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddUserSecrets(typeof(Program).Assembly);
var connectionString = builder.Configuration.GetConnectionString("Default")!;
builder.Services.AddDbContext(connectionString);

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.Run();

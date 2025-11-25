var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// TODO: JE - Make this not magic
builder.Services.AddRevrenLedgerSQLiteDbContext("Data Source=RevrenLoveLedger.db");

builder.Services.AddControllers();
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();

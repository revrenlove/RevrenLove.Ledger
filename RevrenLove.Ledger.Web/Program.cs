using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RevrenLove.Ledger.Web;
using RevrenLove.Ledger.Web.Auth;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var ledgerApiClientBaseAddress = builder.Configuration.GetValue<string>("LedgerApiClientBaseAddress")!;
var ledgerUiBaseAddress = builder.Configuration.GetValue<string>("LedgerUiBaseAddress")!;

builder
    .Services
        .AddLedgerApiClient(ledgerApiClientBaseAddress, configure =>
        {
            configure.AddHttpMessageHandler<CookieHandler>();
        })
        .AddTransient<CookieHandler>()
        .AddAuthorizationCore()
        .AddScoped<AuthenticationStateProvider, CookieAuthenticationStateProvider>()
        .AddScoped(sp => (IAccountManagement)sp.GetRequiredService<AuthenticationStateProvider>())
        .AddScoped(_ => new HttpClient { BaseAddress = new(ledgerUiBaseAddress) });

await builder.Build().RunAsync();

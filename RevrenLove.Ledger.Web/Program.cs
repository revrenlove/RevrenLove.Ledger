using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using RevrenLove.Ledger.Web;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var ledgerApiClientBaseAddress = builder.Configuration.GetValue<string>("LedgerApiClientBaseAddress")!;

builder.Services.AddLedgerApiClient(ledgerApiClientBaseAddress);

await builder.Build().RunAsync();

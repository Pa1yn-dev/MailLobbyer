using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MailLobbyer.CSVServiceComponent;
using MailLobbyer.CSVFileClass;
using Microsoft.AspNetCore.ResponseCompression;
using MailLobbyer.Server.Hubs;
using ElectronNET.API;
using ElectronNET.API.Entities;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseElectron(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddResponseCompression(opts =>
{
   opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
         new[] { "application/octet-stream" });
});

builder.Services.AddElectron();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseResponseCompression();
app.MapHub<CSVHub>("/csvhub");
app.MapHub<EmailHub>("/emailhub");
app.MapHub<SettingsHub>("/settingshub");

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");


await app.StartAsync();

var electronBrowserWindowOptions = new BrowserWindowOptions {AutoHideMenuBar = true};
await Electron.WindowManager.CreateWindowAsync(electronBrowserWindowOptions);


app.WaitForShutdown();




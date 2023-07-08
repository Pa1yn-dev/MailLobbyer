using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using MailLobbyer.CSVServiceComponent;
using MailLobbyer.CSVFileClass;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

//Checks for a folder for CSV files for contact groups in easy to access locations on both Linux and Windows, if it does not exist a folder is created.
string documentspath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
string directoryname = "MailLobbyer";
string directorypath = Path.Combine(documentspath, directoryname);

if (!Directory.Exists(directorypath))
{
    Directory.CreateDirectory(directorypath);
    Console.WriteLine("CSV contact-grouping directory created successfully.");
}
else
{
    Console.WriteLine("CSV contact-grouping directory already exists.");
}

CSVService csvserviceinstance = new CSVService();
csvserviceinstance.CSVFileSeeker(directorypath);

foreach (CSVFile csvfile in csvserviceinstance.CSVFilesindir)
{
    await csvserviceinstance.CSVParser(csvfile.Filepath);
}

app.Run();



using System.Reflection;
using Microsoft.AspNetCore.ResponseCompression;
using ProductViewer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Add http client to the container
builder.Services.AddHttpClient();
// Add the appsettings.json file to the container
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
// Add the appsettings file to the container depending on the environment
builder.Configuration.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true);
// Add ProductApiService to the container
builder.Services.AddTransient<IProductsApi, ProductsApi>();
// Add ResponseCompression to the container
builder.Services.AddResponseCompression(options => {
    options.Providers.Add<BrotliCompressionProvider>();
    options.Providers.Add<GzipCompressionProvider>();
    options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
});
// Add ResponseCache to the container
builder.Services.AddResponseCaching(options => {
    options.MaximumBodySize = 1024;
    options.UseCaseSensitivePaths = true;
});

var app = builder.Build();

// Configure logging using log4net
var repository = log4net.LogManager.GetRepository(Assembly.GetEntryAssembly());
var fileInfo = new FileInfo(@"log4net.config");
log4net.Config.XmlConfigurator.Configure(repository, fileInfo);

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

// Enable response compression
app.UseResponseCompression();
app.UseResponseCaching();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");

app.MapFallbackToFile("index.html");

app.Run();
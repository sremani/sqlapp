using Microsoft.FeatureManagement;
using sqlapp.Services;

var builder = WebApplication.CreateBuilder(args);
var connectionString = "Endpoint=https://azureconfig1000.azconfig.io;Id=xuIh;Secret=+O+mM9i2/yWvOOS1+ZXpqT2dWW0/Pk4wIKztRJX5+RY=";
builder.Host.ConfigureAppConfiguration(builder =>
    {
        builder.AddAzureAppConfiguration(options => options.Connect(connectionString).UseFeatureFlags()); 
    });
// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddTransient<IProductService, ProductService>();
builder.Services.AddFeatureManagement();

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

app.UseAuthorization();

app.MapRazorPages();



app.Run();

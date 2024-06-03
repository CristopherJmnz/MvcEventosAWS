using MvcEventosAWS.Helpers;
using MvcEventosAWS.Models;
using MvcEventosAWS.Services;
using Newtonsoft.Json;

async Task<string> GetSecretAsync()
{
    return await HelperSecretManager.GetSecretAsync();
}

var builder = WebApplication.CreateBuilder(args);
string secret = GetSecretAsync().GetAwaiter().GetResult();

KeysModel keys = JsonConvert.DeserializeObject<KeysModel>(secret);
string connectionString = keys.ConnectionString;
builder.Services.AddTransient<EventosService>();
builder.Services.AddSingleton<KeysModel>(x => keys);
// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Eventos}/{action=Index}/{id?}");

app.Run();

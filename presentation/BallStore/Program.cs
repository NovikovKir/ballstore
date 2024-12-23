using Store;
using Store.Memory;
using Store.Messages;
using Store.Contractors;
using BallStore.YandexKassa;
using BallStore.Contractors;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
}

);
builder.Services.AddSingleton<IBallRepository, BallRepository>();
builder.Services.AddSingleton<IOrderRepository, OrderRepository>();
builder.Services.AddSingleton<BallService>();
builder.Services.AddSingleton<INotificationService, DebugNotificationService>();
builder.Services.AddSingleton<IDeliveryService, PostamateDeliveryService>();
builder.Services.AddSingleton<IPaymentService, CashPaymentService>();
builder.Services.AddSingleton<IPaymentService, YandexKassaPaymentService>();
builder.Services.AddSingleton<IWebContractorsService, YandexKassaPaymentService>();


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

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapAreaControllerRoute(
    name: "yandex.kassa",
    areaName: "YandexKassa",
    pattern: "YandexKassa/{controller=Home}/{action=Index}/{id?}");

app.Run();

using System.Runtime;
using Application.Services.Behaviour;
using Application.Services.Interface;
using Microsoft.Extensions.Configuration;
using Persistence;
using Persistence.Context.Behaviour;
using Persistence.Context.Interface;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<DbConfig>(options => builder.Configuration.GetSection("DbConfig").Bind(options));

builder.Services.AddSingleton<IDbClient, DbClient>();
builder.Services.AddScoped<IBookService, BookService>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromHours(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

builder.Services.AddControllersWithViews().AddRazorRuntimeCompilation();

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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

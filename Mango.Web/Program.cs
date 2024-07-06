using Mango.Web.Service;
using Mango.Web.Service.IService;
using Mango.Web.Utility;

namespace Mango.Web;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();

        //Registering Service For IHttpClientFactory
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddHttpClient();
        builder.Services.AddHttpClient<ICouponService,CouponService>();
        // This is Configuring Base Url
       SD.CouponAPIBase = builder.Configuration["ServiceUrls:CouponApi"];

        builder.Services.AddScoped<ICouponService,CouponService>();
        builder.Services.AddScoped<IBaseService, BaseService>();

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
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}

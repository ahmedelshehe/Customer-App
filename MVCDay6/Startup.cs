using Microsoft.EntityFrameworkCore;
using MVCDay6.Models;
using MVCDay6.Repositories.CustomerRepositories;
using MVCDay6.Repositories.OrderRepositories;
using MVCDay6.Repositories.ProductRepositories;

namespace MVCDay6
{
    public class Startup
    {

        public Startup(IConfiguration configuration, IWebHostEnvironment webHost)
        {
            Configuration = configuration;
            WebHost = webHost;
        }

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment WebHost { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<Context>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddControllersWithViews();
        }
        public void Configure(IApplicationBuilder app)
        {

            if (!WebHost.IsEnvironment("Development"))
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseStaticFiles();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}

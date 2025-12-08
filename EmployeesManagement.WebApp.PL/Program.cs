using EmployeesManagement.WebApp.DAL.Persistence.Data.Contexts;
using EmployeesManagement.WebApp.DAL.Persistence.Repositories.Departments;
using Microsoft.EntityFrameworkCore;

namespace EmployeesManagement.WebApp.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<ApplicationDbContext>(optionsBuilder => {
                //optionsBuilder.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                //});
                optionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                });

            builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            app.MapStaticAssets();
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}")
                .WithStaticAssets();
            
            app.Run();
        }
    }
}

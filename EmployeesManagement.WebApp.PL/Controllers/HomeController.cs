using System.Diagnostics;
using EmployeesManagement.WebApp.DAL.Persistence.Data.Contexts;
using EmployeesManagement.WebApp.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeesManagement.WebApp.PL.Controllers
{
    public class HomeController : Controller
    { 
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

using EmployeesManagement.WebApp.BLL.DTOs;
using EmployeesManagement.WebApp.BLL.Services.Departments;
using EmployeesManagement.WebApp.PL.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace EmployeesManagement.WebApp.PL.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto departmentDto)
        {
            if (!ModelState.IsValid)
                return View(departmentDto);

            var message = string.Empty;

            try
            {
                var result = _departmentService.CreateDepartment(departmentDto);

                

                if (result > 0)
                    return RedirectToAction(nameof(Index));

                else
                {
                    message = "An error occurred while creating the department.";
                    ModelState.AddModelError(string.Empty, message);

                    return View(departmentDto);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                var errorViewModel = new ErrorViewModel
                {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
                };

                return View("Error", errorViewModel);
            }
        }

        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var department = _departmentService.GetDepartmentsById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }
    }

}

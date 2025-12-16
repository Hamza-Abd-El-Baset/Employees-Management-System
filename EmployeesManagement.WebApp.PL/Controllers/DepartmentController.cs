using EmployeesManagement.WebApp.BLL.DTOs;
using EmployeesManagement.WebApp.BLL.Services.Departments;
using EmployeesManagement.WebApp.PL.ViewModels.Common;
using EmployeesManagement.WebApp.PL.ViewModels.Departments;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Build.Tasks;
using System.Diagnostics;

namespace EmployeesManagement.WebApp.PL.Controllers
{
    public class DepartmentController : Controller
    {
        #region Services
        
        private readonly IDepartmentService _departmentService;
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(IDepartmentService departmentService, ILogger<DepartmentController> logger, IWebHostEnvironment environment)
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }

        #endregion

        #region Index

        [HttpGet]
        public IActionResult Index()
        {
            var departments = _departmentService.GetAllDepartments();

            return View(departments);
        }

        #endregion

        #region Create

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
        
        #endregion

        #region Details

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

        #endregion

        #region Edit

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (!id.HasValue)
                return BadRequest();

            var department = _departmentService.GetDepartmentsById(id.Value);

            if (department is null)
                return NotFound();

            return View(new DepartmentEditViewModel()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate
            });
        }

        [HttpPost]
        public IActionResult Edit([FromRoute] int id, DepartmentEditViewModel departmentVM)
        {
            if (!ModelState.IsValid)
                return View(departmentVM);

            string message;

            try
            {
                var updatedDepartment = new UpdatedDepartmentDto()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate
                };

                bool isUpdated = _departmentService.UpdateDepartment(updatedDepartment) > 0;

                if (!isUpdated)
                {
                    message = "An error occurred while updating the department.";

                    ModelState.AddModelError(string.Empty, message);

                    return View(departmentVM);
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An error occurred while updating the department.";

                ModelState.AddModelError(string.Empty, message);

                return View(departmentVM);
            }

        } 
        
        #endregion

        #region Delete

        //[HttpGet]
        //public IActionResult Delete(int? id)
        //{

        //    if (!id.HasValue)
        //        return BadRequest();

        //    var department = _departmentService.GetDepartmentsById(id.Value);

        //    if (department is null)
        //        return NotFound();

        //    return View(department);
        //}

        [HttpPost]
        public IActionResult Delete([FromRoute] int id)
        {
            string message;

            try
            {

                bool isDeleted = _departmentService.DeleteDepartment(id);

                if (!isDeleted)
                {
                    message = "An error occurred while deleting the department.";

                    ModelState.AddModelError(string.Empty, message);

                    return RedirectToAction(nameof(Delete), new { id });
                }

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                message = _environment.IsDevelopment() ? ex.Message : "An error occurred while deleting the department.";

                ModelState.AddModelError(string.Empty, message);

                return RedirectToAction(nameof(Delete), new { id });
            }
        }

        #endregion

    }
}

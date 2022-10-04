using CRUDEmployeesProject.Data;
using CRUDEmployeesProject.Models;
using CRUDEmployeesProject.Models.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CRUDEmployeesProject.Controllers
{
    public class Employees : Controller
    {
        private readonly MVCDemoDBContext mvcDemoDbContext;
        public Employees(MVCDemoDBContext mvcDbContext)
        {
            this.mvcDemoDbContext = mvcDbContext;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var employees = await this.mvcDemoDbContext.Employees.ToListAsync();
            return View(employees);
        }

        [Authorize]
        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddEmployeeViewModel addEmployeeRequest)
        {
             var employee = new Employee()
             {
                 Id = Guid.NewGuid(),
                 Name = addEmployeeRequest.Name,
                 Email = addEmployeeRequest.Email,
                 Salary = addEmployeeRequest.Salary,
                 Department = addEmployeeRequest.Department,
                 DateOfBirth = addEmployeeRequest.DateOfBirth,
             };
             Console.Write(employee);
             await mvcDemoDbContext.Employees.AddAsync(employee);
             await mvcDemoDbContext.SaveChangesAsync();
             return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> View(Guid Id)
        {
            var employee = await this.mvcDemoDbContext.Employees.FirstOrDefaultAsync(x => x.Id == Id);

            if (employee != null)
            {
                var viewModel = new UpdateEmployeeViewModel()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Email = employee.Email,
                    Salary = employee.Salary,
                    Department = employee.Department,
                    DateOfBirth = employee.DateOfBirth,
                };

                return await Task.Run( () => View("View", viewModel));
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> View(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                employee.Name = model.Name;
                employee.Email = model.Email;
                employee.Salary = model.Salary;
                employee.DateOfBirth = model.DateOfBirth;
                employee.Department = model.Department;

                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(UpdateEmployeeViewModel model)
        {
            var employee = await mvcDemoDbContext.Employees.FindAsync(model.Id);

            if (employee != null)
            {
                mvcDemoDbContext.Employees.Remove(employee);
                await mvcDemoDbContext.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }
    }
}

using MasterCompanyAPI.Interfaces;
using MasterCompanyAPI.Models;
using MasterCompanyAPI.Repositories;
using MasterCompanyAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace MasterCompanyAPI.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeService employeeService;
        private readonly EmployeeRepository employeeRepo;

        public EmployeeController() {
            employeeService = new EmployeeService();
            employeeRepo = new EmployeeRepository();
        }

        [Route("{document}")]
        [HttpGet, ActionName("Get")]
        [SwaggerOperation(Summary = "- Gets a employee by his/her document")]
        public async Task<JsonResult> Get(string? document)
        {
            Employee? employee = await employeeRepo.GetEmployeeByDocument(document);
            object data =  (employee != null)? new { employee } : new { };
            return new JsonResult(new { data });
        }

        [Route("salary-range/")]
        [HttpGet, ActionName("GetBySalaryRange")]
        [SwaggerOperation(Summary = "- Gets employees by a salary range (from, to)")]
        public async Task<JsonResult> GetBySalaryRange([Required]double? from, [Required]double? to)
        {
            List<Employee>? employees = await employeeRepo.GetEmployeesBySalaryRange(from, to);
            var data = new { total = employees.Count, employees };
            return new JsonResult(new { data });
        }

        [Route("salary-raise")]
        [HttpGet, ActionName("GetSalaryRaise")]
        [SwaggerOperation(Summary = "- Gets employees with their Salary increases")]
        public async Task<JsonResult> GetSalaryRaise()
        {
            var data = await employeeService.GetSalaryRaise();
            return new JsonResult(new { data });
        }

        [HttpGet, ActionName("Get")]
        [SwaggerOperation(Summary = "- Gets all employees (duplicated, enabled, disabled)")]
        public async Task<JsonResult> Get()
        {
            List<Employee> employees = await employeeRepo.GetEmployees();
            var data = new { total = employees.Count, employees };
            return new JsonResult(new { data });
        }

        [Route("no-duplicated")]
        [HttpGet, ActionName("GetNoDuplicated")]
        [SwaggerOperation(Summary = "- Gets employees (no duplicated)")]
        public async Task<JsonResult> GetNoDuplicated()
        {
            List<Employee> employees = await employeeRepo.GetNoDuplicatedEmployees();
            var data = new { total = employees.Count, employees };
            return new JsonResult(new { data });
        }

        [Route("gender-percentages")]
        [HttpGet, ActionName("GetGenderPercentages")]
        [SwaggerOperation(Summary = "- Gets the gender percentages of employees")]
        public async Task<JsonResult> GetGenderPercentages()
        {
            var data = await employeeService.GetGenderPercentages();
            return new JsonResult(new { data });
        }

        [Route("add")]
        [HttpPost, ActionName("Post")]
        [SwaggerOperation(Summary = "- Add an employee")]
        public async Task<JsonResult> Post([FromBody] Employee? employee)
        {
            var data = new { EmployeeAdded = await employeeRepo.AddEmployee(employee) };
            return new JsonResult(new { data });
        }

        [Route("update")]
        [HttpPut, ActionName("Put")]
        public async Task<JsonResult> Put([FromBody] Employee employee)
        {
            await Task.Delay(3000);
            return new JsonResult(new { employee });
        }

        [Route("delete/{id:int}")]
        [HttpDelete, ActionName("Delete")]
        public async Task<JsonResult> Delete(int? id)
        {
            await Task.Delay(3000);
            return new JsonResult(new { id });
        }

    }
}

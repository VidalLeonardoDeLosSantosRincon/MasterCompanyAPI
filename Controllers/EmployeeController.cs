using MasterCompanyAPI.Interfaces;
using MasterCompanyAPI.Models;
using MasterCompanyAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MasterCompanyAPI.Controllers
{
    [Route("api/employees")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository employeeRepo;
        public EmployeeController() {
            employeeRepo = new EmployeeRepository();
        }

        [Route("{document}")]
        [HttpGet, ActionName("Get")]
        public async Task<JsonResult> Get(string? document)
        {
            Employee? employee = await employeeRepo.GetEmployeeByDocument(document);
            object data =  (employee != null)? new { employee } : new { };
            return new JsonResult(new { data });
        }

        [Route("salary-range/")]
        [HttpGet, ActionName("GetBySalaryRange")]
        public async Task<JsonResult> GetBySalaryRange([Required]double? from, [Required]double? to)
        {
            List<Employee>? employees = await employeeRepo.GetEmployeesBySalaryRange(from, to);
            var data = new { total = employees.Count, employees };
            return new JsonResult(new { data });
        }

        [HttpGet, ActionName("Get")]
        public async Task<JsonResult> Get()
        {
            List<Employee> employees = await employeeRepo.GetEmployees();
            var data = new { total = employees.Count, employees };
            return new JsonResult(new { data });
        }

        [Route("no-duplicated")]
        [HttpGet, ActionName("GetNoDuplicated")]
        public async Task<JsonResult> GetNoDuplicated()
        {
            List<Employee> employees = await employeeRepo.GetNoDuplicatedEmployees();
            var data = new { total = employees.Count, employees };
            return new JsonResult(new { data });
        }

        [Route("add")]
        [HttpPost, ActionName("Post")]
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

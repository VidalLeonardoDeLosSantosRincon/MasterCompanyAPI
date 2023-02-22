﻿using MasterCompanyAPI.Models;
using MasterCompanyAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

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

        [Route("{id:int}")]
        [HttpGet, ActionName("Get")]
        public async Task<JsonResult> Get(int? id)
        {
            await Task.Delay(3000);
            return new JsonResult(new { id });
        }

        [HttpGet, ActionName("Get")]
        public async Task<JsonResult> Get()
        {
            employeeRepo.GetEmployees();
            await Task.Delay(3000);
            return new JsonResult(new { });
        }

        [Route("add")]
        [HttpPost, ActionName("Post")]
        public async Task<JsonResult> Post(int? id)
        {
            await Task.Delay(3000);
            return new JsonResult(new { id });
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
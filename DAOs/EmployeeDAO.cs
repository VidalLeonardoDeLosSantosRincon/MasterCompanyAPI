using MasterCompanyAPI.Database;
using MasterCompanyAPI.Interfaces;
using MasterCompanyAPI.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MasterCompanyAPI.DAOs
{
    public class EmployeeDAO
    {
        private readonly Context<Employee> db;

        public EmployeeDAO()
        {
            db = new("Employees", ".txt");
        }

        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///     <para>
        ///         Returns a list of employees(All) 
        ///     </para>
        ///     <para>Uses:
        ///         <code>- <see cref="Context{E}.JsonArray"/></code>
        ///         <code>- <see cref="Context{E}.JsonToModelList"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     <see cref="List{E}"/> of type <see cref="Employee"/>
        /// </returns>
        public async Task<List<Employee>> GetAll()
        {
            List<Employee> employees = new();

            var results = db.JsonToModelList(await db.JsonArray());
            if (results != null) employees = results;

            return employees;
        }

        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///     <para>
        ///         Returns a list of employees(All) 
        ///     </para>
        ///     <para>Uses:
        ///         <code>- <see cref="Context{E}.JsonArray"/></code>
        ///         <code>- <see cref="Context{E}.JsonToModelList"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     <see cref="List{E}"/> of type <see cref="Employee"/>
        /// </returns>
        public async Task<bool> Add(Employee? employee)
        {
            if(employee == null) return false;

            List<Employee> employees = new();

            var results = db.JsonToModelList(await db.JsonArray());
            if (results != null) employees = results;
            employees.Add(employee);

            string json = JsonSerializer.Serialize(employees);

            Debug.WriteLine(json);

            return await db.AddContent(json);

        }
    }
}

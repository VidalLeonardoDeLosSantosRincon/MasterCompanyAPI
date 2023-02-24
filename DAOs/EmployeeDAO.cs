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
        ///     <para>Uses:
        ///         <code>- <see cref="Context{E}.AddContent"/></code>
        ///         <code>- <see cref="Context{E}.JsonArray"/></code>
        ///         <code>- <see cref="Context{E}.JsonToModelList"/></code>
        ///         <code>- <see cref="JsonSerializer.Serialize"/></code>
        ///     </para>
        /// </summary>
        /// <param name="employee">
        ///      Represents the <see langword="object"/> that will be append to the list 
        ///      to update the file content.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> 
        ///     if <see langword="param"/> <paramref name="employee"/> is not <see langword="null"/> 
        ///     and the content was append to the file successfully,
        ///     otherwise <see langword="false"/>.
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

        /// <summary>
        ///     <c><see langword="async"/> method </c>,
        ///     deletes an employee by his/her document
        ///     <para>Uses:
        ///         <code>- <see cref="Context{E}.AddContent"/></code>
        ///         <code>- <see cref="Context{E}.JsonArray"/></code>
        ///         <code>- <see cref="Context{E}.JsonToModelList"/></code>
        ///         <code>- <see cref="JsonSerializer.Serialize"/></code>
        ///     </para>
        /// </summary>
        /// <param name="document">
        ///      Represents the <see langword="property"/> Document of the <see cref="Employee"/> that will be removed from the list 
        ///      to update the file content.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> 
        ///     if <see langword="param"/> <paramref name="document"/> is not <see langword="null"/>,
        ///     
        ///     and the content was removed from the file successfully,
        ///     otherwise <see langword="false"/>.
        /// </returns>
        public async Task<bool> Delete(string? document)
        {
            if (document == null) return false;

            List<Employee> employees = new();

            var results = db.JsonToModelList(await db.JsonArray());
            if (results == null) return false;

            employees = results;
            Employee? employee = employees.Where(x => x.Document != null && x.Document.Equals(document)).FirstOrDefault();

            if (employee == null) return false;
           
            bool removed = employees.Remove(employee);
            if (!removed) return false;

            string json = JsonSerializer.Serialize(employees);
            //Debug.WriteLine(json);

            return await db.AddContent(json);
        }
    }
}

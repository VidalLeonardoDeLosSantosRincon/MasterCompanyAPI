using MasterCompanyAPI.DAOs;
using MasterCompanyAPI.Models;
using System.Diagnostics;
using System.Text.Json;

namespace MasterCompanyAPI.Repositories
{
    public class EmployeeRepository
    {
        private readonly EmployeeDAO employeeDao;

        public EmployeeRepository()
        {
            employeeDao = new();
        }

        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///     <para>
        ///         Returns a list of employees(All) 
        ///     </para>
        ///     <para>Uses:
        ///         <code>- <see cref="EmployeeDAO.GetAll"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     <see cref="List{E}"/> of type <see cref="Employee"/>
        /// </returns>
        public async Task<List<Employee>> GetEmployees()
        {
            List<Employee> employees = await employeeDao.GetAll();
            return employees;
        }

        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///     <para>
        ///         Returns a specific employee by his/her <see cref="Employee.Document"/>
        ///     </para>
        ///     <para>Uses:
        ///         <code>- <see cref="EmployeeDAO.GetAll"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     <see langword="object"/> of type <see cref="Employee"/>
        /// </returns>
        public async Task<Employee?> GetEmployeeByDocument(string? document)
        {
            Employee? employee = null;
            document = (document ?? "").Trim();
            if (document == "") return null;

            List<Employee> employees = await employeeDao.GetAll();
            employee = employees.Where(x => x.Document == document).FirstOrDefault();

            return employee;
        }

        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///     <para>
        ///         Returns a list of employees by a salary (<see cref="Employee.Salary"/>) range
        ///     </para>
        ///     <para>Uses:
        ///         <code>- <see cref="EmployeeRepository.GetEmployees"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     <see cref="List{E}"/> of type <see cref="Employee"/>
        /// </returns>
        public async Task<List<Employee>> GetEmployeesBySalaryRange(double? from_salary, double? to_salary)
        {
            List<Employee> employees = await this.GetEmployees();
            employees = employees.Where(x => x.Salary >= from_salary && x.Salary <= to_salary).ToList();
            return employees;
        }

        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///     <para>
        ///         Returns a list of employees(no duplicated) 
        ///     </para>
        ///     <para>Uses:
        ///         <code>- <see cref="EmployeeRepository.GetEmployees"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     <see cref="List{E}"/> of type <see cref="Employee"/>
        /// </returns>
        public async Task<List<Employee>> GetNoDuplicatedEmployees()
        {
            List<Employee> employees = await this.GetEmployees();
            employees = employees.GroupBy(x => x.Document).Select(x => x.First()).ToList();
            return employees;
        }


        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///     <para>
        ///         Returns a list of employees (disabled) 
        ///     </para>
        ///     <para>Uses:
        ///         <code>- <see cref="EmployeeDAO.GetDisabled"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     <see cref="List{E}"/> of type <see cref="Employee"/>
        /// </returns>
        public async Task<List<Employee>> GetDisabledEmployees()
        {
            return await employeeDao.GetDisabled();
        }

        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///     <para>
        ///         Returns a list of employees (deleted) 
        ///     </para>
        ///     <para>Uses:
        ///         <code>- <see cref="EmployeeDAO.GetDeleted"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     <see cref="List{E}"/> of type <see cref="Employee"/>
        /// </returns>
        public async Task<List<Employee>> GetDeletedEmployees()
        {
            return await employeeDao.GetDeleted();
        }

        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///     <para>Uses:
        ///         <code>- <see cref="EmployeeDAO.Add"/></code>
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
        public async Task<bool> AddEmployee(Employee? employee)
        {
            if (employee == null) return false;
            return await employeeDao.Add(employee);
        }

        /// <summary>
        ///     <c><see langword="async"/> method </c>,
        ///     disables an employee by his/her document
        ///     <para>Uses:
        ///         <code>- <see cref="EmployeeDAO.Delete"/></code>
        ///     </para>
        /// </summary>
        /// <param name="document">
        ///      Represents the <see langword="property"/> Document of the <see cref="Employee"/> that will be removed from the list 
        ///      to update the file content.
        /// </param>
        /// <returns>   
        ///     <para>
        ///         <see langword="true"/> if <see langword="param"/> <paramref name="document"/> is not 
        ///         <see langword="null"/> and the content was moved from the file <see langword="Employees.txt"/>
        ///     </para>
        ///     <para>
        ///         to <see langword="DisabledEmployees.txt"/> successfully,
        ///         otherwise the  property EmployeeDisabled will be <see langword="false"/>.
        ///     </para>
        /// </returns>
        public async Task<bool> DisableEmployee(string? document)
        {
            if (document == null) return false;
            return await employeeDao.Disable(document);
        }

        /// <summary>
        ///     <c><see langword="async"/> method </c>,
        ///     deletes an employee by his/her document
        ///     <para>Uses:
        ///         <code>- <see cref="EmployeeDAO.Delete"/></code>
        ///     </para>
        /// </summary>
        /// <param name="document">
        ///      Represents the <see langword="property"/> Document of the <see cref="Employee"/> that will be removed from the list 
        ///      to update the file content.
        /// </param>
        /// <returns>
        ///     <see langword="true"/> 
        ///     if <see langword="param"/> <paramref name="document"/> is not <see langword="null"/> 
        ///     and the content was removed from the file successfully,
        ///     otherwise <see langword="false"/>.
        /// </returns>
        public async Task<bool> DeteleEmployee(string? document)
        {
            if (document == null) return false;
            return await employeeDao.Delete(document);
        }
    }
}

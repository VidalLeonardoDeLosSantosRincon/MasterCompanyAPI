using MasterCompanyAPI.DAOs;
using MasterCompanyAPI.Models;

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
    }
}

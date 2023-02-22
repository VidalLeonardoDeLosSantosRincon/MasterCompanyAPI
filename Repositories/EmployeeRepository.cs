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
    }
}

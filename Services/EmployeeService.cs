using MasterCompanyAPI.Repositories;

namespace MasterCompanyAPI.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository employeeRepo;
      
        public EmployeeService() {
            employeeRepo = new EmployeeRepository();
        }
    }
}

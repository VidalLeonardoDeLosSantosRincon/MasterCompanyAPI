using MasterCompanyAPI.Database;
using MasterCompanyAPI.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Diagnostics;

namespace MasterCompanyAPI.DAOs
{
    public class EmployeeDAO
    {
        private readonly Context db;

        public EmployeeDAO()
        {
            db = new("Employees", ".txt");
        }
    }
}

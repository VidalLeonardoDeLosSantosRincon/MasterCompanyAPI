using MasterCompanyAPI.Models;
using MasterCompanyAPI.Repositories;

namespace MasterCompanyAPI.Services
{
    public class EmployeeService
    {
        private readonly EmployeeRepository employeeRepo;

        public EmployeeService() {
            employeeRepo = new EmployeeRepository();
        }

        /// <summary>
        ///     Calculates the percentage that a 
        ///     <paramref name="quantity"/> represents of a <paramref name="total"/>
        ///     <para>Uses:
        ///         <code>- <see cref="Math.Round(decimal, int)"/></code>
        ///     </para>
        /// </summary>
        /// <param name="cuantity">
        ///     Represents a part of <paramref name="total"/>
        /// </param>
        /// <param name="total">
        ///     Represents a total of parts
        /// </param>
        /// <returns>
        ///     the percentage that a <paramref name="quantity"/> represents of a <paramref name="total"/>
        /// </returns>
        private double CalculatePercetange(int cuantity, int total)
        {
            if (cuantity == 0 || total == 0) return 0;

            double result = ((double)cuantity / (double)total) * 100;
            result = Math.Round(result, 2);
            return result;
        }

        /// <summary>
        ///     Calculates the gender percentages of employees
        ///     <para>Uses:
        ///         <code>- <see cref="EmployeeRepository.GetEmployees()"/></code>
        ///         <code>- <see cref="CalculatePercetange"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An <see langword="object"/> that contains the total gender percentages
        /// </returns>
        public async Task<Object> GetGenderPercentages()
        {
            List<Employee> employees = await employeeRepo.GetEmployees();
            List<Employee> males = employees.Where(x => x.Gender != null && x.Gender.ToUpper().Equals("M")).ToList();
            List<Employee> females = employees.Where(x => x.Gender != null && x.Gender.ToUpper().Equals("F")).ToList();

            int total = employees.Count;
            int total_males = males.Count;
            int total_females = females.Count;
            double males_percent = CalculatePercetange(total_males, total);
            double females_percent = CalculatePercetange(total_females, total);

            return new
            {
                all = new { total, percentage = 100 },
                males = new { total = total_males, percentage = males_percent },
                females = new { total = total_females, percentage = females_percent },
            };
        }

    }
}

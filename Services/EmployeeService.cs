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

    }
}

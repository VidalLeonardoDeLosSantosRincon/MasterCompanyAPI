using MasterCompanyAPI.DTOs;
using MasterCompanyAPI.Models;
using MasterCompanyAPI.Repositories;
using static System.Net.Mime.MediaTypeNames;

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

        /// <summary>
        ///     Calculates the employees salary increases 
        ///     <para>Uses:
        ///         <code>- <see cref="EmployeeRepository.GetEmployees()"/></code>
        ///         <code>- <see cref="Func{}"/></code>
        ///         <code>- <see cref="Tuple.Create"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     An <see langword="object"/> That contains the employees salary increases.
        /// </returns>
        public async Task<object> GetSalaryRaise()
        {
            List<Employee> employees = await employeeRepo.GetEmployees();
            List<EmployeeDTO> employeesSalariesRaise = new();
            int index = 1;
            int status = 1;

            Func<double, Tuple<double, double, double>> CalculateRaise = (double salary) => { 
                double percentage = 0,
                       increment = 0,
                       raise = 0,
                       limit_amount = 100000;

                if(salary == 0 )return Tuple.Create(percentage, increment, raise);

                percentage = (salary >= limit_amount) ? 30 : 25;
                increment = (percentage / 100) * salary;
                raise = (salary + increment);

                return Tuple.Create(percentage, increment, raise);
            };
            
            foreach (var employee in employees)
            {
                var calculatedRaise = CalculateRaise(employee.Salary);
                employeesSalariesRaise
                .Add(new (){
                    Id = index,
                    Name = employee.Name,
                    LastName = employee.LastName,
                    Document = employee.Document,
                    Salary = calculatedRaise.Item3,
                    Gender = employee.Gender,
                    Position = employee.Position,
                    StartDate = employee.StartDate,
                    Status = status,
                    SalaryRaise = new()
                    {
                        OldSalary = employee.Salary,
                        Percentage = calculatedRaise.Item1,
                        Amount = calculatedRaise.Item2,
                        NewSalary = calculatedRaise.Item3,
                    }
                });
                index++;
            }

            return new { total = employees.Count, employees = employeesSalariesRaise };
        }

        /// <summary>
        ///     Disables an employee by his/her document
        ///     <para>Uses:
        ///          <code>- <see cref="EmployeeRepository.DisableEmployee"/></code>
        ///     </para>
        /// </summary>
        /// <param name="document">
        ///      Represents the <see langword="property"/> Document of the <see cref="Employee"/> that will be removed from the list 
        ///      to update the file content.
        /// </param>
        /// <returns>
        ///     <para>
        ///         An <see langword="object"/> that contains a property EmployeeDisabled that will be <see langword="true"/> 
        ///         if <see langword="param"/> <paramref name="document"/> is not <see langword="null"/> and
        ///     </para>
        ///     <para>
        ///         the content was moved from the file <see langword="Employees.txt"/> to <see langword="DisabledEmployees.txt"/> successfully,
        ///         otherwise the  property EmployeeDisabled will be <see langword="false"/>.
        ///     </para>
        ///     <para>
        ///         If <paramref name="document"/> <see langword="format"/> is not valid will be return a <see langword="object"/>
        ///         with the <see langword="errors"/> info
        ///     </para>
        /// </returns>
        public async Task<object> Disable(string? document)
        {
            List<string> errors = new();
            if (document == null) errors.Add("Document Can't be null");

            if (document != null)
            {
                document = document.Trim();
                if (!document.All(Char.IsDigit)) errors.Add("Only digits allowed");
                if (document == "") errors.Add("Can't be empty");
                if (document.Length != 11) errors.Add($"Required length: 11, current document length: {document.Length}");
            }

            if (errors.Count > 0)
            {
                return new
                {
                    InvalidDocument = document,
                    Constraints = errors
                };
            };

            return new { EmployeeDisabled = await employeeRepo.DisableEmployee(document) };
        }

        /// <summary>
        ///     Deletes an employee by his/her document
        ///     <para>Uses:
        ///          <code>- <see cref="EmployeeRepository.DeteleEmployee"/></code>
        ///     </para>
        /// </summary>
        /// <param name="document">
        ///      Represents the <see langword="property"/> Document of the <see cref="Employee"/> that will be removed from the list 
        ///      to update the file content.
        /// </param>
        /// <returns>
        ///     An <see langword="object"/> That contains a property EmployeeDeleted that will be <see langword="true"/> 
        ///     if <see langword="param"/> <paramref name="document"/> is not <see langword="null"/> 
        ///     and the content was removed from the file successfully,
        ///     otherwise the  property EmployeeDeleted will be <see langword="false"/>.
        ///     <para>
        ///         If <paramref name="document"/> <see langword="format"/> is not valid will be return a <see langword="object"/>
        ///         with the <see langword="errors"/> info
        ///     </para>
        /// </returns>
        public async Task<object> Delete(string? document)
        {
            List<string> errors = new();
            if (document == null) errors.Add("Document Can't be null");

            if (document != null)
            {
                document = document.Trim();
                if (!document.All(Char.IsDigit)) errors.Add("Only digits allowed");
                if (document == "") errors.Add("Can't be empty");
                if (document.Length != 11) errors.Add($"Required length : 11, current document length: {document.Length}");
            }
   
            if(errors.Count > 0) {
                return new { 
                                InvalidDocument = document, 
                                Constraints = errors                            
                            }; 
            };

            return new { EmployeeDeleted = await employeeRepo.DeteleEmployee(document)};
        }
    }
}

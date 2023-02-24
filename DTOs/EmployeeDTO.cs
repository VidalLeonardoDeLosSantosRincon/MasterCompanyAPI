using MasterCompanyAPI.Interfaces;
using MasterCompanyAPI.Models;

namespace MasterCompanyAPI.DTOs
{
    public class EmployeeDTO : IEmployee
    {
        /// <summary>
        /// <c><see langword="int"/> Id</c>, 
        /// represents the <see cref="Employee"/> Id (Identifier)
        /// </summary>
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Document { get; set; }
        public double Salary { get; set; }
        public string? Gender { get; set; }
        public string? Position { get; set; }
        public string? StartDate { get; set; }

        /// <summary>
        ///     <c><see langword="int"/> Status</c>, 
        ///     represents the <see cref="Employee"/> Status <c>(Range [0, 2])</c>
        /// <para>
        ///     If <c>Status</c> equals to <c><see langword="0"/></c>
        ///     the employee is disabled
        /// </para>
        /// <para>
        ///     If <c>Status</c> equals to <c><see langword="1"/></c>
        ///     the employee is enabled
        /// </para>
        ///  <para>
        ///     If <c>Status</c> equals to <c><see langword="2"/></c>
        ///     the employee is deleted
        /// </para>
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// <c><see cref="SalaryIncrease"/> SalaryRaise</c>, 
        /// represents the <see cref="Employee"/> salary increase
        /// </summary>
        public SalaryIncrease? SalaryRaise { get; set; }
    }
}

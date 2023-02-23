using MasterCompanyAPI.Interfaces;

namespace MasterCompanyAPI.Models
{
    /// <summary>
    /// <para>
    /// <see langword="public"/>
    /// <see langword="class"/> 
    /// <see cref="Employee"/>, 
    /// represents the employee data model.
    /// </para>
    /// implements
    /// <see langword="interface"/>
    /// <see cref="IEmployee"/>
    /// </summary>
    public class Employee : IEmployee
    {  
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public string? Document { get; set; }
        public double Salary { get; set; }
        public string? Gender { get; set; }
        public string? Position { get; set; }
        public string? StartDate { get; set; }
    }
}

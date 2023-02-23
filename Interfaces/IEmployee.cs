using MasterCompanyAPI.Models; 

namespace MasterCompanyAPI.Interfaces
{
    /// <summary>
    /// <see langword="public"/>
    /// <see langword="interface"/> 
    /// <see cref="IEmployee"/>,
    /// contains the main attributes for 
    /// <see langword="class"/>
    /// <see cref="Employee"/>
    /// </summary>
    public interface IEmployee
    {
        /// <summary>
        /// <c><see langword="string"/>? Name</c>, 
        /// represents the <see cref="Employee"/> Name
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// <c><see langword="string"/>? LastName</c>, 
        /// represents the <see cref="Employee"/> LastName
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// <c><see langword="string"/>? Document</c>, 
        /// represents the <see cref="Employee"/> Document
        /// </summary>
        public string? Document { get; set; }

        /// <summary>
        /// <c><see langword="double"/> Salary</c>, 
        /// represents the <see cref="Employee"/> Salary
        /// </summary>
        public double Salary { get; set; }

        /// <summary>
        /// <c><see langword="string"/>? Gender</c>, 
        /// represents the <see cref="Employee"/> Gender (M, F)
        /// <para>
        ///  <c>M <see langword="="/> Male</c>
        /// </para>
        /// <para>
        ///  <c>F <see langword="="/> Female</c>
        /// </para>
        /// </summary>
        public string? Gender { get; set; }

        /// <summary>
        /// <c><see langword="string"/>? Position</c>, 
        /// represents the <see cref="Employee"/> Position
        /// </summary>
        public string? Position { get; set; }

        /// <summary>
        /// <c><see cref="string"/>? StartDate</c>, 
        /// represents the <see cref="Employee"/> StartDate
        /// </summary>
        public string? StartDate { get; set; }
    }
}

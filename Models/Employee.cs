using MasterCompanyAPI.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

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
        [Required]
        [MinLength(1)]
        [MaxLength(150)]
        public string? Name { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(200)]
        public string? LastName { get; set; }

        [Required]
        [MinLength(11)]
        [MaxLength(11)]
        public string? Document { get; set; }

        [Required]
        [DefaultValue(0)]
        public double Salary { get; set; }

        [Required]
        [MinLength(1)]
        [MaxLength(1)]
        public string? Gender { get; set; }

        [Required]
        [MinLength(1)]
        public string? Position { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10)]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public string? StartDate { get; set; }
    }
}

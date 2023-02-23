namespace MasterCompanyAPI.Models
{
    /// <summary>
    ///     <see langword="public"/>
    ///     <see langword="class"/> 
    ///     <see cref="SalaryRaise"/>, 
    ///     represents the salary increase data model.
    /// </summary>
    public class SalaryRaise
    {
        /// <summary>
        ///     <c><see langword="double"/> OldSalary</c>, 
        ///     represents the employee old salary
        /// </summary>
        public double OldSalary { get; set; }

        /// <summary>
        ///     <c><see langword="double"/> Percentage</c>, 
        ///     represents the employee salary increase percentage
        /// </summary>
        public double Percentage { get; set; }

        /// <summary>
        ///     <c><see langword="double"/> Amount</c>, 
        ///     represents the employee salary increase amount
        ///     <para>
        ///         <c> 
        ///             <see cref="Amount"/> = 
        ///             <see cref="Percentage"/> <see langword="%"/> of <see cref="OldSalary"/> 
        ///         </c>
        ///     </para>
        /// </summary>
        public double Amount { get; set; }

        /// <summary>
        ///     <c><see langword="double"/> OldSalary</c>, 
        ///     represents the employee new salary (Increased Salary)
        ///     <para>
        ///        <c> 
        ///             <see cref="NewSalary"/> = 
        ///             <see cref="OldSalary"/> + 
        ///             <see cref="Amount"/>
        ///         </c>
        ///     </para>
        /// </summary>
        public double NewSalary { get; set; }   
    }
}

using MasterCompanyAPI.Models;

namespace MasterCompanyAPI.Database
{
    public class Context<E>
        where E : Employee
    {  
        private List<E> Results { get; set; }

        public Context(string? filename, string? extension)
        {
            this.Results = new();
        }

    }
}

using MasterCompanyAPI.Models;
using MasterCompanyAPI.Utils;

namespace MasterCompanyAPI.Database
{
    public class Context<E>
        where E : Employee
    {
        private readonly DataFile dataFile;
        private List<E> Results { get; set; }

        public Context(string? filename, string? extension)
        {
            this.Results = new();
            this.dataFile = new(filename, extension);
        }

    }
}
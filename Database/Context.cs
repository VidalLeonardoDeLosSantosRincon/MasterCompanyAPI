namespace MasterCompanyAPI.Database
{
    public class Context
    {  
        private List<object> Results { get; set; }

        public Context(string? filename, string? extension)
        {
            this.Results = new();
        }

    }
}

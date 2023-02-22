namespace MasterCompanyAPI.Database
{
    public class Context
    {
        private static readonly string BasePath = @"Database";
        private static readonly List<string> ValidExtension = new() { ".txt", ".json" };
        private static string? FilePath = null;

        private List<object> Results { get; set; }

        public Context(string? filename, string? extension)
        {
            this.Results = new();
        }
    }
}

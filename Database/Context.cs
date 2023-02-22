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
            this.SetPath(filename, extension);
        }

        /// <summary>
        /// Returns the current file path.
        /// </summary>
        /// <returns>
        /// <see langword="string"/>? <see cref="Context.FilePath"/>.
        /// </returns>
        public string? GetPath()
        {
            return Context.FilePath;
        }

        /// <summary>
        /// Set the current file path.
        /// </summary>
        /// <param name="file_name">Specify the file name (without extension)</param>
        /// <param name="file_extension">Specify the file extension
        ///    (<see langword=".txt"/>, <see langword=".json"/>)
        /// </param>
        /// <returns>
        /// <see langword="true"/> if everything is okey, otherwise
        /// <see cref="ApplicationException"/> is something fail.
        /// </returns>
        public object SetPath(string? file_name, string? file_extension)
        {

            file_name = (file_name ?? "").Trim();
            file_extension = (file_extension ?? "").Trim();

            if (file_name == "") return new ApplicationException("Nombre de archivo no valido");
            if (file_extension == "" || !Context.ValidExtension.Contains(file_extension)) return new ApplicationException("Extensión de archivo no valida");

            Context.FilePath = string.Format(@"{0}\{1}{2}", Context.BasePath, file_name, file_extension);
            return true;
        }
    }
}

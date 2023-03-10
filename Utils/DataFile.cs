namespace MasterCompanyAPI.Utils
{
    public class DataFile
    {
        private static readonly string BasePath = @"Database";
        private static readonly List<string> ValidExtension = new() { ".txt", ".json" };
        private static string? FilePath = null;

        private List<object> Results { get; set; }

        public DataFile(string? filename, string? extension)
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
            return DataFile.FilePath;
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
            if (file_extension == "" || !DataFile.ValidExtension.Contains(file_extension)) return new ApplicationException("Extensión de archivo no valida");

            DataFile.FilePath = string.Format(@"{0}\{1}{2}", DataFile.BasePath, file_name, file_extension);
            return true;
        }

        /// <summary>
        /// Checks if <paramref name="filepath"></paramref> exists and it's a valid path.
        /// </summary>
        /// <param name="filepath">Specify the path of the file</param>
        /// <returns>
        /// <see langword="true"/> if the <paramref name="filepath"></paramref> exists and it's a valid path, 
        /// otherwise <see langword="false"/>.
        /// </returns>
        public bool ValidateFile(string? filepath)
        {
            return (filepath != null) && File.Exists(filepath);
        }

        /// <summary>
        /// Replaces specified symbols from 
        /// <paramref name="text"></paramref>
        /// </summary>
        /// <param name="text"></param>
        /// <returns>
        /// <para>
        /// If <paramref name="text" ></paramref>
        /// contains any of the specified symbols, 
        /// return <paramref name="text" ></paramref> with the symbols replaced,
        /// </para>
        /// otherwhise return <paramref name="text" ></paramref> without changes
        /// </returns>
        private static string ReplaceSymbols(string text)
        {
            Dictionary<string, string> _symbols = new() {
                {"\n", ""},
                {"\t", ""},
                {"[", ""},
                {"]", ""}
            };

            foreach (string _symbol in _symbols.Keys)
            {
                text = text.Replace(_symbol, _symbols[_symbol]);
            }

            return text;
        }

        /// <summary>
        /// <c><see langword="async"/> method </c>
        /// </summary>
        /// <returns>
        /// the text (<see langword="string"/>) read from the file 
        /// (<see langword="txt"/>, <see langword="json"/>)
        /// </returns>
        public async Task<string> ReadFile()
        {
            string path = this.GetPath() ?? "";
            string result = "[]";

            if (path != "" && ValidateFile(path))
            {
                string text = await File.ReadAllTextAsync(path);
                text = DataFile.ReplaceSymbols(text);

                string[] lines = text.Split('{');
                result = "";
                int index = 1;

                foreach (string line in lines)
                {
                    if (line.Trim() == "") continue;
                    result += "{";
                    result += string.Format("\n\"Id\": {0}, {1}", index, line);
                    index++;
                }
                return string.Format("[{0}]", result);
            }
            return result;
        }

        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///     <para>
        ///         Append <see langword="param"/> <see langword="string"/>? <paramref name="content"/>
        ///         to the file (<see langword=".txt"/>, <see langword=".json"/>)
        ///     </para>
        ///     <para>Uses:
        ///        <code>- <see cref="File.WriteAllTextAsync"/></code>
        ///     </para>
        /// </summary>
        /// <param name="content">
        ///     Represents the content that will be append to the file
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if content was append to the file successfully,
        ///     otherwise <see langword="false"/>.
        /// </returns>
        public async Task<bool> WriteFile(string? content)
        {
            string path = this.GetPath() ?? "";
            bool result = false;

            content = (content ?? "").Trim();
            if (content.Equals("")) return false;

            if (path != "" && ValidateFile(path))
            {
                try
                {
                    await File.WriteAllTextAsync(path, content);
                    result = true;

                } catch(Exception e) {
                    throw new ApplicationException(e.Message);
                }
            }
            return result;
        }
    }
}

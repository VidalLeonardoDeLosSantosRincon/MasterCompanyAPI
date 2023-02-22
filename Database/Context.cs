using MasterCompanyAPI.Models;
using MasterCompanyAPI.Utils;
using Newtonsoft.Json.Linq;

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

        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///    <para>Uses:
        ///         <code>- <see cref="DataFile.ReadFile"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     the text (<see langword="string"/>) read from the file 
        ///     (<see langword="txt"/>, <see langword="json"/>)
        /// </returns>
        public async Task<string> GetContent()
        {
            return await dataFile.ReadFile();
        }

        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///     <para>Uses:
        ///         <code>- <see cref="GetContent"/></code>
        ///         <code>- <see cref="JObject.Parse"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     A <see langword="JSON"/> 
        ///     <see langword="object"/> (<see cref="JObject"/>)
        /// </returns>
        public async Task<JObject> JsonObject()
        {
            return JObject.Parse(await this.GetContent());
        }
    }
}
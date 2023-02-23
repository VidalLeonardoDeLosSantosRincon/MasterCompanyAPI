using MasterCompanyAPI.Models;
using MasterCompanyAPI.Utils;
using Newtonsoft.Json;
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
        ///         <code>- <see cref="DataFile.WriteFile"/></code>
        ///     </para>
        /// </summary>
        ///  <param name="content">
        ///     Represents the content that will be append to the file
        /// </param>
        /// <returns>
        ///     <see langword="true"/> if content was append to the file successfully,
        ///     otherwise <see langword="false"/>.
        /// </returns>
        public async Task<bool> AddContent(string? content)
        {
            return await dataFile.WriteFile(content);
        }

        /// <summary>
        ///    <c><see langword="async"/> method </c>
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

        /// <summary>
        ///     <c><see langword="async"/> method </c>
        ///     <para>Uses:
        ///         <code>- <see cref="GetContent"/></code>
        ///         <code>- <see cref="JArray.Parse"/></code>
        ///     </para>
        /// </summary>
        /// <returns>
        ///     A <see langword="JSON"/> 
        ///     <see langword="array"/> (<see cref="JArray"/>)
        /// </returns>
        public async Task<JArray> JsonArray()
        {
            return JArray.Parse(await this.GetContent());
        }

        /// <summary>
        ///     Convert <see langword="param"/> 
        ///     <see cref="JObject"/> <paramref name="json"></paramref>
        ///     into a <see langword="object"/> of type <see cref="E"/>.
        ///     <para>Uses: 
        ///         <code>- <see cref="JsonConvert.DeserializeObject"/></code>
        ///     </para>
        /// </summary>
        /// <param name="json">
        ///     A <see langword="JSON"/> 
        ///     <see langword="object"/> (<see cref="JObject"/>).
        /// </param>
        /// <returns>
        ///     <see langword="null"/> if <see langword="param"/> 
        ///     <paramref name="json"></paramref> is <see langword="null"/>,
        ///     otherwise a <see langword="object"/> of type <see cref="E"/> .
        /// </returns>
        public E? JsonToModel(JObject json)
        {
            if (json == null) return null;
            return JsonConvert.DeserializeObject<E>(json.ToString());
        }

        /// <summary>
        ///     Convert <see langword="param"/> 
        ///     <see cref="JArray"/> <paramref name="json"></paramref>
        ///     into a <see cref="List{E}"/> of type <see cref="E"/>.
        ///     <para>Uses:
        ///         <code>- <see cref="JsonConvert.DeserializeObject"/></code>    
        ///     </para>
        /// </summary>
        /// <param name="json">
        ///     A <see langword="JSON"/> 
        ///     <see langword="array"/> (<see cref="JArray"/>).
        /// </param>
        /// <returns>
        ///     <see langword="null"/> if <see langword="param"/> 
        ///     <paramref name="json"></paramref> is <see langword="null"/>,
        ///     otherwise a <see cref="List{E}"/> of type <see cref="E"/>.
        /// </returns>
        public List<E>? JsonToModelList(JArray json)
        {
            if (json == null) return new();
            return JsonConvert.DeserializeObject<List<E>>(json.ToString());
        }
    }
}
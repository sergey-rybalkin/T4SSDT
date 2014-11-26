using System.Collections.Generic;
using System.Web.Http.Description;

namespace T4Generators.WebApi
{
    /// <summary>
    /// Contains API HttpMethod metadata required to generate a client call.
    /// </summary>
    public struct ApiActionMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ApiActionMetadata" /> struct.
        /// </summary>
        /// <param name="name">User friendly name of the action.</param>
        /// <param name="urlTemplate">
        /// Template of the URL used to access this action. It may contain placeholders for URL parameters
        /// that need to be replaced with actual values.
        /// </param>
        /// <param name="method">Http method used to access this action.</param>
        /// <param name="documentation">Action documentation from the source code.</param>
        /// <param name="parameters">Collection of parameters that this method expects.</param>
        public ApiActionMetadata(string name,
                                 string urlTemplate,
                                 string method,
                                 string documentation,
                                 IEnumerable<ApiParameterDescription> parameters)
            : this()
        {
            Name = name;
            UrlTemplate = urlTemplate;
            HttpMethod = method;
            Documentation = documentation ?? "No documentation available";
            Parameters = parameters;
        }

        public string Name { get; private set; }

        public string UrlTemplate { get; private set; }

        public string HttpMethod { get; private set; }

        public string Documentation { get; private set; }

        public IEnumerable<ApiParameterDescription> Parameters { get; private set; }
    }
}
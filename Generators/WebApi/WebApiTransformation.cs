using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Reflection;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.VisualStudio.TextTemplating;

namespace T4Generators.WebApi
{
    /// <summary>
    /// Extends standard text transformation class and adds helper methods for working with
    /// <see cref="ApiExplorer" /> interface. Can be used to generated statically typed JavaScript wrapper for
    /// web api.
    /// </summary>
    public abstract class WebApiTransformation : TextTransformation
    {
        public IEnumerable<ApiActionMetadata> Actions { get; private set; }

        /// <summary>
        /// Initializes host with routing information from the specified web assembly.
        /// </summary>
        /// <param name="webAssemblyPath">
        /// Absolute path to the web assembly to import routes information from.
        /// </param>
        /// <param name="configurationClassName">
        /// Full name of the class that is responsible for web api routing configuration.
        /// </param>
        /// <param name="configurationMethodName">
        /// Name of the <paramref name="configurationClassName" /> method that accepts and instance of http
        /// configuration class and configures web api routes.
        /// </param>
        public void Initialize(string webAssemblyPath,
                               string configurationClassName = "Web.Global",
                               string configurationMethodName = "ConfigureApiRoutes")
        {
            HttpConfiguration config = GetHttpConfiguration(webAssemblyPath,
                                                            configurationClassName,
                                                            configurationMethodName);
            if (null == config)
                return;

            string xmlDocsPath = webAssemblyPath.Substring(0, webAssemblyPath.Length - 4) + ".xml";
            config.Services.Replace(typeof(IDocumentationProvider),
                                    new XmlDocumentationProvider(xmlDocsPath));
            config.EnsureInitialized();
            IApiExplorer apiExplorer = config.Services.GetApiExplorer();

            InitializeActionsList(apiExplorer);
        }

        private HttpConfiguration GetHttpConfiguration(string webAssemblyPath,
                                                       string configurationClassName,
                                                       string configurationMethodName)
        {
            Type configurationContainer;
            try
            {
                Assembly assembly = Assembly.LoadFrom(webAssemblyPath);
                configurationContainer = assembly.GetType(configurationClassName);
            }
            catch (FileNotFoundException)
            {
                string msg = string.Format("Web API container assembly {0} does not exist. " +
                                           "Please build the project and make sure that path is correct.",
                                           webAssemblyPath);
                Error(msg);

                return null;
            }

            if (null == configurationContainer)
            {
                string msg = string.Format("Class {0} was not found in {1}.",
                                           configurationClassName,
                                           webAssemblyPath);
                Error(msg);

                return null;
            }

            MethodInfo targetMethod = configurationContainer.GetMethod(configurationMethodName);

            if (null == targetMethod)
            {
                string msg = string.Format("HttpMethod {0} was not found in {1}.",
                                           configurationMethodName,
                                           configurationClassName);
                Error(msg);

                return null;
            }

            HttpConfiguration config = new HttpConfiguration();
            targetMethod.Invoke(null, new object[] { config });
            return config;
        }

        private void InitializeActionsList(IApiExplorer apiExplorer)
        {
            Collection<ApiDescription> descriptions = apiExplorer.ApiDescriptions;
            var actions = new ApiActionMetadata[descriptions.Count];

            for (int index = 0; index < descriptions.Count; index++)
            {
                ApiDescription description = descriptions[index];
                string controllerName = description.ActionDescriptor.ControllerDescriptor.ControllerName;
                string actionName = description.ActionDescriptor.ActionName;

                string name = char.ToLower(controllerName[0]) + controllerName.Substring(1) + actionName;
                ApiActionMetadata metadata = new ApiActionMetadata(name,
                                                                   description.RelativePath,
                                                                   description.HttpMethod.Method,
                                                                   description.Documentation,
                                                                   description.ParameterDescriptions);
                actions[index] = metadata;
            }

            Actions = actions;
        }
    }
}
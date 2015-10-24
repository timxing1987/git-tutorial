#region

using System.Collections.Generic;
using System.Reflection;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Core.IoC
{
    /// <summary>
    /// </summary>
    internal class DefaultAssemblyResolver : IAssemblyResolver
    {
        public DefaultAssemblyResolver(IEnumerable<Assembly> assemblies)
        {
            Guard.ArgumentNotNull(assemblies, "assemblies");
            Assemblies = assemblies;
        }

        /// <summary>
        /// </summary>
        public IEnumerable<Assembly> Assemblies { get; }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Assembly> GetAssemblies()
        {
            return Assemblies;
        }
    }
}
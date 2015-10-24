#region

using System;
using System.Collections.Generic;
using System.Reflection;

#endregion

namespace Cedar.Core.IoC
{
    /// <summary>
    /// </summary>
    internal static class Assemblies
    {
        private static readonly Lazy<IEnumerable<Assembly>> assembliesAccessor =
            new Lazy<IEnumerable<Assembly>>(GetAssembliesCore);

        /// <summary>
        ///     获取核心集合
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<Assembly> GetAssembliesCore()
        {
            var list = new List<Assembly>();
            AssemblyName[] array =
            {
                new AssemblyName("Cedar.Core"),
                new AssemblyName("Cedar.Core.EntLib")
            };
            var array2 = array;
            for (var i = 0; i < array2.Length; i++)
            {
                var assemblyRef = array2[i];
                try
                {
                    var item = Assembly.Load(assemblyRef);
                    list.Add(item);
                }
                catch
                {
                }
            }
            return list;
        }

        public static IEnumerable<Assembly> GetAssemblies()
        {
            return assembliesAccessor.Value;
        }
    }
}
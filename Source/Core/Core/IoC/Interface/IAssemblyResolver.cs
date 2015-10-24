#region

using System.Collections.Generic;
using System.Reflection;

#endregion

namespace Cedar.Core.IoC
{
    /// <summary>
    /// </summary>
    internal interface IAssemblyResolver
    {
        IEnumerable<Assembly> GetAssemblies();
    }
}
using System.Data.Common;

namespace Cedar.Core.Data
{
    /// <summary>
    ///     All parameter cache classes must implement this interface.
    /// </summary>
    public interface IParameterCache
    {
        /// <summary>
        ///     Sets the parameters.
        /// </summary>
        /// <param name="command">The command.</param>
        /// <param name="database">The <see cref="T:Cedar.Core.Data.Database" />.</param>
        void SetParameters(DbCommand command, Database database);
    }
}
#region

using System;
using System.Runtime.Serialization;

#endregion

namespace Cedar.Core.IoC
{
    /// <summary>
    ///     Exception which is thrown when type resolution fails.
    /// </summary>
    [Serializable]
    public class ResolutionException : Exception
    {
        /// <summary>
        /// </summary>
        public ResolutionException()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        public ResolutionException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public ResolutionException(string message, Exception inner)
            : base(message, inner)
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected ResolutionException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
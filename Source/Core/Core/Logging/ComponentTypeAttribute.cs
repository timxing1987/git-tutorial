using System;

namespace Cedar.Core.Logging
{
    /// <summary>
	///
	/// </summary>
	public class ComponentTypeAttribute : Attribute
    {
        /// <summary>
        /// The component type.
        /// </summary>
        public string ComponentType { get; }

        /// <summary>
        /// The contructor of ComponentTypeAttribute
        /// </summary>
        /// <param name="componentType"></param>
        public ComponentTypeAttribute(string componentType)
        {
            this.ComponentType = componentType;
        }
    }
}

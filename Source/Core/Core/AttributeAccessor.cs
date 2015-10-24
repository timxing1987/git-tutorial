#region

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Practices.Unity.Utility;

#endregion

namespace Cedar.Core
{
    /// <summary>
    ///     This static class is used to get attributes from assemblies, types and type members.
    /// </summary>
    public static class AttributeAccessor
    {
        private static readonly Dictionary<object, Attribute[]> attributeCache = new Dictionary<object, Attribute[]>();
        private static readonly object syncHelper = new object();

        /// <summary>
        ///     Gets the attributes applied to the given <see cref="T:System.Reflection.Assembly" />.
        /// </summary>
        /// <param name="assembly">The assembly.</param>
        /// <param name="inherit">if set to <c>true</c> [inherit].</param>
        /// <returns>
        ///     The attributes applied to the given <see cref="T:System.Reflection.Assembly" />
        /// </returns>
        public static IEnumerable<Attribute> GetAttributes(Assembly assembly, bool inherit)
        {
            Guard.ArgumentNotNull(assembly, "assembly");
            return GetAttributes(assembly,
                (Assembly assembly1, bool inherit1) => assembly1.GetCustomAttributes(inherit1), inherit);
        }

        /// <summary>
        ///     Gets the attributes applied to the given <see cref="T:System.Type" />,
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="inherit">if set to <c>true</c> [inherit].</param>
        /// <returns>
        ///     The attributes applied to the given <see cref="T:System.Reflection.MemberInfo" />
        /// </returns>
        public static IEnumerable<Attribute> GetAttributes(Type type, bool inherit)
        {
            Guard.ArgumentNotNull(type, "type");
            return GetAttributes(type, (Type type1, bool inherit1) => type1.GetCustomAttributes(inherit1), inherit);
        }

        /// <summary>
        ///     Gets the attributes applied to the given <see cref="T:System.Reflection.MemberInfo" />.
        /// </summary>
        /// <param name="member">The member.</param>
        /// <param name="inherit">if set to <c>true</c> [inherit].</param>
        /// <returns>
        ///     The attributes applied to the given <see cref="T:System.Type" />
        /// </returns>
        public static IEnumerable<Attribute> GetAttributes(MemberInfo member, bool inherit)
        {
            Guard.ArgumentNotNull(member, "member");
            return GetAttributes(member, (MemberInfo member1, bool inherit1) => member1.GetCustomAttributes(inherit1),
                inherit);
        }

        /// <summary>
        ///     Gets the attributes applied to the given <see cref="T:System.Reflection.Assembly" />.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="assembly">The assembly.</param>
        /// <param name="inherit">if set to <c>true</c> [inherit].</param>
        /// <returns>
        ///     The attributes applied to the given <see cref="T:System.Reflection.Assembly" />
        /// </returns>
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(Assembly assembly, bool inherit)
            where TAttribute : Attribute
        {
            Guard.ArgumentNotNull(assembly, "assembly");
            return GetAttributes(assembly, inherit).OfType<TAttribute>().ToArray();
        }

        /// <summary>
        ///     Gets the attributes applied to the given <see cref="T:System.Type" />,
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="type">The type.</param>
        /// <param name="inherit">if set to <c>true</c> [inherit].</param>
        /// <returns>
        ///     The attributes applied to the given <see cref="T:System.Reflection.MemberInfo" />
        /// </returns>
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(Type type, bool inherit)
            where TAttribute : Attribute
        {
            Guard.ArgumentNotNull(type, "type");
            return GetAttributes(type, inherit).OfType<TAttribute>().ToArray();
        }

        /// <summary>
        ///     Gets the attributes applied to the given <see cref="T:System.Reflection.MemberInfo" />.
        /// </summary>
        /// <typeparam name="TAttribute">The type of the attribute.</typeparam>
        /// <param name="member">The member.</param>
        /// <param name="inherit">if set to <c>true</c> [inherit].</param>
        /// <returns>
        ///     The attributes applied to the given <see cref="T:System.Type" />
        /// </returns>
        public static IEnumerable<TAttribute> GetAttributes<TAttribute>(MemberInfo member, bool inherit)
            where TAttribute : Attribute
        {
            Guard.ArgumentNotNull(member, "member");
            return GetAttributes(member, inherit).OfType<TAttribute>().ToArray();
        }

        private static IEnumerable<Attribute> GetAttributes<T>(T target, Func<T, bool, object[]> attributeAccessor,
            bool inherit)
        {
            if (!attributeCache.ContainsKey(target))
            {
                var value = attributeAccessor(target, inherit).OfType<Attribute>().ToArray();
                lock (syncHelper)
                {
                    if (!attributeCache.ContainsKey(target))
                    {
                        attributeCache[target] = value;
                    }
                }
            }
            return attributeCache[target];
        }
    }
}
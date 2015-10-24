#region

using System;
using System.ComponentModel;
using System.Configuration;
using System.Globalization;
using Cedar.Core.Properties;

#endregion

namespace Cedar.Core.Configuration
{
    /// <summary>
    ///     程序集限制命名配置转化
    /// </summary>
    public class AssemblyQualifiedTypeNameConfigurationConverter : ConfigurationConverterBase
    {
        /// <summary>
        ///     Converts the given object to the type of this converter, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="culture">The <see cref="T:System.Globalization.CultureInfo" /> to use as the current culture.</param>
        /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
        /// <returns>
        ///     An <see cref="T:System.Object" /> that represents the converted value.
        /// </returns>
        /// <exception cref="T:System.ArgumentException"></exception>
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            var text = value as string;
            if (string.IsNullOrWhiteSpace(text))
            {
                return null;
            }
            var type = Type.GetType(text);
            if (null == type)
            {
                throw new ArgumentException(Resources.ExceptionCannotResolveTypeName.Format(value));
            }
            return type;
        }

        /// <summary>
        ///     Converts the given value object to the specified type, using the specified context and culture information.
        /// </summary>
        /// <param name="context">An <see cref="T:System.ComponentModel.ITypeDescriptorContext" /> that provides a format context.</param>
        /// <param name="culture">
        ///     A <see cref="T:System.Globalization.CultureInfo" />. If null is passed, the current culture is
        ///     assumed.
        /// </param>
        /// <param name="value">The <see cref="T:System.Object" /> to convert.</param>
        /// <param name="destinationType">The <see cref="T:System.Type" /> to convert the <paramref name="value" /> parameter to.</param>
        /// <returns>
        ///     An <see cref="T:System.Object" /> that represents the converted value.
        /// </returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value,
            Type destinationType)
        {
            var type = value as Type;
            if (null == type)
            {
                return null;
            }
            return type.AssemblyQualifiedName;
        }
    }
}
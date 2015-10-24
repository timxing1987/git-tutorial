using System;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.Unity;

namespace Cedar.Core.EntLib.IoC
{
    internal static class TypeExtensions
    {
        public static bool IsTypeOfUnity(this Type type)
        {
            var @string = Encoding.Default.GetString(type.Assembly.GetName().GetPublicKeyToken());
            var string2 = Encoding.Default.GetString(typeof (IUnityContainer).Assembly.GetName().GetPublicKeyToken());
            return @string == string2;
        }

        public static bool IsTypeOfEntLib(this Type type)
        {
            var @string = Encoding.Default.GetString(type.Assembly.GetName().GetPublicKeyToken());
            var string2 =
                Encoding.Default.GetString(
                    typeof (SerializableConfigurationSection).Assembly.GetName().GetPublicKeyToken());
            return @string == string2;
        }
    }
}
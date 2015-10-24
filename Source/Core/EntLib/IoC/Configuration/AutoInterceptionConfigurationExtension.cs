using Microsoft.Practices.Unity.Configuration;

namespace Cedar.Core.EntLib.IoC.Configuration
{
    public class AutoInterceptionConfigurationExtension : SectionExtension
    {
        /// <summary>
        ///     Adds the extensions.
        /// </summary>
        /// <param name="context">The context.</param>
        public override void AddExtensions(SectionExtensionContext context)
        {
            context.AddElement<AutoInterceptionElement>("autoInterception");
            context.AddAlias<AutoInterception>("AutoInterception");
        }
    }
}
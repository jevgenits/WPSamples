using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;


namespace WP.Common.Resources
{
    /// <summary>
    /// Helper class for localization. 
    /// </summary>
    public static class Localization
    {

        private const string DefaultCultureName = "de-de";
        private const string AssemblyName = "WP.Common";

        public static string GetByLocalizationKey(string resourceKey)
        {

            string resourceValue = GetResourceFromFile(resourceKey);

            if (resourceValue == null)
            {
                resourceValue = string.Format(CultureInfo.InvariantCulture, "{0} resource key not found", resourceKey);
            }

            return resourceValue;
        }

        private static string GetResourceFromFile(string resourceKey)
        {
            try
            {
                //Assembly assembly = Assembly.Load(AssemblyName); 
                //var resourcer = new ResourceManager("Resources.Strings", assembly);
                //return resourcer.GetString(resourceKey);//, GetCultureInfo());

                var rm = new ResourceManager("WP.Common.Resources.Strings", Assembly.GetExecutingAssembly());
                return rm.GetString(resourceKey, Thread.CurrentThread.CurrentUICulture);
            }
            catch
            {
                return null;
            }
        }

        public static string Format(DateTime valueToFormat)
        {
            return valueToFormat.ToString("dd.mm.yyyy", CultureInfo.InvariantCulture);
        }

        private static string GetCultureName()
        {
            return DefaultCultureName;
        }

        private static CultureInfo GetCultureInfo()
        {
            return new CultureInfo(GetCultureName());
        }
    }
}

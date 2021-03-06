using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Sample
{
    public static class WebsiteEnvironment
    {
        public static bool IsAvailable => !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME"));

        public static bool Is64BitPlatform => Environment.Is64BitProcess;

        public static string InstanceId => Environment.GetEnvironmentVariable("WEBSITE_INSTANCE_ID");

        public static string ShortInstanceId => InstanceId?.Substring(0, 6);

        public static string SiteName => Environment.GetEnvironmentVariable("WEBSITE_SITE_NAME");

        public static string HostName => Environment.GetEnvironmentVariable("WEBSITE_HOSTNAME");

        public static string IisSiteName => Environment.GetEnvironmentVariable("WEBSITE_IIS_SITE_NAME");

        public static string SlotName => Environment.GetEnvironmentVariable("WEBSITE_SLOT_NAME");

        public static bool HttpLoggingEnabled => Environment.GetEnvironmentVariable("WEBSITE_HTTPLOGGING_ENABLED") == "1";

        public static bool ScmAlwaysOnEnabled => Environment.GetEnvironmentVariable("WEBSITE_SCM_ALWAYS_ON_ENABLED") == "1";

        public static bool ScmSeparateStatus => Environment.GetEnvironmentVariable("WEBSITE_SCM_SEPARATE_STATUS") == "1";

        public static WebsiteComputeMode? ComputeMode => EnumHelper.Convert<WebsiteComputeMode>(Environment.GetEnvironmentVariable("WEBSITE_COMPUTE_MODE"));

        public static WebsiteSiteMode? SiteMode => EnumHelper.Convert<WebsiteSiteMode>(Environment.GetEnvironmentVariable("WEBSITE_SITE_MODE"));

        public static WebsiteSku? Sku => EnumHelper.Convert<WebsiteSku>(Environment.GetEnvironmentVariable("WEBSITE_SKU"));

        public static IDictionary<string, string> AppSettings => EnvironmentHelper.GetVariables("APPSETTING_");

        public static IDictionary<string, string> MySqlConnections => EnvironmentHelper.GetVariables("MYSQLCONNSTR_");

        public static IDictionary<string, string> SqlServerConnections => EnvironmentHelper.GetVariables("SQLAZURECONNSTR_");

        internal class EnvironmentHelper
        {
            internal static IDictionary<string, string> GetVariables(string prefix)
            {
                return Environment.GetEnvironmentVariables()
                                  .Cast<DictionaryEntry>()
                                  .Where(p => ((string)p.Key).StartsWith(prefix))
                                  .ToDictionary(p => ((string)p.Key).Substring(prefix.Length), p => (string)p.Value);
            }
        }

        internal class EnumHelper
        {
            internal static TEnum? Convert<TEnum>(string name) where TEnum : struct
            {
                TEnum result;

                if (Enum.TryParse(name, true, out result))
                {
                    return result;
                }

                return null;
            }
        }

        public enum WebsiteComputeMode
        {
            Shared,
            Dedicated
        }

        public enum WebsiteSiteMode
        {
            Limited,
            Basic
        }

        public enum WebsiteSku
        {
            Free,
            Shared,
            Basic,
            Standard
        }
    }
}

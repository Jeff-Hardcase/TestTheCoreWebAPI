
namespace TestTheCoreWebAPI.Models
{
    public class AppSettingsModel
    {
        public SettingsModel Settings { get; set; }
    }

    public class SettingsModel
    {
        public GlobalSettings GlobalSettings { get; set; }
        public LocalSettings LocalSettings { get; set; }
    }

    public class GlobalSettings
    {
        public string ServiceName { get; set; }
        public string GetCodes { get; set; }
        public string GetDetailCache { get; set; }
    }

    public class LocalSettings
    {
        public string ServiceBaseURL { get; set; }
        public string ConnectionString { get; set; }
        public string[] CorsAllowedSites { get; set; }
        public LoggingConfig LoggingConfig { get; set; }
    }

    public class LoggingConfig
    {
        public bool DetailedLogging { get; set; }
        public bool TraceLogging { get; set; }
    }


}

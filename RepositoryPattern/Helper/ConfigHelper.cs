namespace RepositoryPattern.Helper
{
    public static class ConfigHelper
    {
        private static IConfiguration _configuration;

        public static void Initialize(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public static T GetValue<T>(string key)
        {
            return _configuration.GetValue<T>(key);
        }

        public static string GetConnectionString(string name)
        {
            return _configuration.GetConnectionString(name);
        }

        public static T GetSection<T>(string sectionName) where T : class, new()
        {
            return _configuration.GetSection(sectionName).Get<T>();
        }
    }
}

namespace ReverseProxyPoC.Constants
{
    /// <summary>
    /// This model represents the configuration stored within the appsettings.json file.
    /// </summary>
    public sealed class Config
    {
        /// <summary>
        /// The base URL of the target API to proxy.
        /// </summary>
        public string ProxiedApiBaseUrl { get; set; }
    }
}
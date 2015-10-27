namespace Cake.Chutzpah
{
    /// <summary>
    /// Options to launch tests in browser
    /// </summary>
    public enum ChutzpahBrowser
    {
        /// <summary>
        /// Do not launch in browser
        /// </summary>
        None,
        /// <summary>
        /// Launch in default browser
        /// </summary>
        DefaultBrowser,
        /// <summary>
        /// Launch in Internet Explorer
        /// </summary>
        // ReSharper disable once InconsistentNaming
        IE,
        /// <summary>
        /// Launch in Firefox 
        /// </summary>
        Firefox,
        /// <summary>
        /// Launch in Chrome
        /// </summary>
        Chrome
    }
}
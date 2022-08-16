namespace COMDES_for_NMEA0183.Utilities
{
    /// <summary>
    /// CommandLineParser用設定
    /// </summary>
    internal class CommandLineOptions
    {
        /// <summary>
        /// カルチャー情報
        /// </summary>
        [CommandLine.Option('c', "cultureinfo", Required = false)]
        public string CultureInfo { get => _cultureInfo; set => _cultureInfo = value; }
        private string _cultureInfo = string.Empty;
    }
}

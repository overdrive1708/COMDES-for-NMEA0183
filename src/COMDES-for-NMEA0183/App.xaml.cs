using COMDES_for_NMEA0183.Utilities;
using COMDES_for_NMEA0183.Views;
using CommandLine;
using Prism.Ioc;
using System;
using System.Globalization;
using System.Threading;
using System.Windows;

namespace COMDES_for_NMEA0183
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        /// <summary>
        /// Startupイベントハンドラの拡張
        /// </summary>
        /// <param name="e">Startupイベントの引数</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            // コマンドラインオプションを解析する
            using (Parser parser = new((configuration) => configuration.HelpWriter = null))
            {
                ParserResult<CommandLineOptions> parsed = parser.ParseArguments<CommandLineOptions>(e.Args);
                _ = parsed.WithParsed(option =>
                {
                    // カルチャー情報のオプションによる動作の切り替え
                    if (option.CultureInfo == string.Empty)
                    {
                        // オプション未指定時はOSのカルチャーに従う
                    }
                    else if (option.CultureInfo is "en-US" or "ja-JP")
                    {
                        // 想定しているカルチャーの場合はカルチャーを切り替える｡
                        Thread.CurrentThread.CurrentUICulture = new CultureInfo(option.CultureInfo);
                    }
                    else
                    {
                        // 想定していないカルチャーの場合は例外をスローする｡
                        throw new ArgumentException(null);
                    }
                });
            }

            base.OnStartup(e);
        }

        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}

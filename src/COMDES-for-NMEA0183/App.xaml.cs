using COMDES_for_NMEA0183.Utilities;
using COMDES_for_NMEA0183.Views;
using CommandLine;
using Prism.Ioc;
using System;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;

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
            // 未処理の例外を処理するイベントハンドラを登録する
            DispatcherUnhandledException += OnDispatcherUnhandledException;
            TaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
            AppDomain.CurrentDomain.UnhandledException += OnUnhandledException;

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

        /// <summary>
        /// DispatcherUnhandledExceptionイベントハンドラ
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e) => HandleException(e.Exception);

        /// <summary>
        /// UnobservedTaskExceptionイベントハンドラ
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        private void OnUnobservedTaskException(object sender, UnobservedTaskExceptionEventArgs e) => HandleException(e.Exception.InnerException);

        /// <summary>
        /// UnhandledExceptionイベントハンドラ
        /// </summary>
        /// <param name="sender">イベントソース</param>
        /// <param name="e">イベントデータ</param>
        private void OnUnhandledException(object sender, UnhandledExceptionEventArgs e) => HandleException((Exception)e.ExceptionObject);

        /// <summary>
        /// 例外発生時の処理
        /// </summary>
        /// <param name="e">例外情報</param>
        private static void HandleException(Exception e)
        {
            _ = MessageBox.Show(messageBoxText: $"{COMDES_for_NMEA0183.Properties.Resources.MessageFatalError}\r\n{e?.ToString()}",
                                caption: COMDES_for_NMEA0183.Properties.Resources.ImportantNotice,
                                button: MessageBoxButton.OK,
                                icon: MessageBoxImage.Error);
            Environment.Exit(1);
        }
    }
}

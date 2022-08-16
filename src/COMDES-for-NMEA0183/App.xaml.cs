using COMDES_for_NMEA0183.Views;
using Prism.Ioc;
using System.Windows;

namespace COMDES_for_NMEA0183
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override Window CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {

        }
    }
}

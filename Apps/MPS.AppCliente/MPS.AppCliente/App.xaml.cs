using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OS = Sysne.Core.OS;
using MPS.AppCliente.Views.OS;
using MPS.Core.Lib.OS;
using MPS.AppCliente.Views.Views;
using Sysne.Core.OS;
using Xamarin.Essentials;

namespace MPS.AppCliente
{
    public partial class App : Application
    {
        public NotificationsWrapper NotificationsWrapper { get; private set; } = new NotificationsWrapper();

        /// <summary>
        /// Versionamiento de lanzamiento de la app 
        /// </summary>
        public static string VersionName { get; set; }
        /// <summary>
        /// Versionamiento del código.
        /// </summary>
        public static string VersionCode { get; set; }

        public App()
        {
            InitializeComponent();
            VersionTracking.Track();
            VersionName = VersionTracking.CurrentVersion;
            VersionCode = VersionTracking.CurrentBuild;
            OS.DependencyService.Register<NavigationService, INavigationService>(OS.DependencyService.ServiceLifetime.Singleton);
            OS.DependencyService.Register<SettingsStorage, ISettingsStorage>();

            MainPage = new NavigationPage(new Login());
            (OS.DependencyService.Get<INavigationService>() as NavigationService).Navigation = Current.MainPage.Navigation;

            //MainPage.Appearing += (s, e) =>
            OS.DependencyService.Get<IOS>().SetStatusBarColor(((Color)Resources["AlterColor"]).ToHex());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}

using MPS.AppSocio.Views.OS;
using MPS.AppSocio.Views.Views;
using MPS.Core.Lib.OS;
using OS = Sysne.Core.OS;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sysne.Core.OS;

namespace MPS.AppSocio
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

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

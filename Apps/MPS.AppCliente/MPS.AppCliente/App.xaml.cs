using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using OS = Sysne.Core.OS;
using MPS.AppCliente.Views.OS;
using MPS.Core.Lib.OS;
using MPS.AppCliente.Views.Views;

namespace MPS.AppCliente
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            OS.DependencyService.Register<NavigationService, INavigationService>();
            OS.DependencyService.Register<SettingsStorage, ISettingsStorage>();

            MainPage = new NavigationPage(new Login());
            (OS.DependencyService.Get<INavigationService>() as NavigationService).Navigation = Current.MainPage.Navigation;
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

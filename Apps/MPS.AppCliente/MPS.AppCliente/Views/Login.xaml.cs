using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sysne.Core.OS;
using MPS.Core.Lib.Helpers;
using Xamarin.Essentials;

namespace MPS.AppCliente.Views.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            if (string.IsNullOrEmpty(Settings.Current.ModeloDispositivo))
                Settings.Current.ModeloDispositivo = DeviceInfo.Model;

            if (string.IsNullOrEmpty(Settings.Current.SO))
                Settings.Current.SO = DeviceInfo.VersionString;

            Settings.Current.TipoDispositivo = Device.RuntimePlatform == Device.iOS ? "0" : Device.RuntimePlatform == Device.Android ? "1" : Device.RuntimePlatform == Device.UWP ? "6" : "1";
        }

        private async void TapGestureRecognizer_OlvideDatos(object sender, EventArgs e) =>
            await Browser.OpenAsync("https://mpsmovil.com/usuarios/forgot-password.html", BrowserLaunchMode.SystemPreferred);
    }
}
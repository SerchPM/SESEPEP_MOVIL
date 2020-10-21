using MPS.Core.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPS.AppSocio.Views.Views
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

            Settings.Current.TipoDispositivo = Device.RuntimePlatform == Device.iOS ? "0" : Device.RuntimePlatform == Device.Android ? "1" : "1";
        }
    }
}
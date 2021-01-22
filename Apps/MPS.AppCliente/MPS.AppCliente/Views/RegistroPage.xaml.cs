using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPS.AppCliente.Views.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistroPage : ContentPage
    {
        public RegistroPage()
        {
            InitializeComponent();
            uno.Source = "unoseleccionado.png";
            dos.Source = "dos.png";
            ViewModel.ObtenerComponentesCommand.Execute();
            ViewModel.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case "Registro":
                        if(ViewModel.Registro)
                        {
                            ViewModel.Subtitulo = "Datos generales";
                            uno.Source = "unoseleccionado.png";
                            dos.Source = "dos.png";
                        }
                        break;
                    case "RegistroTarjeta":
                        if (ViewModel.RegistroTarjeta)
                        {
                            ViewModel.Subtitulo = "Datos bancarios";
                            uno.Source = "uno.png";
                            dos.Source = "dosseleccionado.png";
                        }
                        break;
                    default:
                        break;
                }
            };
            ViewModel.RegistrarClienteOneSignalCommand.Execute();
        }

        protected override void OnAppearing()
        {
            ViewModel.IOS = Device.RuntimePlatform.Equals(Device.iOS);
        }
    }
}
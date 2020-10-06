using Android.App;
using MPS.AppSocio.Views.CV;
using MPS.Core.Lib.ViewModels.Socios;
using MPS.SharedAPIModel.Socios;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPS.AppSocio.Views.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
            ViewModel.ObtenerDetalleSocioCommand.Execute(null);
        }

        DetalleSocioViewModel ViewModel => BindingContext as DetalleSocioViewModel;

        private void PasswordPopUp(object o, EventArgs e)
        {
            PopupNavigation.Instance.PushAsync(new PasswordPopUp());
        }

        private void Actualizar_Info(object o, EventArgs e)
        {
            DetalleSocio Info = new DetalleSocio
            {
                NOMBRE = Nombre.Text,
                APELLIDO_1 = Apellido_paterno.Text,
                APELLIDO_2 = Apellido_materno.Text,
                FECHA_NACIMIENTO = Fecha_nacimiento.Date.ToShortDateString(),
                E_MAIL = Correo.Text,
                TEL_NUMERO = Telefono.Text
            };
            ViewModel.ActualizaInfoCommand.Execute(Info);
            //AlertDialog alerta = new AlertDialog.Builder(this).Create();
            //alerta.SetTitle("Finalizado");
            //alerta.SetMessage(ViewModel.Mensaje);
            //alerta.SetButton("Aceptar", (c, d) => { });
            //alerta.Show();
        }
    }
}
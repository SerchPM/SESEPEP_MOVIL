using MPS.AppSocio.Views.CV;
using MPS.Core.Lib.ViewModels.Socios;
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
    }
}
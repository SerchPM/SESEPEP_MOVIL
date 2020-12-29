//using Android.App;
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
        }

        protected override async void OnAppearing()
        {
            await ViewModel.ObtenerDetalleSocioCommand.ExecuteAsync();
        }
        //private void PasswordPopUp(object o, EventArgs e)
        //{
        //    PopupNavigation.Instance.PushAsync(new PasswordPopUp());
        //}
    }
}
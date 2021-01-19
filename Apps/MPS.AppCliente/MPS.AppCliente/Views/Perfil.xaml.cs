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
    public partial class Perfil : ContentPage
    {
        public Perfil()
        {
            InitializeComponent();
        }
        protected override async void OnAppearing()
        {
            ViewModel.IOS = Device.RuntimePlatform.Equals(Device.iOS);
            await ViewModel.ObtenerClienteCommand.ExecuteAsync();
        }
    }
}
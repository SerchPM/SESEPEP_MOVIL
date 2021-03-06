using MPS.Core.Lib.ViewModels.Socios;
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
    public partial class FormaDePago : ContentPage
    {
        public FormaDePago()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            ViewModel.ObtenerDatosBancariosCommand.Execute();
        }
    }
}
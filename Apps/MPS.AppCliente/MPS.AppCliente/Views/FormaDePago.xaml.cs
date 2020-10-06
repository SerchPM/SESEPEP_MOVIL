using MPS.Core.Lib.ViewModels.Clientes;
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
    public partial class FormaDePago : ContentPage
    {
        public FormaDePago()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ViewModel.ObtenerComponentesCommand.Execute();
        }
    }
}
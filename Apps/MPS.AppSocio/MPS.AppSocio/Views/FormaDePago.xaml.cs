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
            ViewModel.GetInfoCommand.Execute(null);
        }

        PagoViewModel ViewModel => BindingContext as PagoViewModel;

        private void Update_Info()
        {
            List<string> info = new List<string>();
            if (!(string.IsNullOrEmpty(Cuenta.Text)) && !(string.IsNullOrEmpty(Banco.Text)))
            {
                info.Add(Cuenta.Text);
                info.Add(Banco.Text);
                ViewModel.UpdateInfoCommand.Execute(info);
            }
        }
    }
}
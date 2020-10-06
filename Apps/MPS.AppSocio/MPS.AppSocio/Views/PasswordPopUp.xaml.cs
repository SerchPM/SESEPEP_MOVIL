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
    public partial class PasswordPopUp
    {
        public PasswordPopUp()
        {
            InitializeComponent();
        }

        DetalleSocioViewModel ViewModel => BindingContext as DetalleSocioViewModel;
        private void Envia_Contraseña(object o, EventArgs e)
        {
            if ((Password1.Text.Equals(Password2.Text)) && (Password1.Text.Length >= 8))
            {
                
                ViewModel.UpdatePasswordCommand.Execute(Password1.Text);
            }
            else
            {
                ViewModel.Mensaje = "Contraseña inválida";
            }
        }
    }
}
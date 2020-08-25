using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Sysne.Core.OS;

namespace MPS.AppCliente.Views.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
            //Sysne.Core.OS.DependencyService.Get<IOS>().SetStatusBarColor(((Color)App.Current.Resources["AlterColor"]).ToHex());
        }
    }
}
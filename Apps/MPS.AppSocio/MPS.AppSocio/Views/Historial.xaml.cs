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
    public partial class Historial : ContentPage
    {
        public Historial()
        {
            InitializeComponent();
        }

        HistorialSolicitudesViewModel ViewModel => BindingContext as HistorialSolicitudesViewModel;

        public void Buscar(object o, EventArgs e)
        {
            List<string> fechas = new List<string>();
            fechas.Add(FechaInicial.Date.ToShortDateString());
            fechas.Add(FechaFinal.Date.ToShortDateString());
            ViewModel.GetSolicitudesCommand.Execute(fechas);
        }
    }
}
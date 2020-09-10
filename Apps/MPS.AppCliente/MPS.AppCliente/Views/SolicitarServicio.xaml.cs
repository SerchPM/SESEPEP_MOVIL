using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MPS.AppCliente
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class SolicitarServicio : ContentPage
    {

        bool isVisibleListaServicios = false;
        bool IsVisibleListaServicios
        {
            get => isVisibleListaServicios;
            set
            {
                VisualStateManager.GoToState(this, value ? "SeleccionarServicio" : "ServicioSeleccionado");
                isVisibleListaServicios = value;
            }
        }
        public SolicitarServicio()
        {
            InitializeComponent();
        }

        private void SolicitarServicio_Tapped(object sender, EventArgs e) => IsVisibleListaServicios = !IsVisibleListaServicios;

        private void ServiciosListado_SelectionChanged(object sender, SelectionChangedEventArgs e) => IsVisibleListaServicios = false;

    }
}

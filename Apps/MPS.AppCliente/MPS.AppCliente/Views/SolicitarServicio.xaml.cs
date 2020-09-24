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
            iconApp.SizeChanged += (se, ee) => 
            {
                spacingIcon.Width = iconApp.Width;
            };           
            ViewModel.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(ViewModel.EsExpress):
                        VisualStateManager.GoToState(this, ViewModel.EsExpress ? "Express" : "Personalizado");
                        break;
                    default:
                        break;
                }
            };
        }

        private void SolicitarServicio_Tapped(object sender, EventArgs e) => IsVisibleListaServicios = !IsVisibleListaServicios;

        private void ServiciosListado_SelectionChanged(object sender, SelectionChangedEventArgs e) => IsVisibleListaServicios = false;

        private void ImageButton_Clicked(object sender, EventArgs e)
        {
            modalServicioSolicitado.IsVisible = true;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            modalServicioSolicitado.IsVisible = false;
        }
    }
}

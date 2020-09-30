using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

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

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Position position = new Position(19.432476, -99.133606);
            if (Device.RuntimePlatform == Device.Android)
            {
                Task.Delay(2000)
                    .ContinueWith(task => Device.BeginInvokeOnMainThread(() => Map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(0.5)))));
            }
            else
                Map.MoveToRegion(MapSpan.FromCenterAndRadius(position, Distance.FromKilometers(1.5)));
        }
        private void Map_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            var (la, lo) = (e.Position.Latitude, e.Position.Longitude);
            Position position = new Position(la, lo);
            MapSpan mapSpan = new MapSpan(position, 0.01, 0.01);
            Map = new Map(mapSpan);
        }
    }
}

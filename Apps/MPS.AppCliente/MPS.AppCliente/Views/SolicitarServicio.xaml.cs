using MPS.AppCliente.Views.CV;
using MPS.Core.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
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
            iconAppSolicitud.SizeChanged += (se, ee) =>
            {
                spacingIconSolicitud.Width = iconAppSolicitud.Width;
            };
            iconAppPago.SizeChanged += (se, ee) =>
            {
                spacingIconPago.Width = iconAppPago.Width;
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
            ViewModel.ObteniendoUbicacion += async (s, e) =>
            {
                await Task.Run(() =>
                {
                    Device.BeginInvokeOnMainThread(() =>
                    {
                        Map.UbicacionActual = e.Geoposicion;
                        Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(e.Geoposicion.Latitud.Value, e.Geoposicion.Longitud.Value), Distance.FromMiles(0.2)));
                    });
                });
            };
        }

        protected override async void OnAppearing()
        {
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(19.043455, -98.198686), Distance.FromMiles(0.2)));
            await ViewModel.ObtenerComponentesCommand.ExecuteAsync();
            ViewModel.VerificarCalificacionCommand.Execute();
            ViewModel.VerificarEstatusPagoCommand.Execute();
            ViewModel.VerificarSolicitudCommand.Execute();
        }

        private void SolicitarServicio_Tapped(object sender, EventArgs e) => IsVisibleListaServicios = !IsVisibleListaServicios;

        private void ServiciosListado_SelectionChanged(object sender, SelectionChangedEventArgs e) => IsVisibleListaServicios = false;

        private void Map_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            ViewModel.ObtenerDireccionCommand.Execute((e.Position.Latitude, e.Position.Longitude));
        }

        private async void TapGestureRecognizer_Terminos(object sender, EventArgs e) =>
            await Browser.OpenAsync("https://mpsmovil.com/terminos-condiciones.html", BrowserLaunchMode.SystemPreferred);
    }
}

using MPS.Core.Lib.ViewModels.Socios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace MPS.AppSocio.Views.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SolicitarServicio : ContentPage
    {
        public SolicitarServicio()
        {
            InitializeComponent();
            iconApp.SizeChanged += (se, ee) =>
            {
                spacingIcon.Width = iconApp.Width;
                spasingIconR.Width = iconApp.Width;
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

            ViewModel.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case "EnAtencion":
                        if (ViewModel.EnAtencion)
                        {
                            Device.StartTimer(new TimeSpan(0, 2, 0), () =>
                            {
                                if (ViewModel.EnAtencion)
                                {
                                    ViewModel.RegistrarUbicacionCommand.Execute();
                                    return true;
                                }
                                else
                                    return false;
                            });
                        }
                        break;
                    default:
                        break;
                }
            };
        }

        protected override void OnAppearing()
        {
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(19.043455, -98.198686), Distance.FromMiles(0.2)));
            ViewModel.ObtenerComponentesCommand.Execute();
            ViewModel.VerificarSolicitudCommand.Execute();
        }

        private async void Button_OpenMapAsync(object sender, EventArgs e)
        {
            if (ViewModel.SolicitudDeServicio.Equals(null) || string.IsNullOrEmpty(ViewModel.SolicitudDeServicio.Ubicacion))
                return;

            (double latitud, double longitud) = Core.Lib.Helpers.Utilidades.LimpiarCadenaUbicacion(ViewModel.SolicitudDeServicio.Ubicacion);
            var location = new Location(latitud, longitud);
            var options = new MapLaunchOptions { Name = ViewModel.NombreUbicacion ?? string.Empty, NavigationMode = NavigationMode.None };
            try
            {
                await Xamarin.Essentials.Map.OpenAsync(location, options);
            }
            catch (Exception)
            {
                // No map application available to open
            }

        }

        private async void TapGestureRecognizer_OpenMapRuta(object sender, EventArgs e)
        {
            if (ViewModel.ServicioAtencion.Equals(null) || string.IsNullOrEmpty(ViewModel.ServicioAtencion.UBICACION_SERVICIO))
                return;

            (double latitud, double longitud) = Core.Lib.Helpers.Utilidades.LimpiarCadenaUbicacion(ViewModel.ServicioAtencion.UBICACION_SERVICIO);
            var location = new Location(latitud, longitud);
            var options = new MapLaunchOptions { Name = ViewModel.NombreUbicacion ?? string.Empty, NavigationMode = NavigationMode.Driving };
            try
            {
                await Xamarin.Essentials.Map.OpenAsync(location, options);
            }
            catch (Exception)
            {
                // No map application available to open
            }
        }
    }
}
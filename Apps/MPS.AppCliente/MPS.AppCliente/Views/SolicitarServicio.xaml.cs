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
            ViewModel.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case nameof(ViewModel.EsExpress):
                        VisualStateManager.GoToState(this, ViewModel.EsExpress ? "Express" : "Personalizado");
                        break;
                    case "CargarPersonal":
                        if (ViewModel.CargarPersonal)
                        {
                            personales.Children.Clear();
                            foreach (var socio in ViewModel.Socios)
                            {
                                PersonalItem personal = new PersonalItem
                                {
                                    Nombre = socio.NOMBRE_COMPLETO,
                                    NombreEmpresa = socio.NO_SOCIO,
                                    Imagen = socio.IMAGEN,
                                    Especialidades = socio.SERVICIOS,
                                    SelectedCommand = ViewModel.SeleccionarPersonalCommand,
                                    SelectedCommandParameter = socio,
                                    Edad = socio.EDAD.ToString(),
                                    SourseSelected = "checkoff.png"
                                };
                                personales.Children.Add(personal);
                            }
                            ViewModel.CargarPersonal = false;
                        }
                        break;
                    case "SeleccionarPersonal":
                        if(ViewModel.SeleccionarPersonal)
                        {
                            personales.Children.Clear();
                            foreach (var socio in ViewModel.Socios)
                            {
                                PersonalItem personal = new PersonalItem
                                {
                                    Nombre = socio.NOMBRE_COMPLETO,
                                    NombreEmpresa = socio.NO_SOCIO,
                                    Imagen = socio.IMAGEN,
                                    Especialidades = socio.SERVICIOS,
                                    SelectedCommand = ViewModel.SeleccionarPersonalCommand,
                                    SelectedCommandParameter = socio,
                                    Edad = socio.EDAD.ToString(),
                                    SourseSelected = ViewModel.SociosSeleccionado.Where(w => w.GUID_SOCIO.Equals(socio.GUID_SOCIO)).Count() > 0 ? "checkin.png" : "checkoff.png"
                                };
                                personales.Children.Add(personal);
                            }
                            ViewModel.SeleccionarPersonal = false;
                        }
                        break;
                    case "CargarPersonalSeleccionado":
                        if (ViewModel.CargarPersonalSeleccionado)
                        {
                            personalSelected.Children.Clear();
                            foreach (var socio in ViewModel.SociosSeleccionado)
                            {
                                PersonalRemove personal = new PersonalRemove
                                {
                                    Nombre = socio.NOMBRE_COMPLETO,
                                    NombreEmpresa = socio.NO_SOCIO,
                                    Imagen = socio.IMAGEN,
                                    RemoveCommand = ViewModel.RemovePersonalCommand,
                                    RemoveCommandParameter = socio,
                                    Edad = socio.EDAD.ToString()
                                };
                                personalSelected.Children.Add(personal);
                            }
                            ViewModel.CargarPersonalSeleccionado = false;
                        }
                        break;
                    case "RemovePersonal":
                        if (ViewModel.RemovePersonal)
                        {
                            personalSelected.Children.Clear();
                            foreach (var socio in ViewModel.SociosSeleccionado)
                            {
                                PersonalRemove personal = new PersonalRemove
                                {
                                    Nombre = socio.NOMBRE_COMPLETO,
                                    NombreEmpresa = socio.NO_SOCIO,
                                    Imagen = socio.IMAGEN,
                                    RemoveCommand = ViewModel.RemovePersonalCommand,
                                    RemoveCommandParameter = socio,
                                    Edad = socio.EDAD.ToString()
                                };
                                personalSelected.Children.Add(personal);
                            }
                            ViewModel.RemovePersonal = false;
                        }
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

        protected override void OnAppearing()
        {
            Map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(19.043455, -98.198686), Distance.FromMiles(0.2)));
            ViewModel.VerificarSolicitudCommand.Execute();
            ViewModel.ObtenerComponentesCommand.Execute();
            if (string.IsNullOrEmpty(Settings.Current.ModeloDispositivo))
                Settings.Current.ModeloDispositivo = DeviceInfo.Model;
            
        }

        private void SolicitarServicio_Tapped(object sender, EventArgs e) => IsVisibleListaServicios = !IsVisibleListaServicios;

        private void ServiciosListado_SelectionChanged(object sender, SelectionChangedEventArgs e) => IsVisibleListaServicios = false;

        private void Map_MapClicked(object sender, Xamarin.Forms.Maps.MapClickedEventArgs e)
        {
            ViewModel.ObtenerDireccionCommand.Execute((e.Position.Latitude, e.Position.Longitude));
        }
    }
}

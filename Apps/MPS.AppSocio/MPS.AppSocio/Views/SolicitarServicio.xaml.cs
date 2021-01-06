using MPS.Core.Lib.ViewModels.Socios;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    }
}
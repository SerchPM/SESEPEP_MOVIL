using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPS.AppSocio.Views.CV;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPS.AppSocio.Views.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Solicitudes : ContentPage
    {
        public Solicitudes()
        {
            InitializeComponent();
            ViewModel.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case "CargarServicios":
                        if (ViewModel.CargarServicios)
                        {
                            servicios.Children.Clear();
                            foreach (var solicitud in ViewModel.Solicitudes)
                            {
                                servicios.Children.Add(new SolicitudItem
                                {
                                    TipoServicio = solicitud.TIPO_SOLICITUD,
                                    SourseTipoServicio = solicitud.TIPO_SERVICIO.Equals("EXPRESS") ? "servicioExpressActivo.png" : "servicioPersonalizadoActivo.png",
                                    Cliente = solicitud.NOMBRE_COMPLETO,
                                    AceptacionServicio = solicitud.FECHA_SOLICITUD,
                                    Costo = solicitud.COSTO_SOCIO ?? 0,
                                    Horas = solicitud.HORAS_PACTADAS,
                                    IniciarServicioCommand = ViewModel.IniciarServicioCommand,
                                    IniciarServicioCommandParameter = solicitud
                                });
                            }
                            ViewModel.CargarServicios = false;
                        }
                        break;
                    default:
                        break;
                }
            };
        }

        protected override async void OnAppearing()
        {
            await ViewModel.ObtenerSolicitudesPendientesCommand.ExecuteAsync();
        }
    }
}
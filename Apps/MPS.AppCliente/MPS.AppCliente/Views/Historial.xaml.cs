using MPS.AppCliente.Views.CV;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPS.AppCliente.Views.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Historial : ContentPage
    {
        public Historial()
        {
            InitializeComponent();
            ViewModel.PropertyChanged += (s, e) =>
            {
                switch (e.PropertyName)
                {
                    case "CargarSolicitudes":
                        if (ViewModel.CargarSolicitudes)
                        {
                            solicitudes.Children.Clear();
                            foreach (var solicitud in ViewModel.Historial)
                            {
                                HistoryItem history = new HistoryItem
                                {
                                    Servicio = solicitud.TIPO_SERVICIO,
                                    Ranking = solicitud.VALORACION_CLIENTE ?? 0,
                                    Soli = solicitud.FECHA_SOLICITUD,
                                    InicioSolicitud = solicitud.INICIO_SOLICITUD,
                                    Costo = solicitud.TOTAL_PAGADO ?? 0,
                                    Horas = solicitud.TIEMPO_PACTADO,
                                    EnviarCommand = ViewModel.EnviarHistoricoCommand,
                                    EnviarCommandParameter = solicitud
                                };
                                solicitudes.Children.Add(history);
                            }
                            ViewModel.CargarSolicitudes = false;
                        }
                        break;

                    default:
                        break;
                }
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            solicitudes.Children.Clear();
            ViewModel.Historial.Clear();
            ViewModel.Desde = DateTime.Now;
            ViewModel.Hasta = DateTime.Now;
        }
    }
}
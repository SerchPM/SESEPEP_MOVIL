using MPS.AppSocio.Views.CV;
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
                                    Soli = solicitud.FECHA_SOLICITUD,
                                    InicioSolicitud = solicitud.INICIO_SOLICITUD,
                                    Costo = solicitud.TOTAL_PAG_SOCIO ?? 0,
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
    }
}
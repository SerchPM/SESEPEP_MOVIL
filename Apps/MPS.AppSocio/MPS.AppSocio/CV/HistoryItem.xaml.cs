using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPS.AppSocio.Views.CV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryItem : ContentView
    {
        public HistoryItem()
        {
            InitializeComponent();
        }

        public string Tipo_Servicio
        {
            get => tipo_servicio.Text;
            set => tipo_servicio.Text = value;
        }

        public string Fecha_Solicitud
        {
            get => fecha_solicitud.Text;
            set => fecha_solicitud.Text = value;
        }

        public string Monto
        {
            get => monto.Text;
            set => monto.Text = value;
        }

        public string Fecha_Inicio
        {
            get => fecha_inicio.Text;
            set => fecha_inicio.Text = value;
        }

        public string Tiempo_Real
        {
            get => tiempo_real.Text;
            set => tiempo_real.Text = value;
        }
    }
}
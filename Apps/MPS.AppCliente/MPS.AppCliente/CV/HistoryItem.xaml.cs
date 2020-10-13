using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPS.AppCliente.Views.CV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HistoryItem : ContentView
    {
        public HistoryItem()
        {
            InitializeComponent();
        }

        public DateTime InicioSolicitud
        {
            get => (DateTime)GetValue(InicioSolicitudProperty);
            set => SetValue(InicioSolicitudProperty, value);
        }

        public static readonly BindableProperty InicioSolicitudProperty = BindableProperty.Create(nameof(InicioSolicitud), typeof(DateTime), typeof(HistoryItem), default(DateTime),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (HistoryItem)bindable;
                me.InicioSolicitud = (DateTime)newValue;
                me.inicio.Text = me.InicioSolicitud.ToString("dd/MM/yyyy hh:mm:ss") + " (Inicio)";
            });

        public decimal? Costo
        {
            get => (decimal?)GetValue(CostoProperty);
            set => SetValue(CostoProperty, value);
        }

        public static readonly BindableProperty CostoProperty = BindableProperty.Create(nameof(Costo), typeof(decimal?), typeof(HistoryItem), default(decimal?),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (HistoryItem)bindable;
                me.Costo = (decimal?)newValue;
                me.costo.Text = me.Costo.HasValue ? me.Costo.Value.ToString("C2") : "$0.00";
            });

        public int Horas
        {
            get => (int)GetValue(HorasProperty);
            set => SetValue(HorasProperty, value);
        }

        public static readonly BindableProperty HorasProperty = BindableProperty.Create(nameof(Horas), typeof(int), typeof(HistoryItem), default(int),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (HistoryItem)bindable;
                me.Horas = (int)newValue;
                me.tiempo.Text = $"{me.Horas} hrs";
            });

        public DateTime Soli
        {
            get => (DateTime)GetValue(SoliProperty);
            set => SetValue(SoliProperty, value);
        }

        public static readonly BindableProperty SoliProperty = BindableProperty.Create(nameof(Soli), typeof(DateTime), typeof(HistoryItem), default(DateTime),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (HistoryItem)bindable;
                me.Soli = (DateTime)newValue;
                me.solicitud.Text = me.Soli.ToString("dd/MM/yyyy hh:mm:ss") + " (Solicitud)";
            });

        public ICommand EnviarCommand
        {
            get => (ICommand)GetValue(EnviarCommandProperty);
            set => SetValue(EnviarCommandProperty, value);
        }

        public static readonly BindableProperty EnviarCommandProperty = BindableProperty.Create(nameof(EnviarCommand), typeof(ICommand), typeof(HistoryItem), default(ICommand),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (HistoryItem)bindable;
                me.EnviarCommand = (ICommand)newValue;
                me.enviar.Command = me.EnviarCommand;
            });

        public object EnviarCommandParameter
        {
            get => GetValue(EnviarCommandParameterProperty);
            set => SetValue(EnviarCommandParameterProperty, value);
        }

        public static readonly BindableProperty EnviarCommandParameterProperty = BindableProperty.Create(nameof(EnviarCommandParameter), typeof(object), typeof(HistoryItem),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (HistoryItem)bindable;
                me.EnviarCommandParameter = (object)newValue;
                me.enviar.CommandParameter = me.EnviarCommandParameter;
            });
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPS.AppSocio.Views.CV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SolicitudItem : ContentView
    {
        public SolicitudItem()
        {
            InitializeComponent();
        }
        public string TipoServicio
        {
            get => (string)GetValue(TipoServicioProperty);
            set => SetValue(TipoServicioProperty, value);
        }

        public static readonly BindableProperty TipoServicioProperty = BindableProperty.Create(nameof(TipoServicio), typeof(string), typeof(SolicitudItem), default(string),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (SolicitudItem)bindable;
                me.TipoServicio = (string)newValue;
                me.servicio.Text = me.TipoServicio;
            });

        public string SourseTipoServicio
        {
            get => (string)GetValue(SourseTipoServicioProperty);
            set => SetValue(SourseTipoServicioProperty, value);
        }

        public static readonly BindableProperty SourseTipoServicioProperty = BindableProperty.Create(nameof(SourseTipoServicio), typeof(string), typeof(SolicitudItem),
        defaultBindingMode: BindingMode.TwoWay, propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (SolicitudItem)bindable;
            me.tipoSolicitud.Source = (string)newValue;
        });

        public string Cliente
        {
            get => (string)GetValue(ClienteProperty);
            set => SetValue(ClienteProperty, value);
        }

        public static readonly BindableProperty ClienteProperty = BindableProperty.Create(nameof(Cliente), typeof(string), typeof(SolicitudItem), default(string),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (SolicitudItem)bindable;
                me.Cliente = (string)newValue;
                me.cliente.Text = me.Cliente;
            });

        public DateTime AceptacionServicio
        {
            get => (DateTime)GetValue(InicioSolicitudProperty);
            set => SetValue(InicioSolicitudProperty, value);
        }

        public static readonly BindableProperty InicioSolicitudProperty = BindableProperty.Create(nameof(AceptacionServicio), typeof(DateTime), typeof(SolicitudItem), default(DateTime),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (SolicitudItem)bindable;
                me.AceptacionServicio = (DateTime)newValue;
                me.solicitud.Text = me.AceptacionServicio.ToString("dd/MM/yyyy hh:mm:ss");
            });

        public decimal? Costo
        {
            get => (decimal?)GetValue(CostoProperty);
            set => SetValue(CostoProperty, value);
        }

        public static readonly BindableProperty CostoProperty = BindableProperty.Create(nameof(Costo), typeof(decimal?), typeof(SolicitudItem), default(decimal?),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (SolicitudItem)bindable;
                me.Costo = (decimal?)newValue;
                me.costo.Text = me.Costo.HasValue ? me.Costo.Value.ToString("C2") : "$0.00";
            });

        public int Horas
        {
            get => (int)GetValue(HorasProperty);
            set => SetValue(HorasProperty, value);
        }

        public static readonly BindableProperty HorasProperty = BindableProperty.Create(nameof(Horas), typeof(int), typeof(SolicitudItem), default(int),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (SolicitudItem)bindable;
                me.Horas = (int)newValue;
                me.tiempo.Text = $"{me.Horas} hrs";
            });

        public ICommand IniciarServicioCommand
        {
            get => (ICommand)GetValue(IniciarServicioCommandProperty);
            set => SetValue(IniciarServicioCommandProperty, value);
        }

        public static readonly BindableProperty IniciarServicioCommandProperty = BindableProperty.Create(nameof(IniciarServicioCommand), typeof(ICommand), typeof(SolicitudItem), default(ICommand),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (SolicitudItem)bindable;
                me.IniciarServicioCommand = (ICommand)newValue;
                me.iniciar.Command = me.IniciarServicioCommand;
            });

        public object IniciarServicioCommandParameter
        {
            get => GetValue(IniciarServicioCommandParameterProperty);
            set => SetValue(IniciarServicioCommandParameterProperty, value);
        }

        public static readonly BindableProperty IniciarServicioCommandParameterProperty = BindableProperty.Create(nameof(IniciarServicioCommandParameter), typeof(object), typeof(SolicitudItem),
            propertyChanged: (bindable, oldValue, newValue) =>
            {
                var me = (SolicitudItem)bindable;
                me.IniciarServicioCommandParameter = (object)newValue;
                me.iniciar.CommandParameter = me.IniciarServicioCommandParameter;
            });
    }
}
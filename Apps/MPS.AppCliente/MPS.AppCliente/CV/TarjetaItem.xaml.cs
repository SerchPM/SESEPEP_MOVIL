using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPS.AppCliente.Views.CV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TarjetaItem : ContentView
    {
        public TarjetaItem()
        {
            InitializeComponent();
        }

        public string NoTarjeta
        {
            get => (string)GetValue(NoTarjetaProperty);
            set => SetValue(NoTarjetaProperty, value);
        }

        public static readonly BindableProperty NoTarjetaProperty = BindableProperty.Create(nameof(NoTarjeta), typeof(string), typeof(TarjetaItem), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (TarjetaItem)bindable;
            me.NoTarjeta = (string)newValue;
            me.noTarjeta.Text = me.NoTarjeta;
        });

        public string TipoTarjeta
        {
            get => (string)GetValue(TipoTarjetaProperty);
            set => SetValue(TipoTarjetaProperty, value);
        }

        public static readonly BindableProperty TipoTarjetaProperty = BindableProperty.Create(nameof(TipoTarjeta), typeof(string), typeof(TarjetaItem), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (TarjetaItem)bindable;
            me.TipoTarjeta = (string)newValue;
            me.tipoTarjeta.Text = me.TipoTarjeta;
        });

        public string Orden
        {
            get => (string)GetValue(OrdenProperty);
            set => SetValue(OrdenProperty, value);
        }

        public static readonly BindableProperty OrdenProperty = BindableProperty.Create(nameof(Orden), typeof(string), typeof(TarjetaItem), default(string),
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (TarjetaItem)bindable;
            me.Orden = (string)newValue;
            me.orden.Text = me.Orden;
        });
    }
}
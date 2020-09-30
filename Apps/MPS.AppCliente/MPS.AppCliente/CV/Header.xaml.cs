using MPS.Core.Lib.ViewModels;
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
    public partial class Header : ContentView
    {
        public Header()
        {
            InitializeComponent();
            ViewModel.NombreCommand.Execute(null);
        }


        public HeaderPages ActivePage
        {
            get => (HeaderPages)GetValue(ActivePageProperty);
            set => SetValue(ActivePageProperty, value);
        }
        public static readonly BindableProperty ActivePageProperty = BindableProperty.Create(nameof(ActivePage), typeof(HeaderPages), typeof(Header), HeaderPages.SolicitarServicio,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (Header)bindable;
            me.ActivePage = (HeaderPages)newValue;
            me.solicitarServicio.Source = me.ActivePage == HeaderPages.SolicitarServicio ? "solicitarServicioSel.png" : "solicitarServicio.png";
            me.historial.Source = me.ActivePage == HeaderPages.Historial ? "historialSel.png" : "historial.png";
            me.formaDePago.Source = me.ActivePage == HeaderPages.FormaDePago ? "formaDePagoSel.png" : "formaDePago.png";
            me.perfil.Source = me.ActivePage == HeaderPages.Perfil ? "perfilSel.png" : "perfil.png";
        });

        public enum HeaderPages
        {
            SolicitarServicio,
            Historial,
            FormaDePago,
            Perfil
        }

        HeaderViewModel ViewModel => BindingContext as HeaderViewModel;
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MPS.AppSocio.Views.CV
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StatusBar : ContentView
    {
        public StatusBar()
        {
            InitializeComponent();
        }
        public bool IsLinksVisible
        {
            get => (bool)GetValue(IsLinksVisiblesProperty);
            set => SetValue(IsLinksVisiblesProperty, value);
        }
        public static readonly BindableProperty IsLinksVisiblesProperty = BindableProperty.Create(nameof(IsLinksVisible), typeof(bool), typeof(StatusBar), true,
        propertyChanged: (bindable, oldValue, newValue) =>
        {
            var me = (StatusBar)bindable;
            me.IsLinksVisible = (bool)newValue;
            me.Links.IsVisible = me.IsLinksVisible;
        });

        private async void TapGestureRecognizer_Privacidad(object sender, EventArgs e) => 
             await Browser.OpenAsync("https://dev.mpsmovil.com/aviso-de-privacidad.html", BrowserLaunchMode.SystemPreferred);

        private async void TapGestureRecognizer_Socios(object sender, EventArgs e) =>
            await Browser.OpenAsync("https://dev.mpsmovil.com/socios/Socio.html", BrowserLaunchMode.SystemPreferred);

        private async void TapGestureRecognizer_SitioWeb(object sender, EventArgs e) =>
            await Browser.OpenAsync("https://dev.mpsmovil.com", BrowserLaunchMode.SystemPreferred);

        private async void TapGestureRecognizer_Ayuda(object sender, EventArgs e) =>
            await Browser.OpenAsync("https://dev.mpsmovil.com/ayuda.html", BrowserLaunchMode.SystemPreferred);
    }
}
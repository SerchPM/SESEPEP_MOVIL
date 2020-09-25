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
    }
}
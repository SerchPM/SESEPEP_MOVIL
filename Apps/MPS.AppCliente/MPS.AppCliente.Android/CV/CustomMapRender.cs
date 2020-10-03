using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MPS.AppCliente.Droid.CV;
using MPS.AppCliente.Views.MyMap;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Maps.Android;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRender))]
namespace MPS.AppCliente.Droid.CV
{
    public class CustomMapRender : MapRenderer
    {

        private CustomMap customRenderMap;

        public CustomMapRender(Context contex) : base(contex)
        {

        }

        protected override void OnElementChanged(ElementChangedEventArgs<Map> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
                NativeMap.MapClick -= NativeMap_MapClick;
            if (e.NewElement != null)
            {
                customRenderMap = (CustomMap)e.NewElement;
                Control.GetMapAsync(this);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomMap.UbicacionActualProperty.PropertyName)
                MarcarUbicacionActual(Element as CustomMap);
        }

        private void NativeMap_MapClick(object sender, GoogleMap.MapClickEventArgs e)
        {
           
        }

        private void MarcarUbicacionActual(CustomMap mapCustom)
        {
            var ubicacion = mapCustom.UbicacionActual;
            if (ubicacion != null)
            {
                NativeMap.Clear();
                MarkerOptions miUbicacion = new MarkerOptions();
                miUbicacion.SetPosition(new LatLng(ubicacion.Latitud.Value, ubicacion.Longitud.Value));
                NativeMap.AddMarker(miUbicacion);
            }
        }
    }
}
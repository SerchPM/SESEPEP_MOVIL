using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using MPS.SharedAPIModel;

namespace MPS.AppCliente.Views.MyMap
{
    public class CustomMap : Map
    {
        public static readonly BindableProperty UbicacionActualProperty = BindableProperty.Create(nameof(UbicacionActual), typeof(Geoposicion), typeof(CustomMap),
         propertyChanged: (bindable, oldValue, newValue) =>
         {
             var me = (CustomMap)bindable;
             me.UbicacionActual = (Geoposicion)newValue;
            });

        public Geoposicion UbicacionActual
        {
            get { return (Geoposicion)GetValue(UbicacionActualProperty); }
            set { SetValue(UbicacionActualProperty, value); }
        }
    }
}

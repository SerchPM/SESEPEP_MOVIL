using MPS.AppSocio.UWP.CV;
using MPS.AppSocio.Views.MyMap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
using Xamarin.Forms.Maps.UWP;
using Xamarin.Forms.Platform.UWP;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRender))]
namespace MPS.AppSocio.UWP.CV
{
    public class CustomMapRender : MapRenderer
    {
        CustomMap customMap;
        MapControl mapControl;
        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.Maps.Map> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                mapControl.Children.Clear();
                mapControl = null;
            }
            if (e.NewElement != null)
            {
                customMap = (CustomMap)e.NewElement;
                mapControl = Control as MapControl;
                mapControl.Children.Clear();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomMap.UbicacionActualProperty.PropertyName)
            {
                MarcarPosicionActual((Element as CustomMap));
            }
        }

        private void MarcarPosicionActual(CustomMap mapCustom)
        {
            try
            {
                mapControl.MapElements.Clear();
                if (mapCustom.UbicacionActual != null)
                {
                    BasicGeoposition basicGeoposition = new BasicGeoposition { Latitude = mapCustom.UbicacionActual.Latitud.Value, Longitude = mapCustom.UbicacionActual.Longitud.Value };
                    Geopoint geopoint = new Geopoint(basicGeoposition);
                    MapIcon mapIcon = new MapIcon();
                    mapIcon.CollisionBehaviorDesired = MapElementCollisionBehavior.RemainVisible;
                    mapIcon.Location = geopoint;
                    mapIcon.NormalizedAnchorPoint = new Windows.Foundation.Point(0.5, 1.0);
                    mapControl.MapElements.Add(mapIcon);
                }
            }
            catch
            {

            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CoreLocation;
using Foundation;
using MapKit;
using MPS.AppSocio.iOS.CV;
using MPS.AppSocio.Views.MyMap;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Maps.iOS;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomMap), typeof(CustomMapRender))]
namespace MPS.AppSocio.iOS.CV
{
    public class CustomMapRender : MapRenderer
    {
        CustomMap formsMap;
        MKMapView nativeMap;

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                nativeMap = Control as MKMapView;
                nativeMap.GetViewForAnnotation -= GetViewForAnotation;
            }

            if (e.NewElement != null)
            {
                formsMap = (CustomMap)e.NewElement;
                nativeMap = Control as MKMapView;
                nativeMap.ZoomEnabled = true;
                nativeMap.GetViewForAnnotation += GetViewForAnotation;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == CustomMap.UbicacionActualProperty.PropertyName)
            {
                CustomMap MapaBindable = Element as CustomMap;
                MarcarUbicacionActual(MapaBindable);
            }
        }
        private void MarcarUbicacionActual(CustomMap mapCustom)
        {
            try
            {
                nativeMap.RemoveAnnotations(nativeMap.Annotations);
                if (mapCustom.UbicacionActual != null && mapCustom.UbicacionActual.Latitud != 0 && mapCustom.UbicacionActual.Longitud != 0)
                {
                    var pin = nativeMap.DequeueReusableAnnotation("UbicacionActual");
                    if (Device.RuntimePlatform == Device.iOS)
                    {
                        if (pin == null)
                        {
                            nativeMap.AddAnnotations(new MKUbicacionActual(string.Empty,
                                new CLLocationCoordinate2D()
                                {
                                    Latitude = mapCustom.UbicacionActual.Latitud.Value,
                                    Longitude = mapCustom.UbicacionActual.Longitud.Value
                                }));
                        }
                        else
                            nativeMap.AddAnnotations(new MKUbicacionActual(string.Empty,
                                new CLLocationCoordinate2D()
                                {
                                    Latitude = mapCustom.UbicacionActual.Latitud.Value,
                                    Longitude = mapCustom.UbicacionActual.Longitud.Value
                                }));
                    }
                    if (pin == null)
                    {
                        nativeMap.AddAnnotations(new MKUbicacionActual(string.Empty,
                            new CLLocationCoordinate2D()
                            {
                                Latitude = mapCustom.UbicacionActual.Latitud.Value,
                                Longitude = mapCustom.UbicacionActual.Longitud.Value
                            }));
                    }
                }
            }
            catch { }
        }

        private MKAnnotationView GetViewForAnotation(MKMapView mapView, IMKAnnotation annotation)
        {
            MKAnnotationView annotationView = null;
            if (annotation is MKUbicacionActual)
            {
                annotationView = nativeMap.DequeueReusableAnnotation("UbicacionActual");
                if (annotationView == null)
                    annotationView = new MKAnnotationView(annotation, "UbicacionActual");
            }
            return annotationView;
        }

        public class MKUbicacionActual : MKAnnotation
        {
            CLLocationCoordinate2D coord;
            public override CLLocationCoordinate2D Coordinate
            {
                get
                {
                    return coord;
                }
            }

            private string title;

            public override string Title
            {
                get { return title; }
            }

            public MKUbicacionActual(string title, CLLocationCoordinate2D coord)
            {
                this.title = title;
                this.coord = coord;
            }
        }
    }
}
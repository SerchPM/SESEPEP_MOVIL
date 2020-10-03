using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Sysne.Core.OS;
using System.Threading.Tasks;
using Xamarin.Essentials;
using System.Net;
using System.Net.Mail;
using MPS.SharedAPIModel;

namespace MPS.AppCliente.Droid.OS
{
    public class OS : IOS
    {
        static internal Activity Activity { get; set; }

        public void SetStatusBarColor(string color)
        {
            if (Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Activity.Window.SetStatusBarColor(Android.Graphics.Color.ParseColor(color));
                Activity.Window.SetNavigationBarColor(Android.Graphics.Color.ParseColor(color));
            }
        }

        public void HideNavigation(bool show)
        {
            //if (show && Android.OS.Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            //{
                //Activity.Window.SetNavigationBarColor(Android.Graphics.Color.Transparent); //CAMBIA EL COLOR DE NAVIGATION BAR
                //Activity.Window.DecorView.SystemUiVisibility = (StatusBarVisibility)(SystemUiFlags.HideNavigation
                //    | SystemUiFlags.HideNavigation | SystemUiFlags.HideNavigation | SystemUiFlags.ImmersiveSticky);//OCULTA EL NAVIGAITON BAR
            //}
        }

        public void ShowToast(string text)
        {
            (Toast.MakeText(Application.Context, text, ToastLength.Short)).Show();
        }

        public async Task<Geoposicion> ObtenerGeoposicion(bool precision)
        {
            try
            {
                var request = new GeolocationRequest(precision ? GeolocationAccuracy.Best : GeolocationAccuracy.Best, TimeSpan.FromSeconds(0));
                var location = await Geolocation.GetLastKnownLocationAsync();
                if (location != null)
                {
                    location = await Geolocation.GetLocationAsync(request);
                    return new Geoposicion(location.Latitude, location.Longitude);
                }
                return new Geoposicion(0, 0);
            }
            catch (FeatureNotSupportedException)
            {
                return new Geoposicion(0, 0);
            }
            catch (PermissionException)
            {
                return new Geoposicion(0, 0);
            }
            catch (Exception)
            {
                return new Geoposicion(0, 0);
            }
        }
    }
}
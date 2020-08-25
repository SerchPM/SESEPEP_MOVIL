using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Essentials;

namespace MPS.AppCliente.iOS.OS
{
    public class OS : Sysne.Core.OS.IOS
    {
        public void HideNavigation(bool show)
        {
            //iOS no tiene barra de navegación
        }

        public void SetStatusBarColor(string color)
        {
            //TODO:Al parecer iOS no permite cambiar el StatusBar
        }
        const double LONG_DELAY = 1.5;


        NSTimer alertDelay;
        UIAlertController alert;

        public void ShowToast(string text)
        {
            ShowAlert(text, LONG_DELAY);
        }

        void ShowAlert(string text, double seconds)
        {
            alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
            {
                dismissText();
            });
            alert = UIAlertController.Create(null, text, UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        void dismissText()
        {
            if (alert != null)
            {
                alert.DismissViewController(true, null);
            }
            if (alertDelay != null)
            {
                alertDelay.Dispose();
            }
        }
    }
}
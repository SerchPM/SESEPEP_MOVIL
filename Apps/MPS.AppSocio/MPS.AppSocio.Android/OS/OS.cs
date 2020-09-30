﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Sysne.Core.OS;

namespace MPS.AppSocio.Droid.OS
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
    }
}
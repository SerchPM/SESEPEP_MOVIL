﻿using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Sysne.Core.OS;
using Android;

namespace MPS.AppSocio.Droid
{
    [Activity(Label = "MPS Socio", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            CheckAppPermissions();

            DependencyService.Register<OS.OS, IOS>();
            OS.OS.Activity = this;
            App.VersionName = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 0).VersionName;
            App.VersionCode = Application.Context.PackageManager.GetPackageInfo(Application.Context.PackageName, 0).VersionCode;
            Xamarin.Forms.Forms.SetFlags("Expander_Experimental");
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void CheckAppPermissions()
        {
            if ((int)Build.VERSION.SdkInt < 23)
                return;
            else
            {
                if (PackageManager.CheckPermission(Manifest.Permission.AccessCoarseLocation, PackageName) != Permission.Granted
                    && PackageManager.CheckPermission(Manifest.Permission.AccessFineLocation, PackageName) != Permission.Granted)
                {
                    var permissions = new string[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation };
                    RequestPermissions(permissions, 100);
                }
            }
        }
    }
}
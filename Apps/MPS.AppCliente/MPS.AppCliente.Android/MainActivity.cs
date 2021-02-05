using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Sysne.Core.OS;
using Android;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;

namespace MPS.AppCliente.Droid
{
    [Activity(Label = "MPS", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
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
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);           
            Xamarin.FormsMaps.Init(this, savedInstanceState);
            AppCenter.Start("c2b3df55-6a3f-4794-a855-cff174a622fa",
                  typeof(Analytics), typeof(Crashes));
            LoadApplication(new App());
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
                    var permissions = new string[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation};
                    RequestPermissions(permissions, 100);
                }
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
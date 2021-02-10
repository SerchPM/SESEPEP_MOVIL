using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Sysne.Core.OS;
using UIKit;

namespace MPS.AppSocio.iOS
{
    // The UIApplicationDelegate for the application. This class is responsible for launching the 
    // User Interface of the application, as well as listening (and optionally responding) to 
    // application events from iOS.
    [Register("AppDelegate")]
    public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate
    {
        //
        // This method is invoked when the application has loaded and is ready to run. In this 
        // method you should instantiate the window, load the UI into it and then make the window
        // visible.
        //
        // You have 17 seconds to return from this method, or iOS will terminate your application.
        //
        public override bool FinishedLaunching(UIApplication app, NSDictionary options)
        {
            App.VersionName = NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleShortVersionString")).ToString();
            int.TryParse(NSBundle.MainBundle.InfoDictionary.ValueForKey(new NSString("CFBundleVersion")).ToString(),out int versionCode);
            App.VersionCode = versionCode;
            DependencyService.Register<OS.OS, IOS>();
            global::Xamarin.Forms.Forms.Init();
            Xamarin.FormsMaps.Init();
            LoadApplication(new App());
            AppCenter.Start("31c9830a-3092-4d7d-af1d-25c510ae8b8e",
                       typeof(Analytics), typeof(Crashes));
            return base.FinishedLaunching(app, options);
        }
    }
}

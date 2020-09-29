using MPS.Core.Lib.OS;
using Sysne.Core.MVVM;
using Sysne.Core.OS;
using System;
using System.Runtime.CompilerServices;

namespace MPS.Core.Lib.Helpers
{
    public class Settings : ObservableObject
    {
        readonly ISettingsStorage settingsStorage;
        private Settings()
        {
            settingsStorage = DependencyService.Get<ISettingsStorage>();
            WebAPIUrl = "https://api.mpsmovil.com/api/";
        }

        [ThreadStatic]
        static Settings current;
        public static Settings Current => current ??= new Settings();

        private string webAPilUrl;
        public string WebAPIUrl { get => GetValue<string>(); set => SetValue(ref webAPilUrl, value); }

        void SetValue<T>(ref T field, T newValue = default, [CallerMemberName] string propertyName = null)
        {
            settingsStorage.SetValue(newValue, propertyName);
            Set(ref field, newValue, propertyName);
        }

        T GetValue<T>([CallerMemberName] string propertyName = null) =>
            settingsStorage.GetValue<T>(propertyName);
    }
}
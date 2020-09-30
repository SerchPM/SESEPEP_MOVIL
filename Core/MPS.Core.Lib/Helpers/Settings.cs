using MPS.Core.Lib.OS;
using MPS.SharedAPIModel.Seguridad;
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
        }

        [ThreadStatic]
        static Settings current;
        public static Settings Current => current ??= new Settings();

        private string webAPilUrl;
        public string WebAPIUrl { get => GetValue<string>(); set => SetValue(ref webAPilUrl, value); }

        private string usuario;
        public string Usuario { get => GetValue<string>(); set => SetValue(ref usuario, value); }

        private string contraseña;
        public string Contraseña { get => GetValue<string>(); set => SetValue(ref contraseña, value); }

        public LoginResponse LoginInfo { get; set; } = new LoginResponse();

        void SetValue<T>(ref T field, T newValue = default, [CallerMemberName] string propertyName = null)
        {
            settingsStorage.SetValue(newValue, propertyName);
            Set(ref field, newValue, propertyName);
        }

        T GetValue<T>([CallerMemberName] string propertyName = null) =>
            settingsStorage.GetValue<T>(propertyName);
    }
}
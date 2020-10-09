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

        private string modeloDispositivo;
        /// <summary>
        /// Obtiene o asigna el modelo del dispositivo
        /// </summary>
        public string ModeloDispositivo { get => GetValue<string>(); set => SetValue(ref modeloDispositivo, value); }

        public LoginResponse LoginInfo { get; set; } = new LoginResponse();

        void SetValue<T>(ref T field, T newValue = default, [CallerMemberName] string propertyName = null)
        {
            settingsStorage.SetValue(newValue, propertyName);
            Set(ref field, newValue, propertyName);
        }

        T GetValue<T>([CallerMemberName] string propertyName = null) =>
            settingsStorage.GetValue<T>(propertyName);

        private string estadoMonitorista;
        /// <summary>
        /// Obtiene o asigna el estado de la república donde se encuentra el cliente.
        /// </summary>
        public string Estado { get => GetValue<string>(); set => SetValue(ref estadoMonitorista, value); }

        private string paisMonitorista;
        /// <summary>
        /// Obtiene o asigna el país donde se encuentra el cliente.
        /// </summary>
        public string Pais { get => GetValue<string>(); set => SetValue(ref paisMonitorista, value); }

        private string mapServiceToken;
        /// <summary>
        /// Obtiene o asigna el token del mapa de Bing.
        /// </summary>
        public string MapServiceToken { get => GetValue<string>(mapServiceToken); set => SetValue(ref mapServiceToken, value); }

  
    }
}
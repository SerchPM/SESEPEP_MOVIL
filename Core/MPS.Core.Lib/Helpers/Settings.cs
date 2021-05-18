using MPS.Core.Lib.OS;
using MPS.SharedAPIModel.Clientes;
using MPS.SharedAPIModel.Notificaciones;
using MPS.SharedAPIModel.Seguridad;
using MPS.SharedAPIModel.Socios;
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

        private string tipoDispositivo;
        /// <summary>
        /// Obtiene o asigna el tipo del dispositivo
        /// </summary>
        public string TipoDispositivo { get => GetValue<string>(); set => SetValue(ref tipoDispositivo, value); }

        private string so;
        /// <summary>
        /// Obtiene o asigna el sistema operativo del dispositivo
        /// </summary>
        public string SO { get => GetValue<string>(); set => SetValue(ref so, value); }

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

        private string channelUriUWP;
        /// <summary>
        /// Obtiene o asigna el canal de comunicacion de WNS
        /// </summary>
        public string ChannelUriUWP { get => GetValue<string>(); set => SetValue(ref channelUriUWP, value); }

        public SolicitudServicio Solicitud { get; set; } = new SolicitudServicio();

        public ServicioSolicitado ServicioSolicitado { get; set; } = new ServicioSolicitado();

        public EstatusPago EstatusPago { get; set; } = new EstatusPago();

        private string appId;
        /// <summary>
        /// Obtiene o asigna el AppId del grupo de OneSignal
        /// </summary>
        public string AppId { get => GetValue<string>(); set => SetValue(ref appId, value); }
        
        private string paginaActual;
        public string PaginaActual { get => GetValue<string>(); set => SetValue(ref paginaActual, value); }

        private string playerId;
        public string PlayerId { get => GetValue<string>(); set => SetValue(ref playerId, value); }

        private string versionApp;
        public string VersionApp { get => GetValue<string>(); set => SetValue(ref versionApp, value); }
    }
}
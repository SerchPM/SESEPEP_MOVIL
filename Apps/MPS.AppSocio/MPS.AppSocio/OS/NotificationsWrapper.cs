using Com.OneSignal;
using MPS.AppSocio.Views.Views;
using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.Core.Lib.OS;
using MPS.SharedAPIModel;
using MPS.SharedAPIModel.Notificaciones;
using MPS.SharedAPIModel.Socios;
using Sysne.Core.OS;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.AppSocio.Views.OS
{
    public class NotificationsWrapper
    {
        /// <summary>
        /// Se lanza cuando se recibe una notificación, informa los datos adicionales de la notificación con el código de acción correspondiente
        /// </summary>
        public event EventHandler<MensajeSocio> NotificationReceived;

        private readonly OperacionesBL operacionesBL = null;

        public NotificationsWrapper()
        {
            operacionesBL = new OperacionesBL();
            Core.Lib.BL.SeguridadBL.Autentificado += (s, e) => Init();
        }

        /// <summary>
        /// Monitorea las notificaciones
        /// </summary>
        /// <seealso cref="https://documentation.onesignal.com/docs/xamarin-sdk#section--notificationreceived-"/>
        public void Init()
        {
            //Handle when your app starts
            OneSignal.Current.IdsAvailable(async (playerID, pushToken) =>
            {
                await operacionesBL.RegistrarDispositivoAsync(new Dispositivo
                {
                    Id = Guid.Parse(Settings.Current.LoginInfo.Usr.Id),
                    Modelo = Settings.Current.ModeloDispositivo,
                    SO = Settings.Current.SO,
                    TipoDispositivo = Settings.Current.TipoDispositivo,
                    TipoUsuario = (int)TipoUsuarioEnum.Socio,
                    VercionApp = !string.IsNullOrEmpty(Settings.Current.VersionApp) ? Settings.Current.VersionApp : "0",
                    TimeZona = "-28800",
                    PlayerId = playerID
                });
            });

            //Inicializa la subscripción
            OneSignal.Current.StartInit(MPS.Core.Lib.Helpers.AppSettingsManager.Settings["PushNotificationAppID"])
               .InFocusDisplaying(Com.OneSignal.Abstractions.OSInFocusDisplayOption.Notification)
               .HandleNotificationReceived((notification) =>
               {
                   var mensaje = new MensajeSocio(notification.payload.additionalData);
                   DelegarAccionDeNotificacion(mensaje);
               })
               .HandleNotificationOpened((notification) =>
               {
                   var mensaje = new MensajeSocio(notification.notification.payload.additionalData);
                   MPS.Core.Lib.Helpers.Settings.Current.Solicitud = new SharedAPIModel.Socios.SolicitudServicio 
                   {
                       Mensaje = mensaje.MensajePrincipal,
                       ClaveTipoServicio = mensaje.ClaveTipoServicio,
                       FechaSolicitud = mensaje.FechaSolicitud,
                       FolioSolicitud = mensaje.FolioSolicitud,
                       IdCliente = mensaje.IdCliente,
                       IdSolicitud = mensaje.IdSolicitud,
                       IdTipoSolicitud = mensaje.IdTipoSolicitud,
                       NombreCliente = mensaje.NombreCliente,
                       NombreServicio = mensaje.NombreServicio,
                       TipoServicio = mensaje.TipoServicio,
                       Ubicacion = mensaje.Ubicacion,
                       TipoNotificacion = mensaje.TipoNotificacion
                   };
               })
               .HandleInAppMessageClicked((notification) =>
               {
               }).EndInit();
        }

        public void DelegarAccionDeNotificacion(MensajeSocio mensaje)
        {
            NotificationReceived?.Invoke(this, mensaje);
            var paginaSolicitarServicio = typeof(SolicitarServicio);
            var paginaActual = App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1];

            if(paginaActual.GetType() == paginaSolicitarServicio)
            {
                var dtx = paginaActual.BindingContext as Core.Lib.ViewModels.Socios.SolicitarServicioViewModel;
                var servicio = new SolicitudServicio
                {
                    Mensaje = mensaje.MensajePrincipal,
                    ClaveTipoServicio = mensaje.ClaveTipoServicio,
                    FechaSolicitud = mensaje.FechaSolicitud,
                    FolioSolicitud = mensaje.FolioSolicitud,
                    IdCliente = mensaje.IdCliente,
                    IdSolicitud = mensaje.IdSolicitud,
                    IdTipoSolicitud = mensaje.IdTipoSolicitud,
                    NombreCliente = mensaje.NombreCliente,
                    NombreServicio = mensaje.NombreServicio,
                    TipoServicio = mensaje.TipoServicio,
                    Ubicacion = mensaje.Ubicacion,
                    TipoNotificacion = mensaje.TipoNotificacion
                };
                if (mensaje.TipoNotificacion.Equals((int)TipoNotificacionEnum.ClienteSolicita))
                    dtx.MostrarModalSolicitudCommand.Execute(servicio);
                else if (mensaje.TipoNotificacion.Equals((int)TipoNotificacionEnum.Alerta))
                    dtx.AbrirModalAlertaCommand.Execute(mensaje.MensajePrincipal);
            }
            else
            {
                MPS.Core.Lib.Helpers.Settings.Current.Solicitud = new SharedAPIModel.Socios.SolicitudServicio
                {
                    Mensaje = mensaje.MensajePrincipal,
                    ClaveTipoServicio = mensaje.ClaveTipoServicio,
                    FechaSolicitud = mensaje.FechaSolicitud,
                    FolioSolicitud = mensaje.FolioSolicitud,
                    IdCliente = mensaje.IdCliente,
                    IdSolicitud = mensaje.IdSolicitud,
                    IdTipoSolicitud = mensaje.IdTipoSolicitud,
                    NombreCliente = mensaje.NombreCliente,
                    NombreServicio = mensaje.NombreServicio,
                    TipoServicio = mensaje.TipoServicio,
                    Ubicacion = mensaje.Ubicacion,
                    TipoNotificacion = mensaje.TipoNotificacion
                };
            }
        }
    }
}

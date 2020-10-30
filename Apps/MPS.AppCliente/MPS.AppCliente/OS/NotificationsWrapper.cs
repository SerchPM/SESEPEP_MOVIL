using Com.OneSignal;
using MPS.SharedAPIModel;
using MPS.SharedAPIModel.Clientes;
using MPS.SharedAPIModel.Notificaciones;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.AppCliente.Views.OS
{
    public class NotificationsWrapper
    {
        public NotificationsWrapper()
        {
            Core.Lib.BL.SeguridadBL.Autentificado += (s, e) => Init();
        }

        /// <summary>
        /// Se lanza cuando se recibe una notificación, informa los datos adicionales de la notificación con el código de acción correspondiente
        /// </summary>
        public event EventHandler<MensajeCliente> NotificationReceived;

        /// <summary>
        /// Monitorea las notificaciones
        /// </summary>
        /// <seealso cref="https://documentation.onesignal.com/docs/xamarin-sdk#section--notificationreceived-"/>
        public void Init()
        {
            //Handle when your app starts
            OneSignal.Current.IdsAvailable(async (playerID, pushToken) =>
            {
            });

            //Inicializa la subscripción
            OneSignal.Current.StartInit(MPS.Core.Lib.Helpers.AppSettingsManager.Settings["PushNotificationAppID"])
               .InFocusDisplaying(Com.OneSignal.Abstractions.OSInFocusDisplayOption.Notification)
               .HandleNotificationReceived((notification) =>
               {
                   var mensaje = new MensajeCliente(notification.payload.additionalData);
                   DelegarAccionDeNotificacion(mensaje);
               }).HandleNotificationOpened((notification) =>
               {
                   var mensaje = new MensajeCliente(notification.notification.payload.additionalData);
                   MPS.Core.Lib.Helpers.Settings.Current.ServicioSolicitado = new ServicioSolicitado
                   {
                       ActualLat = mensaje.ActualLat,
                       CalificacionSocio = mensaje.CalificacionSocio,
                       ClaveTipoServicio = mensaje.ClaveTipoServicio,
                       FechaSolicitud = mensaje.FechaSolicitud,
                       FolioSolicitud = mensaje.FolioSolicitud,
                       IdSocio = mensaje.IdSocio,
                       IdSolicitud = mensaje.IdSolicitud,
                       IdTipoSolicitud = mensaje.IdTipoSolicitud,
                       NombreServicio = mensaje.NombreServicio,
                       NombreSocio = mensaje.NombreSocio,
                       TipoServicio = mensaje.TipoServicio
                   };
               })
               .HandleInAppMessageClicked((notification) =>
               {
               }).EndInit();
        }

        private void DelegarAccionDeNotificacion(MensajeCliente mensaje)
        {
            NotificationReceived?.Invoke(this, mensaje);
            var paginaSolicitarServicio = typeof(SolicitarServicio);
            var paginaActual = App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1];

            if (paginaActual.GetType() == paginaSolicitarServicio)
            {
                var dtx = paginaActual.BindingContext as Core.Lib.ViewModels.Clientes.SolicitudDeServicioViewModel;
                var solicitudAceptada = new ServicioSolicitado
                {
                    ActualLat = mensaje.ActualLat,
                    CalificacionSocio = mensaje.CalificacionSocio,
                    ClaveTipoServicio = mensaje.ClaveTipoServicio,
                    FechaSolicitud = mensaje.FechaSolicitud,
                    FolioSolicitud = mensaje.FolioSolicitud,
                    IdSocio = mensaje.IdSocio,
                    IdSolicitud = mensaje.IdSolicitud,
                    IdTipoSolicitud = mensaje.IdTipoSolicitud,
                    NombreServicio = mensaje.NombreServicio,
                    NombreSocio = mensaje.NombreSocio,
                    TipoServicio = mensaje.TipoServicio
                };
                dtx.MostrarModalSolicitudCommand.Execute(solicitudAceptada);
            }
            else
            {
                MPS.Core.Lib.Helpers.Settings.Current.ServicioSolicitado = new ServicioSolicitado
                {
                    ActualLat = mensaje.ActualLat,
                    CalificacionSocio = mensaje.CalificacionSocio,
                    ClaveTipoServicio = mensaje.ClaveTipoServicio,
                    FechaSolicitud = mensaje.FechaSolicitud,
                    FolioSolicitud = mensaje.FolioSolicitud,
                    IdSocio = mensaje.IdSocio,
                    IdSolicitud = mensaje.IdSolicitud,
                    IdTipoSolicitud = mensaje.IdTipoSolicitud,
                    NombreServicio = mensaje.NombreServicio,
                    NombreSocio = mensaje.NombreSocio,
                    TipoServicio = mensaje.TipoServicio
                };
            }
        }
    }
}

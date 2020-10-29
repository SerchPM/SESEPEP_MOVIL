using Com.OneSignal;
using MPS.AppSocio.Views.Views;
using MPS.Core.Lib.OS;
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
        public NotificationsWrapper()
        {
            Core.Lib.BL.SeguridadBL.Autentificado += (s, e) => Init();
        }

        /// <summary>
        /// Se lanza cuando se recibe una notificación, informa los datos adicionales de la notificación con el código de acción correspondiente
        /// </summary>
        public event EventHandler<MensajeSocio> NotificationReceived;

        /// <summary>
        /// Monitorea las notificaciones
        /// </summary>
        /// <seealso cref="https://documentation.onesignal.com/docs/xamarin-sdk#section--notificationreceived-"/>
        public void Init()
        {
            //Handle when your app starts
            OneSignal.Current.IdsAvailable(async (playerID, pushToken) =>
            {
                var r = playerID;
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
                       ClaveTipoServicio = mensaje.ClaveTipoServicio,
                       FechaSolicitud = mensaje.FechaSolicitud,
                       FolioSolicitud = mensaje.FolioSolicitud,
                       IdCliente = mensaje.IdCliente,
                       IdSolicitud = mensaje.IdSolicitud,
                       IdTipoSolicitud = mensaje.IdTipoSolicitud,
                       NombreCliente = mensaje.NombreCliente,
                       NombreServicio = mensaje.NombreServicio,
                       TipoServicio = mensaje.TipoServicio,
                       Ubicacion = mensaje.Ubicacion
                   };
               })
               .HandleInAppMessageClicked((notification) =>
               {
               }).EndInit();
        }

        private void DelegarAccionDeNotificacion(MensajeSocio mensaje)
        {
            NotificationReceived?.Invoke(this, mensaje);
            var paginaSolicitarServicio = typeof(SolicitarServicio);
            var paginaActual = App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1];

            if(paginaActual.GetType() == paginaSolicitarServicio)
            {
                var dtx = paginaActual.BindingContext as Core.Lib.ViewModels.Socios.SolicitarServicioViewModel;
                var servicio = new SolicitudServicio
                {
                    ClaveTipoServicio = mensaje.ClaveTipoServicio,
                    FechaSolicitud = mensaje.FechaSolicitud,
                    FolioSolicitud = mensaje.FolioSolicitud,
                    IdCliente = mensaje.IdCliente,
                    IdSolicitud = mensaje.IdSolicitud,
                    IdTipoSolicitud = mensaje.IdTipoSolicitud,
                    NombreCliente = mensaje.NombreCliente,
                    NombreServicio = mensaje.NombreServicio,
                    TipoServicio = mensaje.TipoServicio,
                    Ubicacion = mensaje.Ubicacion
                };
                dtx.MostrarModalSolicitudCommand.Execute(servicio);
            }
            else
            {
                MPS.Core.Lib.Helpers.Settings.Current.Solicitud = new SharedAPIModel.Socios.SolicitudServicio
                {
                    ClaveTipoServicio = mensaje.ClaveTipoServicio,
                    FechaSolicitud = mensaje.FechaSolicitud,
                    FolioSolicitud = mensaje.FolioSolicitud,
                    IdCliente = mensaje.IdCliente,
                    IdSolicitud = mensaje.IdSolicitud,
                    IdTipoSolicitud = mensaje.IdTipoSolicitud,
                    NombreCliente = mensaje.NombreCliente,
                    NombreServicio = mensaje.NombreServicio,
                    TipoServicio = mensaje.TipoServicio,
                    Ubicacion = mensaje.Ubicacion
                };
            }
        }
    }
}

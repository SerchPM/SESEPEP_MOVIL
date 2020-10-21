using Com.OneSignal;
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
        public event EventHandler<bool> NotificationReceived;

        /// <summary>
        /// Monitorea las notificaciones
        /// </summary>
        /// <seealso cref="https://documentation.onesignal.com/docs/xamarin-sdk#section--notificationreceived-"/>
        public void Init()
        {
            //Inicializa la subscripción
            OneSignal.Current.StartInit(MPS.Core.Lib.Helpers.AppSettingsManager.Settings["PushNotificationAppID"])
               .InFocusDisplaying(Com.OneSignal.Abstractions.OSInFocusDisplayOption.Notification)
               .HandleNotificationReceived((notification) =>
               {
                   //var mensaje = new Mensaje(notification.payload.additionalData);
                   DelegarAccionDeNotificacion(true);
               }).EndInit();
        }

        private async void DelegarAccionDeNotificacion(bool mensaje)
        {
            NotificationReceived?.Invoke(this, mensaje);
        }
    }
}

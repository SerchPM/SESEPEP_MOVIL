using Com.OneSignal;
using MPS.SharedAPIModel;
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
        public event EventHandler<Mensaje> NotificationReceived;

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
                   var mensaje = new Mensaje(notification.payload.additionalData);
                   DelegarAccionDeNotificacion(mensaje);
               }).EndInit();
        }

        private async void DelegarAccionDeNotificacion(Mensaje mensaje)
        {
            NotificationReceived?.Invoke(this, mensaje);
        }
    }
}

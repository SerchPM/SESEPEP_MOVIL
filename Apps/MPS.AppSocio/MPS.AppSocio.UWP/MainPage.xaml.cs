using MPS.SharedAPIModel.Notificaciones;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.PushNotifications;
using Windows.Security.ExchangeActiveSyncProvisioning;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace MPS.AppSocio.UWP
{
    public sealed partial class MainPage
    {
        PushNotificationChannel channel;

        Views.OS.NotificationsWrapper notifications;
        Views.OS.NotificationsWrapper Notificaciones => notifications ??= new Views.OS.NotificationsWrapper();
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new MPS.AppSocio.App());
            CreateChannel();
        }

        private async void CreateChannel()
        {
            channel = await PushNotificationChannelManager.CreatePushNotificationChannelForApplicationAsync();
            Core.Lib.Helpers.Settings.Current.ChannelUriUWP = channel.Uri;
            Core.Lib.Helpers.Settings.Current.AppId = MPS.Core.Lib.Helpers.AppSettingsManager.Settings["PushNotificationAppID"];
            channel.PushNotificationReceived += (s, e) =>
            {
                Dictionary<string, object> dictionaryMessage = new Dictionary<string, object>();
                string data = string.Empty;
                if (e.ToastNotification.Content != null && e.ToastNotification.Content.DocumentElement != null)
                    data = e.ToastNotification.Content.DocumentElement.Attributes.FirstOrDefault().InnerText ?? string.Empty;
                var mensaje = JsonConvert.DeserializeObject<RootC>(data);
                dictionaryMessage.Add("MensajePrincipal", mensaje.custom.a.MensajePrincipal);
                dictionaryMessage.Add("CLAVE_TIPO_SERVICIO", mensaje.custom.a.ClaveTipoServicio);
                dictionaryMessage.Add("FECHA_SOLICITUD", mensaje.custom.a.FechaSolicitud);
                dictionaryMessage.Add("FOLIO_SOLICITUD", mensaje.custom.a.FolioSolicitud);
                dictionaryMessage.Add("GUID_CLIENTE", mensaje.custom.a.IdCliente);
                dictionaryMessage.Add("GUID_SOLICITUD", mensaje.custom.a.IdSolicitud);
                dictionaryMessage.Add("GUID_TIPO_SOLICITUD", mensaje.custom.a.IdTipoSolicitud);
                dictionaryMessage.Add("NOMBRE_CLIENTE", mensaje.custom.a.NombreCliente);
                dictionaryMessage.Add("NOMBRE_SERVICIO", mensaje.custom.a.NombreServicio);
                dictionaryMessage.Add("TIPO_SERVICIO", mensaje.custom.a.TipoServicio);
                dictionaryMessage.Add("UBICACION_1", mensaje.custom.a.Ubicacion);
                dictionaryMessage.Add("TIPO_NOTIFICACION", mensaje.custom.a.TipoNotificacion);
                Notificaciones.DelegarAccionDeNotificacion(new MensajeSocio(dictionaryMessage));
            };
        }
    }
}

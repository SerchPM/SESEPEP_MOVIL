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

namespace MPS.AppCliente.UWP
{
    public sealed partial class MainPage
    {
        PushNotificationChannel channel;

        Views.OS.NotificationsWrapper notifications;
        Views.OS.NotificationsWrapper Notificaciones => notifications ??= new Views.OS.NotificationsWrapper();
        public MainPage()
        {
            this.InitializeComponent();
            LoadApplication(new MPS.AppCliente.App());
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
                var mensaje = JsonConvert.DeserializeObject<Root>(data);
                dictionaryMessage.Add("MensajePrincipal", mensaje.custom.a.MensajePrincipal);
                dictionaryMessage.Add("ACTUAL_LAT", mensaje.custom.a.ActualLat);
                dictionaryMessage.Add("CALIFICACION_SOCIO", mensaje.custom.a.CalificacionSocio);
                dictionaryMessage.Add("CLAVE_TIPO_SERVICIO", mensaje.custom.a.TipoServicio);
                dictionaryMessage.Add("FECHA_SOLICITUD", mensaje.custom.a.FechaSolicitud);
                dictionaryMessage.Add("FOLIO_SOLICITUD", mensaje.custom.a.FolioSolicitud);
                dictionaryMessage.Add("GUID_SOCIO", mensaje.custom.a.IdSocio);
                dictionaryMessage.Add("GUID_SOLICITUD", mensaje.custom.a.IdSolicitud);
                dictionaryMessage.Add("GUID_TIPO_SOLICITUD", mensaje.custom.a.IdTipoSolicitud);
                dictionaryMessage.Add("NOMBRE_SERVICIO", mensaje.custom.a.NombreServicio);
                dictionaryMessage.Add("NOMBRE_SOCIO", mensaje.custom.a.NombreSocio);
                dictionaryMessage.Add("TIPO_SERVICIO", mensaje.custom.a.TipoServicio);
                dictionaryMessage.Add("TIPO_NOTIFICACION", mensaje.custom.a.TipoNotificacion);
                dictionaryMessage.Add("MONTO", mensaje.custom.a.Monto);
                dictionaryMessage.Add("NO_AUTORIZACION", mensaje.custom.a.NoAutorizacion);
                dictionaryMessage.Add("BANCO", mensaje.custom.a.Banco);
                dictionaryMessage.Add("NO_TARJETA", mensaje.custom.a.NoTarjeta);
                dictionaryMessage.Add("DESCRIPCION", mensaje.custom.a.Descripcion);
                dictionaryMessage.Add("CODIGO_OPERACION", mensaje.custom.a.Codigo);
                dictionaryMessage.Add("STATUS", mensaje.custom.a.Status);
                Notificaciones.DelegarAccionDeNotificacion(new MensajeCliente(dictionaryMessage));
            };
        }
    }
}

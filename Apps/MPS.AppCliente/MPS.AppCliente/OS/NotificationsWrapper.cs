using Com.OneSignal;
using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
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
        /// <summary>
        /// Se lanza cuando se recibe una notificación, informa los datos adicionales de la notificación con el código de acción correspondiente
        /// </summary>
        public event EventHandler<MensajeCliente> NotificationReceived;

        private readonly OperacionesBL operacionesBL = null;

        public NotificationsWrapper()
        {
            operacionesBL = new OperacionesBL();
            Core.Lib.ViewModels.Clientes.RegistroViewModel.RegistroCliente += (s, e) => InitRegistro();
            Core.Lib.BL.SeguridadBL.Autentificado += (s, e) => Init();
        }

        /// <summary>
        /// Metodo para registro de un nuevo cliente
        /// </summary>
        public void InitRegistro()
        {
            OneSignal.Current.IdsAvailable((playerID, pushToken) =>
            {
                Settings.Current.PlayerId = playerID;
            });

            //Inicializa la subscripción
            OneSignal.Current.StartInit(MPS.Core.Lib.Helpers.AppSettingsManager.Settings["PushNotificationAppID"])
               .InFocusDisplaying(Com.OneSignal.Abstractions.OSInFocusDisplayOption.Notification)
               .HandleNotificationReceived((notification) =>
               {
                 
               }).HandleNotificationOpened((notification) =>
               {
                  
               })
               .HandleInAppMessageClicked((notification) =>
               {
               }).EndInit();
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
                    TipoUsuario = (int)TipoUsuarioEnum.Cliente,
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
                       TipoServicio = mensaje.TipoServicio,
                       TipoNotificacion = mensaje.TipoNotificacion
                   };
               })
               .HandleInAppMessageClicked((notification) =>
               {
               }).EndInit();
        }

        public void DelegarAccionDeNotificacion(MensajeCliente mensaje)
        {
            NotificationReceived?.Invoke(this, mensaje);
            var paginaSolicitarServicio = typeof(SolicitarServicio);
            var paginaActual = App.Current.MainPage.Navigation.NavigationStack[App.Current.MainPage.Navigation.NavigationStack.Count - 1];

            if (paginaActual.GetType() == paginaSolicitarServicio)
            {
                var dtx = paginaActual.BindingContext as Core.Lib.ViewModels.Clientes.SolicitudDeServicioViewModel;
                if (mensaje.TipoNotificacion.Equals((int)TipoNotificacionEnum.SocioAcepta))
                {
                    dtx.MostrarModalSolicitudCommand.Execute(new ServicioSolicitado
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
                    });
                }
                else if (mensaje.TipoNotificacion.Equals((int)TipoNotificacionEnum.Finalizado))
                    dtx.AbrirModalCalificarCommand.Execute(mensaje.IdSolicitud);   
                else if (mensaje.TipoNotificacion.Equals((int)TipoNotificacionEnum.EstatusPago))
                {
                    dtx.MostrarModalEstatusPagoCommand.Execute(new EstatusPago
                    {
                        Banco = mensaje.Banco,
                        NoAutorizacion = mensaje.NoAutorizacion,
                        NoTarjeta = mensaje.NoTarjeta,
                        Monto = mensaje.Monto,
                        Descripcion = mensaje.Descripcion,
                        ClaveTipoServicio = mensaje.ClaveTipoServicio,
                        NombreServicio = mensaje.NombreServicio,
                        Codigo = mensaje.Codigo,
                        Status = mensaje.Status
                    });
                }
            }
            else
            {
                if (mensaje.TipoNotificacion.Equals((int)TipoNotificacionEnum.SocioAcepta) || mensaje.TipoNotificacion.Equals((int)TipoNotificacionEnum.Finalizado))
                {
                    Settings.Current.ServicioSolicitado = new ServicioSolicitado
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
                else if (mensaje.TipoNotificacion.Equals((int)TipoNotificacionEnum.EstatusPago))
                {
                    Settings.Current.EstatusPago = new EstatusPago
                    {
                        Banco = mensaje.Banco,
                        NoAutorizacion = mensaje.NoAutorizacion,
                        NoTarjeta = mensaje.NoTarjeta,
                        Monto = mensaje.Monto,
                        Descripcion = mensaje.Descripcion,
                        ClaveTipoServicio = mensaje.ClaveTipoServicio,
                        NombreServicio = mensaje.NombreServicio,
                        Codigo = mensaje.Codigo,
                        Status = mensaje.Status
                    };
                }
            }
        }
    }
}

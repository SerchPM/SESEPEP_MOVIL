using MPS.Core.Lib.ApiClient;
using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel;
using MPS.SharedAPIModel.Operaciones;
using MPS.SharedAPIModel.Socios;
using MPS.SharedAPIModel.Solicitud;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.Core.Lib.ViewModels.Socios
{
    public class SolicitarServicioViewModel : ViewModelWithBL<SociosBL>
    {
        public SolicitarServicioViewModel()
        {
            Id = Guid.Parse(Settings.Current.LoginInfo.Usr.Id);
        }

        MapBL mapaBL;
        MapBL MapaBL => mapaBL ??= new MapBL();

        readonly SolicitudBL solicitudBL = null;
        SolicitudBL SolicitudBL => solicitudBL ?? new SolicitudBL();

        #region Eventos
        public event EventHandler<UbicacionActualEvent> ObteniendoUbicacion;
        #endregion

        #region Propiedades
        string solicitud;
        public string Solicitud { get => solicitud; set { Set(ref solicitud, value); } }

        private Guid id;
        public Guid Id { get => id; set { Set(ref id, value); } }

        bool modal;
        public bool Modal { get => modal; set { Set(ref modal, value); } }

        string mensaje;
        public string Mensaje { get => mensaje; set { Set(ref mensaje, value); } }

        private string nombreUbicacion;
        public string NombreUbicacion { get => nombreUbicacion; set => Set(ref nombreUbicacion, value); }

        private SolicitudServicio solicitudDeServicio;
        public SolicitudServicio SolicitudDeServicio { get => solicitudDeServicio; set => Set(ref solicitudDeServicio, value); }

        private bool modalServicioAsignado;
        public bool ModalServicioAsignado { get => modalServicioAsignado; set => Set(ref modalServicioAsignado, value); }

        private bool express;
        public bool Express { get => express; set => Set(ref express, value); }

        private bool personalizada;
        public bool Personalizada { get => personalizada; set => Set(ref personalizada, value); }

        private SolicitudPendiente servicioAtencion;
        public SolicitudPendiente ServicioAtencion { get => servicioAtencion; set => Set(ref servicioAtencion, value); }

        private bool enAtencion;
        public bool EnAtencion { get => enAtencion; set => Set(ref enAtencion, value); }

        private string estatusServicio;
        public string EstatusServicio { get => estatusServicio; set => Set(ref estatusServicio, value); }

        private bool alertaActiva;
        public bool AlertaActiva { get => alertaActiva; set => Set(ref alertaActiva, value); }
        #endregion

        #region Commandos
        private RelayCommand obtenerComponentesCommand = null;
        public RelayCommand ObtenerComponentesCommand
        {
            get => obtenerComponentesCommand ??= new RelayCommand(async () =>
            {
                var (estatus, result) = await bl.ServicioEnAtencionAysnc(Id);
                if (estatus)
                {
                    AlertaActiva = !result.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso);
                    EstatusServicio = result.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.Alerta) ? "soson.png" : "sosoff.png";
                    Solicitud = result.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso) ? "Iniciar" : "Finalizar";
                    ServicioAtencion = result;
                    var (latitud, longitud) = Utilidades.LimpiarCadenaUbicacion(ServicioAtencion.UBICACION_SERVICIO);
                    ObtenerDireccionCommand.Execute((latitud, longitud));
                    EnAtencion = true;
                }
                else
                {
                    var ubicacion = await Sysne.Core.OS.DependencyService.Get<Sysne.Core.OS.IOS>().ObtenerGeoposicion(false);
                    if (ObteniendoUbicacion != null && ubicacion != null)
                    {
                        UbicacionActualEvent args = new UbicacionActualEvent
                        {
                            Geoposicion = ubicacion
                        };
                        ObteniendoUbicacion(this, args);
                    }
                }
            });
        }

        private RelayCommand ocultarModalCommand = null;
        public RelayCommand OcultarModalCommand
        {
            get => ocultarModalCommand ??= new RelayCommand(() =>
            {
                Modal = false;
                Mensaje = string.Empty;
            });
        }

        RelayCommand<(double, double)> obtenerDireccionCommand = null;
        public RelayCommand<(double, double)> ObtenerDireccionCommand
        {
            get => obtenerDireccionCommand ??= new RelayCommand<(double, double)>(async (ubicacion) =>
            {
                var geoposicion = new Geoposicion(ubicacion.Item1, ubicacion.Item2);
                if (ObteniendoUbicacion != null && geoposicion != null )
                {
                    var result = await MapaBL.ObtenerDireccion(geoposicion.Latitud.Value, geoposicion.Longitud.Value);
                    NombreUbicacion = result ?? string.Empty;
                    UbicacionActualEvent args = new UbicacionActualEvent
                    {
                        Geoposicion = geoposicion
                    };
                    ObteniendoUbicacion(this, args);
                }
            });
        }

        RelayCommand finalizaOrIniciarServicioCommand = null;
        public RelayCommand FinalizaOrIniciarServicioCommand
        {
            get => finalizaOrIniciarServicioCommand ??= new RelayCommand(async () =>
            {
                await (new SolicitudBL()).AsignarSocioAsync(new SocioAsignado
                {
                    IdSocio = Id,
                    IdSolicitud = ServicioAtencion.GUID_SOLICITUD,
                    Estatus = ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso) ? (int)EstatusSolicitudEnum.Atendiendo : (int)EstatusSolicitudEnum.Finalizado
                });

                if (ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.Atendiendo) || ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.Alerta))
                {
                    var ubicacion = await Sysne.Core.OS.DependencyService.Get<Sysne.Core.OS.IOS>().ObtenerGeoposicion(false);
                    if (ObteniendoUbicacion != null && ubicacion != null)
                    {
                        UbicacionActualEvent args = new UbicacionActualEvent
                        {
                            Geoposicion = ubicacion
                        };
                        ObteniendoUbicacion(this, args);
                    }
                    EnAtencion = false;
                    AlertaActiva = false;
                    ServicioAtencion = new SolicitudPendiente();
                    NombreUbicacion = string.Empty;
                }
                else
                {
                    ServicioAtencion.ESTATUS_SOLICITUD = (int)EstatusSolicitudEnum.Atendiendo;
                    Solicitud = ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso) ? "Iniciar" : "Finalizar";
                    AlertaActiva = !ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso);
                }

            });
        }

        RelayCommand tomarSolicitudCommand = null;
        public RelayCommand TomarSolicitudCommand
        {
            get => tomarSolicitudCommand ??= new RelayCommand(async () =>
            {
                var resultado = await SolicitudBL.AsignarSocioAsync(new SocioAsignado
                {
                    IdSocio = Id,
                    IdSolicitud = SolicitudDeServicio.IdSolicitud,
                    Estatus = SolicitudDeServicio.ClaveTipoServicio.Equals((int)TipoSolicitudEnum.Express) ? (int)EstatusSolicitudEnum.EnCurso : (int)EstatusSolicitudEnum.Aceptado
                });

                if (resultado)
                {
                    Settings.Current.Solicitud = new SolicitudServicio();
                    ModalServicioAsignado = false;
                    SolicitudDeServicio = new SolicitudServicio();
                   

                    var (estatus, result) = await bl.ServicioEnAtencionAysnc(Id);
                    if (estatus)
                    {
                        if (Express)
                        {
                            EstatusServicio = result.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.Alerta) ? "soson.png" : "sosoff.png";
                            Solicitud = result.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso) ? "Iniciar" : "Finalizar";
                            ServicioAtencion = result;
                            var (latitud, longitud) = Utilidades.LimpiarCadenaUbicacion(ServicioAtencion.UBICACION_SERVICIO);
                            ObtenerDireccionCommand.Execute((latitud, longitud));
                            EnAtencion = true;
                        }
                    }
                    Express = false;
                    Personalizada = false;
                }
                else
                {
                    Settings.Current.Solicitud = new SolicitudServicio();
                    ModalServicioAsignado = false;
                    SolicitudDeServicio = new SolicitudServicio();
                    Express = false;
                    Personalizada = false; Mensaje = "Ya hay un socio asignado al servicio";
                    Modal = true;
                }
            });
        }

        private RelayCommand rechazarSolicitudCommand = null;
        public RelayCommand RechazarSolicitudCommand
        {
            get => rechazarSolicitudCommand ??= new RelayCommand(() =>
            {
                ModalServicioAsignado = false;
                SolicitudDeServicio = new SolicitudServicio();
                Settings.Current.Solicitud = new SolicitudServicio();
                Express = false;
                Personalizada = false;
            });
        }

        private RelayCommand<SolicitudServicio> mostrarModalSolicitudCommand = null;
        public RelayCommand<SolicitudServicio> MostrarModalSolicitudCommand
        {
            get => mostrarModalSolicitudCommand ??= new RelayCommand<SolicitudServicio>((servicio) =>
            {
                if (servicio.ClaveTipoServicio.Equals((int)TipoSolicitudEnum.Express))
                    Express = true;
                else
                    Personalizada = true;
                SolicitudDeServicio = servicio;
                ModalServicioAsignado = true;
            });
        }

        private RelayCommand verificarSolicitudCommand = null;
        public RelayCommand VerificarSolicitudCommand
        {
            get => verificarSolicitudCommand ??= new RelayCommand(() =>
            {
                if (Settings.Current.Solicitud != null && !string.IsNullOrEmpty(Settings.Current.Solicitud.FolioSolicitud))
                {
                    SolicitudDeServicio = Settings.Current.Solicitud;
                    if (SolicitudDeServicio.ClaveTipoServicio.Equals(1))
                        Express = true;
                    else
                        Personalizada = true;
                    ModalServicioAsignado = true;
                }
            });
        }

        private RelayCommand alertaCommand = null;
        public RelayCommand AlertaCommand
        {
            get => alertaCommand ??= new RelayCommand(async () =>
            {
                bool resultado = false;
                if (!ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.Alerta) && !ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso))
                {
                    resultado = await SolicitudBL.AsignarSocioAsync(new SocioAsignado
                    {
                        IdSocio = Id,
                        IdSolicitud = ServicioAtencion.GUID_SOLICITUD,
                        Estatus = (int)EstatusSolicitudEnum.Alerta
                    });
                    EstatusServicio = "soson.png";
                }
            });
        }

        private RelayCommand registrarUbicacionCommand = null;
        public RelayCommand RegistrarUbicacionCommand
        {
            get => registrarUbicacionCommand ??= new RelayCommand(async () =>
            {
                var ubicacion = await Sysne.Core.OS.DependencyService.Get<Sysne.Core.OS.IOS>().ObtenerGeoposicion(false);
                if(ubicacion != null && ServicioAtencion != null && ServicioAtencion.GUID_SOLICITUD != Guid.Empty)
                    await (new SolicitudBL()).MandarUbicacionAsync(ServicioAtencion.GUID_SOLICITUD, ubicacion.Latitud.ToString(), ubicacion.Longitud.ToString(), ubicacion.Latitud.ToString(), ubicacion.Longitud.ToString());
            });
        }
        #endregion
    }
    public class UbicacionActualEvent : EventArgs
    {
        public Geoposicion Geoposicion { get; set; }
    }
}

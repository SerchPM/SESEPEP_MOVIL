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
using System.Collections.ObjectModel;
using System.Linq;
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

        private SolicitudServicio solicitudDeServicio = new SolicitudServicio();
        public SolicitudServicio SolicitudDeServicio { get => solicitudDeServicio; set => Set(ref solicitudDeServicio, value); }

        private bool modalServicioAsignado;
        public bool ModalServicioAsignado { get => modalServicioAsignado; set => Set(ref modalServicioAsignado, value); }

        private bool express;
        public bool Express { get => express; set => Set(ref express, value); }

        private bool personalizada;
        public bool Personalizada { get => personalizada; set => Set(ref personalizada, value); }

        private SolicitudPendiente servicioAtencion = new SolicitudPendiente();
        public SolicitudPendiente ServicioAtencion { get => servicioAtencion; set => Set(ref servicioAtencion, value); }

        private bool enAtencion;
        public bool EnAtencion { get => enAtencion; set => Set(ref enAtencion, value); }

        private string estatusServicio;
        public string EstatusServicio { get => estatusServicio; set => Set(ref estatusServicio, value); }

        private bool alertaActiva;
        public bool AlertaActiva { get => alertaActiva; set => Set(ref alertaActiva, value); }

        private ObservableCollection<Ranking> rankings;
        public ObservableCollection<Ranking> Rankings { get => rankings; set => Set(ref rankings, value); }

        private string observaciones;
        public string Observaciones { get => observaciones; set => Set(ref observaciones, value); }

        private bool modalCalificar;
        public bool ModalCalificar { get => modalCalificar; set => Set(ref modalCalificar, value); }

        private bool modalAlerta;
        public bool ModalAlerta { get => modalAlerta; set => Set(ref modalAlerta, value); }

        private string mensajeAlerta;
        public string MensajeAlerta { get => mensajeAlerta; set => Set(ref mensajeAlerta, value); }

        private bool iconUbicacion;
        public bool IconUbicacion { get => iconUbicacion; set => Set(ref iconUbicacion, value); }
        #endregion

        #region Commandos
        private RelayCommand obtenerComponentesCommand = null;
        public RelayCommand ObtenerComponentesCommand
        {
            get => obtenerComponentesCommand ??= new RelayCommand(async () =>
            {
                var rank = new List<Ranking>();
                for (int i = 1; i <= 5; i++)
                {
                    if (i.Equals(1))
                        rank.Add(new Ranking { Rank = i, Imagen = "estrellaon.png", Selected = false });
                    else
                        rank.Add(new Ranking { Rank = i, Imagen = "estrellaoff.png", Selected = false });
                }
                Rankings = new ObservableCollection<Ranking>(rank);

                var (estatus, result) = await bl.ServicioEnAtencionAysnc(Id);
                if (estatus)
                {
                    IconUbicacion = result.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso);
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
                if (ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.Atendiendo) || ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.Alerta))
                    ModalCalificar = true;
                else
                {
                    await (new SolicitudBL()).AsignarSocioAsync(new SocioAsignado
                    {
                        IdSocio = Id,
                        IdSolicitud = ServicioAtencion.GUID_SOLICITUD,
                        Estatus = ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso) ? (int)EstatusSolicitudEnum.Atendiendo : (int)EstatusSolicitudEnum.Finalizado
                    });
                    ServicioAtencion.ESTATUS_SOLICITUD = (int)EstatusSolicitudEnum.Atendiendo;
                    Solicitud = ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso) ? "Iniciar" : "Finalizar";
                    AlertaActiva = !ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso);
                    IconUbicacion = ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso);
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
                            IconUbicacion = result.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso);
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
            get => mostrarModalSolicitudCommand ??= new RelayCommand<SolicitudServicio>(async(servicio) =>
            {
                if (servicio.ClaveTipoServicio.Equals((int)TipoSolicitudEnum.Express))
                    Express = true;
                else
                    Personalizada = true;
                SolicitudDeServicio = servicio;
                var (latitud, longitud) = Utilidades.LimpiarCadenaUbicacion(SolicitudDeServicio.Ubicacion);
                NombreUbicacion = await MapaBL.ObtenerDireccion(latitud, longitud);
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
                    if (Settings.Current.Solicitud.TipoNotificacion.Equals((int)TipoNotificacionEnum.ClienteSolicita))
                        MostrarModalSolicitudCommand.Execute(Settings.Current.Solicitud);
                    else if (Settings.Current.Solicitud.TipoNotificacion.Equals((int)TipoNotificacionEnum.Alerta))
                        AbrirModalAlertaCommand.Execute(Settings.Current.Solicitud.Mensaje);
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
                    ServicioAtencion.ESTATUS_SOLICITUD = (int)EstatusSolicitudEnum.Alerta;
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

        private RelayCommand enviarCalificacionCommand = null;
        public RelayCommand EnviarCalificacionCommand
        {
            get => enviarCalificacionCommand ??= new RelayCommand(async () =>
            {
                Observaciones ??= string.Empty;
                var result = await bl.CalificarClienteAysnc(ServicioAtencion.GUID_SOLICITUD, Rankings.Where(w => w.Selected).Count(), Observaciones);
                if (result)
                {
                    await (new SolicitudBL()).AsignarSocioAsync(new SocioAsignado
                    {
                        IdSocio = Id,
                        IdSolicitud = ServicioAtencion.GUID_SOLICITUD,
                        Estatus = ServicioAtencion.ESTATUS_SOLICITUD.Equals((int)EstatusSolicitudEnum.EnCurso) ? (int)EstatusSolicitudEnum.Atendiendo : (int)EstatusSolicitudEnum.Finalizado
                    });
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
                    IconUbicacion = false;
                    ServicioAtencion = new SolicitudPendiente();
                    NombreUbicacion = string.Empty;
                    ModalCalificar = false;
                }
                else
                {
                    ModalCalificar = false;
                    Mensaje = "Ocurrio un problema al finalizar el servicio";
                    Modal = true;
                }
                Observaciones = string.Empty;
                SelectRankingCommand.Execute(new Ranking { Rank = 1 });
            });
        }

        private RelayCommand ocultarModalCalificarCommand = null;
        public RelayCommand OcultarModalCalificarCommand
        {
            get => ocultarModalCalificarCommand ??= new RelayCommand(() =>
            {
                ModalCalificar = false;
                Observaciones = string.Empty;
                SelectRankingCommand.Execute(new Ranking { Rank = 1});
            });
        }

        private RelayCommand<Ranking> selectRankingCommand = null;
        public RelayCommand<Ranking> SelectRankingCommand
        {
            get => selectRankingCommand ??= new RelayCommand<Ranking>((ranking) =>
            {

                var rank = new List<Ranking>();
                for (int i = 1; i <= 5; i++)
                {
                    if (i <= ranking.Rank)
                        rank.Add(new Ranking { Rank = i, Imagen = "estrellaon.png", Selected = true });
                    else
                        rank.Add(new Ranking { Rank = i, Imagen = "estrellaoff.png", Selected = false });
                }
                Rankings.Clear();
                Rankings = new ObservableCollection<Ranking>(rank);
            });
        }

        private RelayCommand<string> abrirModalAlertaCommand = null;
        public RelayCommand<string> AbrirModalAlertaCommand
        {
            get => abrirModalAlertaCommand ??= new RelayCommand<string>((sms) =>
            {
                MensajeAlerta = sms;
                ModalAlerta = true;
            });
        }

        private RelayCommand ocultarModalAlertaCommand = null;
        public RelayCommand OcultarModalAlertaCommand
        {
            get => ocultarModalAlertaCommand ??= new RelayCommand(() =>
            {
                ModalAlerta = false;
                MensajeAlerta = string.Empty;
            });
        }
        #endregion
    }
    public class UbicacionActualEvent : EventArgs
    {
        public Geoposicion Geoposicion { get; set; }
    }
}

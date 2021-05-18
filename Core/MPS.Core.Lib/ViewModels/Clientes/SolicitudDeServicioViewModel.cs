using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel;
using MPS.SharedAPIModel.Clientes;
using MPS.SharedAPIModel.Notificaciones;
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

namespace MPS.Core.Lib.ViewModels.Clientes
{
    public class SolicitudDeServicioViewModel : ViewModelWithBL<SolicitudBL>
    {
        #region Constructor
        public SolicitudDeServicioViewModel()
        {
        }
        #endregion

        #region Eventos
        public event EventHandler<UbicacionActualEvent> ObteniendoUbicacion;
        #endregion

        #region Properties
        MapBL mapaBL;
        /// <summary>
        /// Obtiene o asigna la instancia de la clase de logica de operacione con Mapas.
        /// </summary>
        MapBL MapaBL => mapaBL ??= new MapBL();

        private List<Servicio> servicios = new List<Servicio>();
        public List<Servicio> Servicios { get => servicios; set => Set(ref servicios, value); }

        private Servicio servicioSeleccionado = new Servicio();
        public Servicio ServicioSeleccionado { get => servicioSeleccionado; set => Set(ref servicioSeleccionado, value); }

        private bool esExpress = true;
        public bool EsExpress
        {
            get => esExpress;
            set
            {
                Set(ref esExpress, value);
                RaisePropertyChanged(() => EsPersonalizado);
            }
        }

        public bool EsPersonalizado { get => !EsExpress; }

        bool openModalRegitsro;
        public bool OpenModalRegistro { get => openModalRegitsro; set => Set(ref openModalRegitsro, value); }

        private Solicitud solicitudServicio = new Solicitud();
        public Solicitud SolicitudServicio { get => solicitudServicio; set => Set(ref solicitudServicio, value); }

        private DateTime fecha = DateTime.UtcNow;
        public DateTime Fecha { get => fecha; set => Set(ref fecha, value); }

        private TimeSpan hora = new TimeSpan(12, 0, 0);
        public TimeSpan Hora { get => hora; set => Set(ref hora, value); }

        private ObservableCollection<Socio> socios = new ObservableCollection<Socio>();
        public ObservableCollection<Socio> Socios { get => socios; set => Set(ref socios, value); }

        private ObservableCollection<Socio> sociosSeleccionado = new ObservableCollection<Socio>();
        public ObservableCollection<Socio> SociosSeleccionado { get => sociosSeleccionado; set => Set(ref sociosSeleccionado, value); }

        private Socio socioRemove = new Socio();
        public Socio SocioRemove { get => socioRemove; set => Set(ref socioRemove, value); }

        private Geoposicion ubicacionSolicitud;
        public Geoposicion UbicacionSolicitud { get => ubicacionSolicitud; set => Set(ref ubicacionSolicitud, value); }

        private bool terminos;
        public bool Terminos { get => terminos; set => Set(ref terminos, value); }

        private string nombreUbicacion;
        public string NombreUbicacion { get => nombreUbicacion; set => Set(ref nombreUbicacion, value); }

        private bool cargarPersonal;
        public bool CargarPersonal { get => cargarPersonal; set => Set(ref cargarPersonal, value); }

        private bool cargarPersonalSeleccionado;
        public bool CargarPersonalSeleccionado { get => cargarPersonalSeleccionado; set => Set(ref cargarPersonalSeleccionado, value); }

        private bool seleccionarPersonal;
        public bool SeleccionarPersonal { get => seleccionarPersonal; set => Set(ref seleccionarPersonal, value); }

        private bool removePersonal;
        public bool RemovePersonal { get => removePersonal; set => Set(ref removePersonal, value); }

        private string mensaje;
        public string Mensaje { get => mensaje; set => Set(ref mensaje, value); }

        private bool modal;
        public bool Modal { get => modal; set => Set(ref modal, value); }

        private bool modalAgregarServicio;
        public bool ModalAgregarServicio { get => modalAgregarServicio; set => Set(ref modalAgregarServicio, value); }

        private string filtroSocios = string.Empty;
        public string FiltroSocios { get => filtroSocios; set => Set(ref filtroSocios, value); }

        private bool modalSolicitud;
        public bool ModalSolicitud { get => modalSolicitud; set => Set(ref modalSolicitud, value); }

        private ServicioSolicitado servicio;
        public ServicioSolicitado Servicio { get => servicio; set => Set(ref servicio, value); }

        private bool express;
        public bool Express { get => express; set => Set(ref express, value); }

        private bool personalizada;
        public bool Personalizada { get => personalizada; set => Set(ref personalizada, value); }

        private bool modalServicioAceptado;
        public bool ModalServicioAceptado { get => modalServicioAceptado; set => Set(ref modalServicioAceptado, value); }

        private List<int> horas;
        public List<int> Horas { get => horas; set => Set(ref horas, value); }

        private int horasSelected;
        public int HorasSelected
        {
            get => horasSelected;
            set
            {
                Set(ref horasSelected, value);
                SolicitudServicio.HorasSolicidatas = value;
            }
        }

        private string cosotoServicio;
        public string CosotoServicio { get => cosotoServicio; set => Set(ref cosotoServicio, value); }

        private ObservableCollection<Ranking> rankings;
        public ObservableCollection<Ranking> Rankings { get => rankings; set => Set(ref rankings, value); }

        private string observaciones;
        public string Observaciones { get => observaciones; set => Set(ref observaciones, value); }

        private bool modalCalificar;
        public bool ModalCalificar { get => modalCalificar; set => Set(ref modalCalificar, value); }

        private Guid idSolcitud;
        public Guid IdSolcitud { get => idSolcitud; set => Set(ref idSolcitud, value); }

        private bool modalEstatusPago;
        public bool ModalEstatusPago { get => modalEstatusPago; set { Set(ref modalEstatusPago, value); } }

        private EstatusPago estatusPago;
        public EstatusPago EstatusPago { get => estatusPago; set { Set(ref estatusPago, value); } }
        #endregion

        #region Commands
        RelayCommand<string> cambiarPrioridadCommand = null;
        public RelayCommand<string> CambiarPrioridadCommand
        {
            get => cambiarPrioridadCommand ??= new RelayCommand<string>((string p) =>
            {
                EsExpress = p == "Express";
                if (EsExpress)
                {
                    SociosSeleccionado.Clear();
                    CargarPersonalSeleccionado = true;
                }
            }, (string p) => true);
        }

        RelayCommand cerrarModalRegistroCommand = null;
        public RelayCommand CerrarModalRegistroCommand
        {
            get =>cerrarModalRegistroCommand ??= new RelayCommand(() =>
            {
                if(EsPersonalizado && SolicitudServicio.HorasSolicidatas == 0)
                {
                    Mensaje = "No a especificado las horas estimadas";
                    Modal = true;
                    return;
                }
                SeleccionarPersonal = true;
                OpenModalRegistro = OpenModalRegistro != true;
            });
        }

        RelayCommand obtenerComponentesCommand = null;
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

                var horasAux = new List<int>();
                for (int i = 1; i < 10; i++)
                    horasAux.Add(8 * i);
                Horas = new List<int>(horasAux);
                HorasSelected = Horas.FirstOrDefault();
                
                Servicios = await bl.ObtenerServiciosAsync();
                if(Servicios.Count > 0)
                    ServicioSeleccionado = Servicios.FirstOrDefault(s => s.NOMBRE.Contains("INTRAMUROS"));
                var ubicacion = await Sysne.Core.OS.DependencyService.Get<Sysne.Core.OS.IOS>().ObtenerGeoposicion(false);
                if (ObteniendoUbicacion != null && ubicacion != null && ServicioSeleccionado != null)
                {
                    UbicacionActualEvent args = new UbicacionActualEvent
                    {
                        Geoposicion = ubicacion
                    };
                    ObteniendoUbicacion(this, args);
                    UbicacionSolicitud = new Geoposicion(ubicacion.Latitud, ubicacion.Longitud);
                    if (string.IsNullOrEmpty(Settings.Current.Pais) && string.IsNullOrEmpty(Settings.Current.Estado))
                    {
                        var (estado, pais) = await MapaBL.ObtenerEstadoPais(ubicacion.Latitud.Value, ubicacion.Longitud.Value);
                        Settings.Current.Pais = pais;
                        Settings.Current.Estado = estado;
                    }
                }
            });
        }

        RelayCommand registrarSolicitudCommand = null;
        public RelayCommand RegistrarSolicitudCommand
        {
            get => registrarSolicitudCommand ??= new RelayCommand(async () =>
            {
                if (Terminos)
                {
                    SolicitudServicio.IdTipoSolicitud = ServicioSeleccionado.Guid;
                    SolicitudServicio.IdTipoServicio = EsExpress ? (int)TipoSolicitudEnum.Express : (int)TipoSolicitudEnum.Personalizada;
                    SolicitudServicio.FechaSolicitud = EsExpress ? DateTime.UtcNow : new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, Hora.Hours, Hora.Minutes, Hora.Seconds);
                    SolicitudServicio.IdCliente = Guid.Parse(Settings.Current.LoginInfo.Usr.Id);
                    SolicitudServicio.IdMoneda = Guid.Parse("f913e4ac-24b9-48e1-817d-ae4deb73c555");
                    SolicitudServicio.VersionApp = "VERSION";
                    SolicitudServicio.Movil = Settings.Current.ModeloDispositivo;
                    SolicitudServicio.Latitud = UbicacionSolicitud.Latitud.Value;
                    SolicitudServicio.Longitud = UbicacionSolicitud.Longitud.Value;
                    SolicitudServicio.SociosSelected = string.Empty;
                    if (EsPersonalizado)
                    {
                        var so = SociosSeleccionado.LastOrDefault();
                        foreach (var socio in SociosSeleccionado)
                        {
                            if (socio.Equals(so))
                                SolicitudServicio.SociosSelected += $"{socio.GUID_SOCIO}";
                            else
                                SolicitudServicio.SociosSelected += $"{socio.GUID_SOCIO},";
                        }
                    }
                    var (idSolicitud,(result, mensaje)) = await bl.RegistrarSolicitudAsync(SolicitudServicio);
                    if (result)
                    {
                        foreach (var socio in SociosSeleccionado)
                            await bl.AsignarSocioAsync(new SocioAsignado { IdSolicitud = idSolicitud, IdSocio = socio.GUID_SOCIO, Estatus = (int)EstatusSolicitudEnum.Inicial});
                        SolicitudServicio = new Solicitud() { TotalPago = 0, TiempoGenerarSolicitud = 0 };
                        HorasSelected = Horas.FirstOrDefault();
                        SociosSeleccionado.Clear();
                        CargarPersonalSeleccionado = true;
                        Mensaje = mensaje;
                        Terminos = false;
                        Modal = true;
                        ModalAgregarServicio = false;
                        NombreUbicacion = string.Empty;
                        var ubicacion = await Sysne.Core.OS.DependencyService.Get<Sysne.Core.OS.IOS>().ObtenerGeoposicion(false);
                        if (ObteniendoUbicacion != null && ubicacion != null && ServicioSeleccionado != null)
                        {
                            UbicacionActualEvent args = new UbicacionActualEvent
                            {
                                Geoposicion = ubicacion
                            };
                            ObteniendoUbicacion(this, args);
                            UbicacionSolicitud = new Geoposicion(ubicacion.Latitud, ubicacion.Longitud);
                            if (string.IsNullOrEmpty(Settings.Current.Pais) && string.IsNullOrEmpty(Settings.Current.Estado))
                            {
                                var (estado, pais) = await MapaBL.ObtenerEstadoPais(ubicacion.Latitud.Value, ubicacion.Longitud.Value);
                                Settings.Current.Pais = pais;
                                Settings.Current.Estado = estado;
                            }
                        }
                    }
                    else
                    {
                        ModalAgregarServicio = false;
                        Mensaje = mensaje;
                        Modal = true;
                    }
                }
                else
                {
                    Mensaje = "No ha aceptado los terminos y condiciones";
                    Modal = true;
                }
              

            });
        }

        private RelayCommand modalRegistrarServiciosCommand = null;
        public RelayCommand ModalRegistrarServiciosCommand
        {
            get => modalRegistrarServiciosCommand ??= new RelayCommand(async () =>
            {
                if (SolicitudServicio.NoElementos == 0 || SolicitudServicio.HorasSolicidatas == 0)
                {
                    Mensaje = "Faltan campos por capturar";
                    Modal = true;
                    return;
                }
                if (EsPersonalizado)
                    if (SociosSeleccionado.Count == 0)
                    {
                        Mensaje = "Debe de agregar al menos a un personal";
                        Modal = true;
                        return;
                    }
                var costo = await bl.CalcularCostoServicioAsync(ServicioSeleccionado.Guid, HorasSelected, EsExpress ? (int)TipoSolicitudEnum.Express : (int)TipoSolicitudEnum.Personalizada);
                CosotoServicio = costo.ToString("C2");
                SolicitudServicio.TotalPago = costo;
                ModalAgregarServicio = true;
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

        private RelayCommand ocultarModalServiciosCommand = null;
        public RelayCommand OcultarModalServiciosCommand
        {
            get => ocultarModalServiciosCommand ??= new RelayCommand(() =>
            {
                ModalAgregarServicio = false;
            });
        }

        private RelayCommand buscarSociosCommand = null;
        public RelayCommand BuscarSociosCommand
        {
            get => buscarSociosCommand ??= new RelayCommand(async () =>
            {
                Socios.Clear();
                var socios = await bl.ObtenerSociosAsync(ServicioSeleccionado.Guid, Fecha, SolicitudServicio.HorasSolicidatas, FiltroSocios);
                if (socios.Count > 0)
                    Socios = new ObservableCollection<Socio>(socios);
            });
        }

        private RelayCommand<Socio> seleccionarPersonalCommand = null;
        public RelayCommand<Socio> SeleccionarPersonalCommand
        {
            get => seleccionarPersonalCommand ??= new RelayCommand<Socio>((socio) =>
            {
                if (SociosSeleccionado.Where(w => w.GUID_SOCIO.Equals(socio.GUID_SOCIO)).Count() > 0)
                    SociosSeleccionado.Remove(socio);
                else
                    SociosSeleccionado.Add(socio);
                var index = Socios.IndexOf(socio);
                socio.Seleccionado = socio.Seleccionado.Equals("checkoff.png") ? "checkin.png" : "checkoff.png";
                Socios.Remove(socio);
                Socios.Insert(index, socio);
            });
        }

        private RelayCommand mostrarPersonalSeleccionadoCommand = null;
        public RelayCommand MostrarPersonalSeleccionadoCommand
        {
            get => mostrarPersonalSeleccionadoCommand ??= new RelayCommand(() =>
            {
                SociosSeleccionado = new ObservableCollection<Socio>(SociosSeleccionado);
                OpenModalRegistro = !OpenModalRegistro;
                Socios.Clear();
            });
        }

        private RelayCommand<Socio> removePersonalCommand = null;
        public RelayCommand<Socio> RemovePersonalCommand
        {
            get => removePersonalCommand ??= new RelayCommand<Socio>((socio) =>
            {
                SociosSeleccionado.Remove(socio);
            });
        }

        RelayCommand<(double, double)> obtenerDireccionCommand = null;
        public RelayCommand<(double, double)> ObtenerDireccionCommand
        {
            get => obtenerDireccionCommand ??= new RelayCommand<(double, double)>(async (ubicacion) =>
            {
                var geoposicion = new Geoposicion(ubicacion.Item1, ubicacion.Item2);
                if (ObteniendoUbicacion != null && geoposicion != null && ServicioSeleccionado != null)
                {
                    var result = await MapaBL.ObtenerDireccion(geoposicion.Latitud.Value, geoposicion.Longitud.Value);
                    NombreUbicacion = result ?? string.Empty;
                    UbicacionActualEvent args = new UbicacionActualEvent
                    {
                        Geoposicion =  geoposicion
                    };
                    ObteniendoUbicacion(this, args);
                    UbicacionSolicitud = new Geoposicion(geoposicion.Latitud, geoposicion.Longitud);
                }
            });
        }

        RelayCommand buscarUbicacionCommand = null;
        public RelayCommand BuscarUbicacionCommand
        {
            get => buscarUbicacionCommand ??= new RelayCommand(async () =>
            {
                if (!string.IsNullOrEmpty(NombreUbicacion))
                {
                    var result = await MapaBL.ObtenerPoints(NombreUbicacion);
                    if (ObteniendoUbicacion != null && result != null && result.Count > 1 && ServicioSeleccionado != null)
                    {
                        var geoposicion = new Geoposicion(result[0], result[1]);
                        UbicacionActualEvent args = new UbicacionActualEvent
                        {
                            Geoposicion = geoposicion
                        };
                        ObteniendoUbicacion(this, args);
                        UbicacionSolicitud = new Geoposicion(geoposicion.Latitud, geoposicion.Longitud);
                    }
                }
            });
        }

        private RelayCommand<ServicioSolicitado> mostrarModalSolicitudCommand = null;
        public RelayCommand<ServicioSolicitado> MostrarModalSolicitudCommand
        {
            get => mostrarModalSolicitudCommand ??= new RelayCommand<ServicioSolicitado>((s) =>
            {
                if (s.ClaveTipoServicio.Equals((int)TipoSolicitudEnum.Express))
                    Express = true;
                else
                    Personalizada = true;
                Servicio = s;
                ModalServicioAceptado = true;
            });
        }

        private RelayCommand ocultarModalSolicitudCommand = null;
        public RelayCommand OcultarModalSolicitudCommand
        {
            get => ocultarModalSolicitudCommand ??= new RelayCommand(() =>
            {
                ModalServicioAceptado = false;
                Servicio = new ServicioSolicitado();
                Settings.Current.ServicioSolicitado = new ServicioSolicitado();
                Express = false;
                Personalizada = false;
                SelectRankingCommand.Execute(new Ranking { Rank = 1 });
            });
        }

        private RelayCommand verificarSolicitudCommand = null;
        public RelayCommand VerificarSolicitudCommand
        {
            get => verificarSolicitudCommand ??= new RelayCommand(() =>
            {
                if (Settings.Current.ServicioSolicitado != null && !string.IsNullOrEmpty(Settings.Current.ServicioSolicitado.FolioSolicitud))
                {
                    if (Settings.Current.ServicioSolicitado.TipoNotificacion.Equals((int)TipoNotificacionEnum.SocioAcepta))
                    {
                        Servicio = Settings.Current.ServicioSolicitado;
                        if (Servicio.ClaveTipoServicio.Equals(1))
                            Express = true;
                        else
                            Personalizada = true;
                        ModalServicioAceptado = true;
                    }
                    else if (Settings.Current.ServicioSolicitado.TipoNotificacion.Equals((int)TipoNotificacionEnum.Finalizado))
                        ModalCalificar = true;
                }
            });
        }

        private RelayCommand enviarCalificacionCommand = null;
        public RelayCommand EnviarCalificacionCommand
        {
            get => enviarCalificacionCommand ??= new RelayCommand(async () =>
            {
                var result = await (new ClientesBL()).CalificarSocioAysnc(IdSolcitud, Rankings.Where(w => w.Selected).Count(), Observaciones);
                if(result)
                {
                    IdSolcitud = Guid.Empty;
                    ModalCalificar = false;
                }
                else
                {
                    ModalCalificar = false;
                    Mensaje = "Ocurrio un problema al finalizar el servicio";
                    Modal = true;
                }
            });
        }

        private RelayCommand<Guid> abrirModalCalificarCommand = null;
        public RelayCommand<Guid> AbrirModalCalificarCommand
        {
            get => abrirModalCalificarCommand ??= new RelayCommand<Guid>((id) =>
            {
                IdSolcitud = id;
                ModalCalificar = true;
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

        private RelayCommand verificarCalificacionCommand = null;
        public RelayCommand VerificarCalificacionCommand
        {
            get => verificarCalificacionCommand ??= new RelayCommand(() =>
            {
                if (Settings.Current.ServicioSolicitado != null && !string.IsNullOrEmpty(Settings.Current.ServicioSolicitado.FolioSolicitud))
                {
                    if (Settings.Current.ServicioSolicitado.TipoNotificacion.Equals((int)TipoNotificacionEnum.Finalizado))
                        AbrirModalCalificarCommand.Execute(Settings.Current.ServicioSolicitado.IdSolicitud);
                }
            });
        }

        private RelayCommand<EstatusPago> mostrarModalEstatusPagoCommand = null;
        public RelayCommand<EstatusPago> MostrarModalEstatusPagoCommand
        {
            get => mostrarModalEstatusPagoCommand ??= new RelayCommand<EstatusPago>((e) =>
            {
                if (e.ClaveTipoServicio.Equals((int)TipoSolicitudEnum.Express))
                    Express = true;
                else
                    Personalizada = true;
                if (e.Codigo != 1)
                    e.Descripcion = Utilidades.ErrorRegistroOpenpay(e.Codigo);
                EstatusPago = e;
                ModalEstatusPago = true;
            });
        }

        private RelayCommand ocultarModalEstatusPagoCommand = null;
        public RelayCommand OcultarModalEstatusPagoCommand
        {
            get => ocultarModalEstatusPagoCommand ??= new RelayCommand(() =>
            {
                ModalEstatusPago = false;
                EstatusPago = new EstatusPago();
                Settings.Current.EstatusPago = new EstatusPago();
                Express = false;
                Personalizada = false;
            });
        }

        private RelayCommand verificarEstatusPagoCommand = null;
        public RelayCommand VerificarEstatusPagoCommand
        {
            get => verificarEstatusPagoCommand ??= new RelayCommand(() =>
            {
                if (Settings.Current.EstatusPago != null && !string.IsNullOrEmpty(Settings.Current.EstatusPago.NombreServicio))
                {
                    EstatusPago = Settings.Current.EstatusPago;
                    if (EstatusPago.ClaveTipoServicio.Equals((int)TipoSolicitudEnum.Express))
                        Express = true;
                    else
                        Personalizada = true;
                    if (EstatusPago.Codigo != 1)
                        EstatusPago.Descripcion = Utilidades.ErrorRegistroOpenpay(Settings.Current.EstatusPago.Codigo);
                    ModalEstatusPago = true;
                }
            });
        }
        #endregion
    }
    public class UbicacionActualEvent : EventArgs
    {
        public Geoposicion Geoposicion { get; set; }
    }
}

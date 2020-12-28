using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel;
using MPS.SharedAPIModel.Clientes;
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

        private Solicitud solicitudServicio = new Solicitud() { TotalPago = 1000, TiempoGenerarSolicitud = 5};
        public Solicitud SolicitudServicio { get => solicitudServicio; set => Set(ref solicitudServicio, value); }

        private DateTime fecha = DateTime.Now;
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
                    SolicitudServicio.IdTipoServicio = EsExpress ? 1 : 2;
                    SolicitudServicio.FechaSolicitud = new DateTime(Fecha.Year, Fecha.Month, Fecha.Day, Hora.Hours, Hora.Minutes, Hora.Seconds);
                    SolicitudServicio.IdCliente = Guid.Parse(Settings.Current.LoginInfo.Usr.Id);
                    SolicitudServicio.IdMoneda = Guid.Parse("f913e4ac-24b9-48e1-817d-ae4deb73c555");
                    SolicitudServicio.VersionApp = "VERSION";
                    SolicitudServicio.Movil = Settings.Current.ModeloDispositivo;
                    SolicitudServicio.Latitud = UbicacionSolicitud.Latitud.Value;
                    SolicitudServicio.Longitud = UbicacionSolicitud.Longitud.Value;
                    var (idSolicitud,(result, mensaje)) = await bl.RegistrarSolicitudAsync(SolicitudServicio);
                    if (result)
                    {
                        foreach (var socio in SociosSeleccionado)
                            await bl.AsignarSocioAsync(new SocioAsignado { IdSolicitud = idSolicitud, IdSocio = socio.GUID_SOCIO, Estatus = 0});
                        SolicitudServicio = new Solicitud() { TotalPago = 1000, TiempoGenerarSolicitud = 5 };
                        SociosSeleccionado.Clear();
                        CargarPersonalSeleccionado = true;
                        Mensaje = mensaje;
                        Terminos = false;
                        Modal = true;
                        ModalAgregarServicio = false;
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
            get => modalRegistrarServiciosCommand ??= new RelayCommand(() =>
            {
                if (SolicitudServicio.NoElementos == 0 && SolicitudServicio.HorasSolicidatas == 0)
                {
                    Mensaje = "Faltan campos por capturar,\nverifique informacion";
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
                var so = socios.LastOrDefault();
                socios.Remove(so);
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
                if (s.ClaveTipoServicio.Equals(1))
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
                Settings.Current.Solicitud = new SolicitudServicio();
                Express = false;
                Personalizada = false;
            });
        }

        private RelayCommand verificarSolicitudCommand = null;
        public RelayCommand VerificarSolicitudCommand
        {
            get => verificarSolicitudCommand ??= new RelayCommand(() =>
            {
                if (Settings.Current.ServicioSolicitado != null && !string.IsNullOrEmpty(Settings.Current.ServicioSolicitado.FolioSolicitud))
                {
                    Servicio = Settings.Current.ServicioSolicitado;
                    if (Servicio.ClaveTipoServicio.Equals(1))
                        Express = true;
                    else
                        Personalizada = true;
                    ModalServicioAceptado = true;
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

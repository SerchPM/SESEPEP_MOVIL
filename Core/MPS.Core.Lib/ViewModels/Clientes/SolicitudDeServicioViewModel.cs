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
        MapBL MapaBL => mapaBL ?? (mapaBL = new MapBL());
        private List<Servicio> servicios = new List<Servicio>();
        public List<Servicio> Servicios { get => servicios; set => Set(ref servicios, value); }

        private Servicio servicioSeleccionado;
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

        private List<Socios> socios = new List<Socios>();
        public List<Socios> Socios { get => socios; set => Set(ref socios, value); }

        private List<Socios> sociosSeleccionado = new List<Socios>();
        public List<Socios> SociosSeleccionado { get => sociosSeleccionado; set => Set(ref sociosSeleccionado, value); }

        private Socios socioRemove = new Socios();
        public Socios SocioRemove { get => socioRemove; set => Set(ref socioRemove, value); }

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
        #endregion

        #region Commands

        RelayCommand<string> cambiarPrioridadCommand = null;
        public RelayCommand<string> CambiarPrioridadCommand
        {
            get => cambiarPrioridadCommand ??= new RelayCommand<string>((string p) =>
            {
                EsExpress = p == "Express";
            }, (string p) => true);
        }

        RelayCommand cerrarModalRegistroCommand = null;
        public RelayCommand CerrarModalRegistroCommand
        {
            get =>cerrarModalRegistroCommand ??= new RelayCommand(() =>
            {
                OpenModalRegistro = (OpenModalRegistro == true) ? false : true;
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
                    Socios = await bl.ObtenerSociosAsync(ServicioSeleccionado.Guid, Fecha, 8);
                    if (Socios.Count > 0)
                        CargarPersonal = true;
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
                    var (result, mensaje) = await bl.RegistrarSolicitudAsync(SolicitudServicio);
                    if (result)
                    {
                        SolicitudServicio = new Solicitud() { TotalPago = 1000, TiempoGenerarSolicitud = 5 };
                        Mensaje = mensaje;
                        Modal = true;
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
                    Mensaje = "No a aceptado los terminos y condiciones";
                    Modal = true;
                }
              

            });
        }

        private RelayCommand modalRegistrarServiciosCommand = null;
        public RelayCommand ModalRegistrarServiciosCommand
        {
            get => modalRegistrarServiciosCommand ??= new RelayCommand(() =>
            {
                if (SolicitudServicio.NoElementos > 0 && SolicitudServicio.HorasSolicidatas > 0)
                    ModalAgregarServicio = true;
                else
                {
                    Mensaje = "Faltan campos por capturar,\nverifique informacion";
                    Modal = true;
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

        private RelayCommand ocultarModalServiciosCommand = null;
        public RelayCommand OcultarModalServiciosCommand
        {
            get => ocultarModalServiciosCommand ??= new RelayCommand(() =>
            {
                ModalAgregarServicio = false;
            });
        }

        private RelayCommand<Socios> seleccionarPersonalCommand = null;
        public RelayCommand<Socios> SeleccionarPersonalCommand
        {
            get => seleccionarPersonalCommand ??= new RelayCommand<Socios>((socio) =>
            {
                if (SociosSeleccionado.Where(w => w.GUID_SOCIO.Equals(socio.GUID_SOCIO)).Count() > 0)
                    SociosSeleccionado.Remove(socio);
                else
                    SociosSeleccionado.Add(socio);
                if (SociosSeleccionado != null)
                    SeleccionarPersonal = true;
            });
        }

        private RelayCommand mostrarPersonalSeleccionadoCommand = null;
        public RelayCommand MostrarPersonalSeleccionadoCommand
        {
            get => mostrarPersonalSeleccionadoCommand ??= new RelayCommand(() =>
            {
                CargarPersonalSeleccionado = true;
                OpenModalRegistro = (OpenModalRegistro == true) ? false : true;
            });
        }

        private RelayCommand<Socios> removePersonalCommand = null;
        public RelayCommand<Socios> RemovePersonalCommand
        {
            get => removePersonalCommand ??= new RelayCommand<Socios>((socio) =>
            {
                SociosSeleccionado.Remove(socio);
                if (SocioRemove != null)
                    RemovePersonal = true;
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
                    Socios = await bl.ObtenerSociosCercanosAsync(geoposicion.Latitud.Value, geoposicion.Longitud.Value, ServicioSeleccionado.Guid);
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
                        Socios = await bl.ObtenerSociosCercanosAsync(geoposicion.Latitud.Value, geoposicion.Longitud.Value, ServicioSeleccionado.Guid);
                    }
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

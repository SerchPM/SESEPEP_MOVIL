using MPS.Core.Lib.ApiClient;
using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel;
using MPS.SharedAPIModel.Operaciones;
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
            Id = Settings.Current.LoginInfo.Usr.Id;
        }

        MapBL mapaBL;
        MapBL MapaBL => mapaBL ??= new MapBL();

        SolicitudBL solicitudBL;
        SolicitudBL SolicitudBL => solicitudBL ?? new SolicitudBL();

        #region Eventos
        public event EventHandler<UbicacionActualEvent> ObteniendoUbicacion;
        #endregion

        string solicitud;
        public string Solicitud { get => solicitud; set { Set(ref solicitud, value); } }

        string id;
        public string Id { get => id; set { Set(ref id, value); } }

        bool modal;
        public bool Modal {get => modal; set{Set(ref modal, value); } }

        string mensaje;
        public string Mensaje { get => mensaje; set { Set(ref mensaje, value); } }

        bool estatusSolicitud;
        public bool EstatusSolicitud { get => estatusSolicitud; set { Set(ref estatusSolicitud, value); } }

        private string nombreUbicacion;
        public string NombreUbicacion { get => nombreUbicacion; set => Set(ref nombreUbicacion, value); }

        private Geoposicion ubicacionSolicitud;
        public Geoposicion UbicacionSolicitud { get => ubicacionSolicitud; set => Set(ref ubicacionSolicitud, value); }

        private Servicio servicioSeleccionado = new Servicio();
        public Servicio ServicioSeleccionado { get => servicioSeleccionado; set => Set(ref servicioSeleccionado, value); }

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
                if (ObteniendoUbicacion != null && geoposicion != null && ServicioSeleccionado != null)
                {
                    var result = await MapaBL.ObtenerDireccion(geoposicion.Latitud.Value, geoposicion.Longitud.Value);
                    NombreUbicacion = result ?? string.Empty;
                    UbicacionActualEvent args = new UbicacionActualEvent
                    {
                        Geoposicion = geoposicion
                    };
                    ObteniendoUbicacion(this, args);
                    UbicacionSolicitud = new Geoposicion(geoposicion.Latitud, geoposicion.Longitud);
                }
            });
        }

        RelayCommand solicitudActivaCommand = null;
        public RelayCommand SolicitudActivaCommand
        {
            get => solicitudActivaCommand ?? (solicitudActivaCommand = new RelayCommand(async () =>
            {
                var respuesta = await SolicitudBL.SolicitudActiva(Settings.Current.LoginInfo.Usr.Id);
                if (respuesta.Item1)
                    EstatusSolicitud = true;
            }));
        }

        RelayCommand consultaDetalleSolicitudCommand = null; 
        /// <summary>
        /// Consulta el detalle de una solicitud al seleccionarla en el mapa
        /// </summary>
        public RelayCommand ConsultaDetalleSolicitudCommand
        {
            get => consultaDetalleSolicitudCommand ?? (consultaDetalleSolicitudCommand = new RelayCommand(async () =>
            {

            }));
        }

        RelayCommand<int> finalizaSolicitudCommand = null;
        public RelayCommand<int> FinalizaSolicitud
        {
            get => finalizaSolicitudCommand ?? (finalizaSolicitudCommand = new RelayCommand<int>(async (status) =>
            {
                var resultado = await bl.ActualizaSolicitud(Guid.Parse(Id), Guid.Parse(Solicitud), status);
            }));
        }

        RelayCommand tomarSolicitud = null;
        public RelayCommand Tomarsolicitud
        {
            get => tomarSolicitud ?? (tomarSolicitud = new RelayCommand(async () =>
            {
                var asignarSocio = new SocioAsignado
                {
                    IdSocio = Guid.Parse(Id),
                    IdSolicitud = Guid.Parse(solicitud),
                    Estatus = 1
                };
                var resultado = await SolicitudBL.AsignarSocioAsync(asignarSocio);
                if (resultado)
                    EstatusSolicitud = true;
            }));
        }

        public class UbicacionActualEvent : EventArgs
        {
            public Geoposicion Geoposicion { get; set; }
        }
    }
}

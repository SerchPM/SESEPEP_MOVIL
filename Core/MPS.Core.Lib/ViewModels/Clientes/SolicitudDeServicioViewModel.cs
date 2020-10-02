using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Operaciones;
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
            ObtenerComponentesCommand.Execute();
        }
        #endregion

        #region Properties
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

        private Solicitud solicitudServicio = new Solicitud();
        public Solicitud SolicitudServicio { get => solicitudServicio; set => Set(ref solicitudServicio, value); }

        private DateTime fecha = DateTime.Now;
        public DateTime Fecha { get => fecha; set => Set(ref fecha, value); }

        private TimeSpan hora = new TimeSpan(12, 0, 0);
        public TimeSpan Hora { get => hora; set => Set(ref hora, value); }

        //private List<Socios> socios;
        //public List<Socios> Socios { get => socios; set => Set(ref socios, value); }

        private bool terminos;
        public bool Terminos 
        { 
            get => terminos; 
            set => Set(ref terminos, value); 
        }
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
                {
                    ServicioSeleccionado = Servicios.FirstOrDefault(s => s.NOMBRE.Contains("INTRAMUROS"));
                    //Socios = await bl.ObtenerSociosAsync(ServicioSeleccionado.Guid);
                }
            });
        }

        RelayCommand registrarSolicitudCommand = null;
        public RelayCommand RegistrarSolicitudCommand
        {
            get => registrarSolicitudCommand ??= new RelayCommand(async () =>
            {
                SolicitudServicio.IdTipoSolicitud = ServicioSeleccionado.Guid;
                var r = Settings.Current.LoginInfo.Usr.Id;
                SolicitudServicio.IdCliente = Guid.Parse(Settings.Current.LoginInfo.Usr.Id);
                SolicitudServicio.IdTipoServicio = EsExpress ? 1 : 2;
                var result = await bl.RegistrarSolicitudAsync(SolicitudServicio);
            });
        }
        #endregion
    }
}

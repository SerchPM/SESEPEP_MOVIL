using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using MPS.Core.Lib.Helpers;
using MPS.Core.Lib.OS;
using MPS.SharedAPIModel.Socios;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using Sysne.Core.OS;

namespace MPS.Core.Lib.ViewModels.Socios
{
    public class SolicitudesViewModel : ViewModelWithBL<BL.SociosBL>
    {
        #region Constructor
        public SolicitudesViewModel()
        {

        }
        #endregion

        #region Propiedades
        private List<SolicitudPendiente> solicitudes = new List<SolicitudPendiente>();
        public List<SolicitudPendiente> Solicitudes { get => solicitudes; set => Set(ref solicitudes, value); }

        private bool cargarServicios;
        public bool CargarServicios { get => cargarServicios; set => Set(ref cargarServicios, value); }

        bool modal;
        public bool Modal { get => modal; set { Set(ref modal, value); } }

        string mensaje;
        public string Mensaje { get => mensaje; set { Set(ref mensaje, value); } }
        #endregion

        #region Comandos

        private RelayCommand obtenerSolicitudesPendientesCommand = null;
        public RelayCommand ObtenerSolicitudesPendientesCommand
        {
            get => obtenerSolicitudesPendientesCommand ??= new RelayCommand(async () =>
            {
                Solicitudes.Clear();
                Solicitudes = await bl.ObtenerSolicitudesPendientesAsync(Guid.Parse(Helpers.Settings.Current.LoginInfo.Usr.Id));
                CargarServicios = true; 
            });
        }

        private RelayCommand<SolicitudPendiente> iniciarServicioCommand = null;
        public RelayCommand<SolicitudPendiente> IniciarServicioCommand
        {
            get => iniciarServicioCommand ??= new RelayCommand<SolicitudPendiente>(async (servicio) =>
            {
                var result = await (new BL.SolicitudBL()).AsignarSocioAsync(new SharedAPIModel.Solicitud.SocioAsignado { IdSocio=Guid.Parse(Settings.Current.LoginInfo.Usr.Id), IdSolicitud = servicio.GUID_SOLICITUD, Estatus = (int)EstatusSolicitudEnum.EnCurso});
                if (result)
                    await DependencyService.Get<INavigationService>().NavigateTo(PagesKeys.SolicitarServicio);
                else
                {
                    Mensaje = "Ya tienes un servicio en atencion";
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
        #endregion
    }
}

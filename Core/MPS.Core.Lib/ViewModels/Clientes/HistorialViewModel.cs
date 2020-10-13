using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Clientes;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.Core.Lib.ViewModels.Clientes
{
    public class HistorialViewModel :ViewModelWithBL<ClientesBL>
    {
        #region Constructor
        public HistorialViewModel()
        {
        }
        #endregion

        #region Propeidades
        private List<ClienteSolicitud> historial =  new List<ClienteSolicitud>();
        public List<ClienteSolicitud> Historial { get => historial; set => Set(ref historial, value); }

        private DateTime desde = DateTime.Now;
        public DateTime Desde { get => desde; set => Set(ref desde, value); }

        private DateTime hasta = DateTime.Now;
        public DateTime Hasta { get => hasta; set => Set(ref hasta, value); }

        private bool cargarSolicitudes;
        public bool CargarSolicitudes { get => cargarSolicitudes; set => Set(ref cargarSolicitudes, value); }
        #endregion

        #region Comandos

        private RelayCommand buscarHistorialCommand = null;
        public RelayCommand BuscarHistorialCommand
        {
            get => buscarHistorialCommand ??= new RelayCommand(async () =>
            {
                Historial.Clear();
                Historial = await bl.GetSolicitudesAsync(Guid.Parse(Settings.Current.LoginInfo.Usr.Id), Desde, Hasta);
                CargarSolicitudes = true;
            });
        }

        private RelayCommand<ClienteSolicitud> enviarHistoricoCommand = null;
        public RelayCommand<ClienteSolicitud> EnviarHistoricoCommand
        {
            get => enviarHistoricoCommand ??= new RelayCommand<ClienteSolicitud>(async (param) =>
            {

            });
        }

        private RelayCommand exportarResultadosCommand = null;
        public RelayCommand ExportarResultadosCommand
        {
            get => exportarResultadosCommand ??= new RelayCommand(async () =>
            {

            });
        }

        #endregion
    }
}

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
    public class HistorialViewModel :ViewModelWithBL<SociosBL>
    {
        #region Constructor
        public HistorialViewModel()
        {
        }
        #endregion

        #region Propeidades
        private List<Historial> historial =  new List<Historial>();
        public List<Historial> Historial { get => historial; set => Set(ref historial, value); }

        private DateTime desde = DateTime.Now;
        public DateTime Desde { get => desde; set => Set(ref desde, value); }

        private DateTime hasta = DateTime.Now;
        public DateTime Hasta { get => hasta; set => Set(ref hasta, value); }
        #endregion

        #region Comandos

        private RelayCommand buscarHistorialCommand = null;
        public RelayCommand BuscarHistorialCommand
        {
            get => buscarHistorialCommand ??= new RelayCommand(async () =>
            {
                var r = await bl.GetHistoricoSolicitudesAsync(Guid.Parse(Settings.Current.LoginInfo.Usr.Id), Desde, Hasta);
            });
        }

        private RelayCommand<Historial> enviarHistoricoCommand = null;
        public RelayCommand<Historial> EnviarHistoricoCommand
        {
            get => enviarHistoricoCommand ??= new RelayCommand<Historial>(async (param) =>
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

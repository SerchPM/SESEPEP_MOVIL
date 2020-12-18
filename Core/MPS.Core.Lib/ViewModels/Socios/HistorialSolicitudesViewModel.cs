using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Socios;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.Core.Lib.ViewModels.Socios
{
    public class HistorialSolicitudesViewModel : ViewModelWithBL<SociosBL>
    {
        #region Constructor
        public HistorialSolicitudesViewModel()
        {
            Id = Settings.Current.LoginInfo.Usr.Id;
        }
        #endregion

        #region Propiedades
        List<HistorialSolicitudes> historial;
        public List<HistorialSolicitudes> Historial { get => historial; set { Set(ref historial, value); } }

        string id;
        public string Id { get => id; set { Set(ref id, value); } }

        private DateTime desde = DateTime.Now;
        public DateTime Desde { get => desde; set => Set(ref desde, value); }

        private DateTime hasta = DateTime.Now;
        public DateTime Hasta { get => hasta; set => Set(ref hasta, value); }

        private bool cargarSolicitudes;
        public bool CargarSolicitudes { get => cargarSolicitudes; set => Set(ref cargarSolicitudes, value); }
        #endregion

        #region Comandos
        RelayCommand getSolicitudesCommand = null;
        public RelayCommand GetSolicitudesCommand
        {
            get => getSolicitudesCommand ??= new RelayCommand(async () =>
            {
                Historial = await bl.GetHistoricoSolicitudesAsync(Guid.Parse(Id), Desde, Hasta);
                CargarSolicitudes = true;
            });
        }

        private RelayCommand<HistorialSolicitudes> enviarHistoricoCommand = null;
        public RelayCommand<HistorialSolicitudes> EnviarHistoricoCommand
        {
            get => enviarHistoricoCommand ??= new RelayCommand<HistorialSolicitudes>((param) =>
            {

            });
        }

        private RelayCommand exportarResultadosCommand = null;
        public RelayCommand ExportarResultadosCommand
        {
            get => exportarResultadosCommand ??= new RelayCommand(() =>
            {

            });
        }
        #endregion
    }
}

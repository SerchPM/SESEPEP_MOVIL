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
        public HistorialSolicitudesViewModel()
        {
            Id = Settings.Current.LoginInfo.Usr.Id;
        }

        List<HistorialSolicitudes> historial;
        public List<HistorialSolicitudes> Historial { get => historial; set { Set(ref historial, value); } }

        string id;
        public string Id { get => id; set { Set(ref id, value); } }

        RelayCommand<List<string>> getSolicitudesCommand = null;
        public RelayCommand<List<string>> GetSolicitudesCommand
        {
            get => getSolicitudesCommand ??= new RelayCommand<List<string>>(async (fechas) =>
            {
                var inicio = "";
                var fin = "";
                foreach(var a in fechas)
                {
                    if (string.IsNullOrEmpty(inicio)) { inicio = a; }
                    else { fin = a; }
                }
                DateTime begin = DateTime.Parse(inicio);
                DateTime end = DateTime.Parse(fin);
                Historial = await bl.GetHistoricoSolicitudesAsync(Guid.Parse(Id), begin, end);
                foreach(var a in Historial)
                {
                    if (string.IsNullOrEmpty(a.TIEMPO_REAL))
                        a.TIEMPO_REAL = "0";
                    if (string.IsNullOrEmpty(a.TOTAL_PAG_SOCIO))
                        a.TOTAL_PAG_SOCIO = "$0.00";
                    //if (a.VALORACION_CLIENTE != null) //No tiene sentido la comparación pues es float y no float?
                        a.VALORACION_CLIENTE = 0;
                }
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
    }
}

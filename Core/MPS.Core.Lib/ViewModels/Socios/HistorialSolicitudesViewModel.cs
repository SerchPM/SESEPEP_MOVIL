using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
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

        List<SharedAPIModel.Clientes.Historial> historial;
        public List<SharedAPIModel.Clientes.Historial> Historial { get => historial; set { Set(ref historial, value); } }

        string id;
        public string Id { get => id; set { Set(ref id, value); } }

        RelayCommand<List<string>> getSolicitudesCommand = null;
        public RelayCommand<List<string>> GetSolicitudesCommand
        {
            get => getSolicitudesCommand ?? (getSolicitudesCommand = new RelayCommand<List<string>>(async (fechas) =>
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
            }));
        }

        private RelayCommand<SharedAPIModel.Clientes.Historial> enviarHistoricoCommand = null;
        public RelayCommand<SharedAPIModel.Clientes.Historial> EnviarHistoricoCommand
        {
            get => enviarHistoricoCommand ??= new RelayCommand<SharedAPIModel.Clientes.Historial>(async (param) =>
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
    }
}

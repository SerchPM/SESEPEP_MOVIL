using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.Core.Lib.OS;
using MPS.SharedAPIModel.Socios;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
//using Xamarin.Forms;

namespace MPS.Core.Lib.ViewModels.Socios
{
    public class DetalleSocioViewModel : ViewModelWithBL<SociosBL>
    {
        public DetalleSocioViewModel()
        {
            Id = Settings.Current.LoginInfo.Usr.Id;
        }

        string id;
        public string Id { get => id; set { Set(ref id, value); } }

        DetalleSocio detallesSocio;
        public DetalleSocio DetallesSocio { get => detallesSocio; set { Set(ref detallesSocio, value); } }

        string nombreCompleto;
        public string NombreCompleto { get => nombreCompleto; set { Set(ref nombreCompleto, value); } }

        string ranking;
        public string Ranking { get => ranking; set { Set(ref ranking, value); } }

        string noCliente;
        public string NoCliente { get => noCliente; set { Set(ref noCliente, value); } }

        RelayCommand obtenerDetalleSocioCommand = null;
        public RelayCommand ObtenerDetalleSocioCommand
        {
            get => obtenerDetalleSocioCommand ?? (obtenerDetalleSocioCommand = new RelayCommand(async () =>
            {
                var (exito, detalles) = await bl.DetalleSocio(Id);
                NoCliente = detalles.NO_CLIENTE.ToString()??"";
                Ranking = detalles.RANKING.ToString();
                DetallesSocio = detalles;
                NombreCompleto = $"{DetallesSocio.NOMBRE}{DetallesSocio.APELLIDO_1}{DetallesSocio.APELLIDO_2}";
            }));
        }

        RelayCommand<DetalleSocio> actualizaInfoCommand = null;
        public RelayCommand<DetalleSocio> ActualizaInfoCommand
        {
            get => actualizaInfoCommand ?? (actualizaInfoCommand = new RelayCommand<DetalleSocio>(async (DetalleSocio info) =>
            {
                var (exito, respuesta) = await bl.ActualizaInfoSocio(Id, info);
            }));
        }
    }
}

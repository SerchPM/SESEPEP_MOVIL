using Core.MVVM.Helpers;
using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.Core.Lib.OS;
using MPS.SharedAPIModel.Socios;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Security.Cryptography;
using System.Text;

namespace MPS.Core.Lib.ViewModels.Socios
{
    public class DetalleSocioViewModel : ViewModelWithBL<SociosBL>
    {
        public DetalleSocioViewModel()
        {
            Id = Settings.Current.LoginInfo.Usr.Id;
            NoCliente = "";
            Ranking = "0.0";
            Sexo = "";
            Fecha_Nacimiento = DateTime.Now;
            Mensaje = "";
        }
        string mensaje;
        public string Mensaje { get => mensaje; set { Set(ref mensaje, value); } }

        DateTime fecha_Nacimiento;
        public DateTime Fecha_Nacimiento { get => fecha_Nacimiento; set { Set(ref fecha_Nacimiento, value); } }

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

        string sexo;
        public string Sexo { get => sexo; set { Set(ref sexo, value); } }

        RelayCommand obtenerDetalleSocioCommand = null;
        public RelayCommand ObtenerDetalleSocioCommand
        {
            get => obtenerDetalleSocioCommand ?? (obtenerDetalleSocioCommand = new RelayCommand(async () =>
            {
                var (exito, detalles) = await bl.DetalleSocio(Id);
                if (detalles == null)
                {
                    DetallesSocio = new DetalleSocio();
                }
                else
                {
                    DetallesSocio = detalles;
                }
                switch (detalles.GUID_SEXO)
                {
                    case "3cf6a15f-8692-4fe2-bb53-6b8d33ff4fce": Sexo = "Mujer/Femenino"; 
                        break;
                    case "ff32e57f-e1b7-4f03-a75a-c6af65f47e88": Sexo = "Hombre/Masculino";
                        break;
                    default: break;
                }
                if (!(string.IsNullOrEmpty(detalles.FECHA_NACIMIENTO)))
                    Fecha_Nacimiento = Convert.ToDateTime(detalles.FECHA_NACIMIENTO);
                if (!(string.IsNullOrEmpty(detalles.NO_CLIENTE)))
                    NoCliente = detalles.NO_CLIENTE;
                if (detalles.RANKING != null)
                    Ranking = detalles.RANKING.ToString("0.0");
                NombreCompleto = $"{DetallesSocio.NOMBRE}{" "}{DetallesSocio.APELLIDO_1}{" "}{DetallesSocio.APELLIDO_2}";
            }));
        }

        RelayCommand<DetalleSocio> actualizaInfoCommand = null;
        public RelayCommand<DetalleSocio> ActualizaInfoCommand
        {
            get => actualizaInfoCommand ?? (actualizaInfoCommand = new RelayCommand<DetalleSocio>(async (DetalleSocio info) =>
            {
                switch (Sexo)
                {
                    case "Mujer/Femenino":
                        info.GUID_SEXO = "3cf6a15f-8692-4fe2-bb53-6b8d33ff4fce";
                        break;
                    case "Hombre/Masculino":
                        info.GUID_SEXO = "ff32e57f-e1b7-4f03-a75a-c6af65f47e88";
                        break;
                    default: break;
                }
                info.P_PWD = "";
                var (exito, respuesta) = await bl.ActualizaInfoSocio(Id, info);
                if (exito)
                {
                    Mensaje = "Registro exitoso.";
                }
                else
                {
                    Mensaje = "Problema interno del servidor.";
                }
            }));
        }

        RelayCommand<string> updatePasswordCommand = null;
        public RelayCommand<string> UpdatePasswordCommand
        {
            get => updatePasswordCommand ?? (updatePasswordCommand = new RelayCommand<string>(async (string password) =>
            {
                var c = Crypto.EncodePassword(password);
                DetalleSocio pwd = new DetalleSocio
                {
                    NOMBRE = "",
                    APELLIDO_1 = "",
                    APELLIDO_2 = "",
                    FECHA_NACIMIENTO = "",
                    GUID_SEXO = "",
                    TEL_NUMERO = "",
                    P_PWD = c
                };
                var (exito, respuesta) = await bl.ActualizaInfoSocio(Id, pwd);
            }));
        }
    }
}

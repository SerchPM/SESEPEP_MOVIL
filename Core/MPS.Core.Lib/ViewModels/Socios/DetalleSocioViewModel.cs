using Core.MVVM.Helpers;
using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.Core.Lib.OS;
using MPS.SharedAPIModel.Operaciones;
using MPS.SharedAPIModel.Socios;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using Sysne.Core.OS;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace MPS.Core.Lib.ViewModels.Socios
{
    public class DetalleSocioViewModel : ViewModelWithBL<SociosBL>
    {
        public DetalleSocioViewModel()
        {
            Id = Guid.Parse(Settings.Current.LoginInfo.Usr.Id);
            Ranking = "0.0";
            Mensaje = "";
        }

        #region Propiedades
        string mensaje;
        public string Mensaje { get => mensaje; set { Set(ref mensaje, value); } }

        private Guid id;
        public Guid Id { get => id; set { Set(ref id, value); } }

        DetalleSocio detallesSocio;
        public DetalleSocio DetallesSocio { get => detallesSocio; set { Set(ref detallesSocio, value); } }

        string nombreCompleto;
        public string NombreCompleto { get => nombreCompleto; set { Set(ref nombreCompleto, value); } }

        string ranking;
        public string Ranking { get => ranking; set { Set(ref ranking, value); } }

        private List<Sexo> sexos;
        public List<Sexo> Sexos { get => sexos; set => Set(ref sexos, value); }

        private Sexo sexoSelected = new Sexo();
        public Sexo SexoSelected { get => sexoSelected; set => Set(ref sexoSelected, value); }

        private string password;
        public string Password
        {
            get => password;
            set
            {
                Set(ref password, value);
                if (!string.IsNullOrEmpty(value))
                {
                    var validar = LetrasYNumeros(value);
                    if (validar && value.Length > 8)
                        MensajePassword = string.Empty;
                    else
                        MensajePassword = "La contraseña debe tener más de 8 caracteres";
                }
            }
        }

        private string passwordConfirm;
        public string PasswordConfirm
        {
            get => passwordConfirm;
            set
            {
                Set(ref passwordConfirm, value);
                if (!string.IsNullOrEmpty(value))
                {
                    var validar = LetrasYNumeros(value);
                    if (value.Equals(Password) && validar && value.Length > 8)
                    {
                        MensajePasswordConfirm = string.Empty;
                        PasswordCorrect = true;
                    }
                    else
                    {
                        MensajePasswordConfirm = "La contraseña no coincide";
                        PasswordCorrect = false;
                    }
                }
            }
        }

        private bool modal;
        public bool Modal { get => modal; set => Set(ref modal, value); }

        private string mensajePassword;
        public string MensajePassword { get => mensajePassword; set => Set(ref mensajePassword, value); }

        private string mensajePasswordConfirm;
        public string MensajePasswordConfirm { get => mensajePasswordConfirm; set => Set(ref mensajePasswordConfirm, value); }

        private bool passwordCorrect;
        public bool PasswordCorrect { get => passwordCorrect; set => Set(ref passwordCorrect, value); }

        private bool modalPassword;
        public bool ModalPassword { get => modalPassword; set => Set(ref modalPassword, value); }

        private bool enviarFechaDeNacimiento = true;
        public bool EnviarFechaDeNacimiento { get => enviarFechaDeNacimiento; set => Set(ref enviarFechaDeNacimiento, value); }

        private bool iOS;
        public bool IOS { get => iOS; set => Set(ref iOS, value); }
        #endregion

        #region Comandos
        RelayCommand obtenerDetalleSocioCommand = null;
        public RelayCommand ObtenerDetalleSocioCommand
        {
            get => obtenerDetalleSocioCommand ??= new RelayCommand(async () =>
            {
                Sexos = await (new OperacionesBL()).GetSexosAsync();
                DetallesSocio = await bl.DetalleSocio(Id.ToString());
                Ranking = DetallesSocio.RANKING.ToString("0.0");
                NombreCompleto = $"{DetallesSocio.NOMBRE} {DetallesSocio.APELLIDO_1} {DetallesSocio.APELLIDO_2}";
                if (DetallesSocio.GUID_SEXO != null && !DetallesSocio.GUID_SEXO.Equals(Guid.Empty))
                {
                    if (Sexos.Count > 0)
                        SexoSelected = Sexos.Where(w => w.GUID.Equals(DetallesSocio.GUID_SEXO)).FirstOrDefault();
                }
                if (string.IsNullOrEmpty(DetallesSocio.FECHA_NACIMIENTO))
                    DetallesSocio.FECHA_NACIMIENTO = DateTime.UtcNow.ToString();
            });
        }

        RelayCommand actualizaInfoCommand = null;
        public RelayCommand ActualizaInfoCommand
        {
            get => actualizaInfoCommand ??= new RelayCommand(async () =>
            {
                if (IOS)
                {
                    if (DetallesSocio != null && (string.IsNullOrEmpty(DetallesSocio.NOMBRE) || string.IsNullOrEmpty(DetallesSocio.APELLIDO_1) || string.IsNullOrEmpty(DetallesSocio.APELLIDO_2)
                                                  || string.IsNullOrEmpty(DetallesSocio.TEL_NUMERO) || string.IsNullOrEmpty(DetallesSocio.E_MAIL)))
                    {
                        Mensaje = "Faltan campos por capturar";
                        Modal = true;
                        return;
                    }
                }
                else
                {
                    if (DetallesSocio != null && (string.IsNullOrEmpty(DetallesSocio.NOMBRE) || string.IsNullOrEmpty(DetallesSocio.APELLIDO_1) || string.IsNullOrEmpty(DetallesSocio.APELLIDO_2)
                                                  || string.IsNullOrEmpty(DetallesSocio.TEL_NUMERO) || string.IsNullOrEmpty(DetallesSocio.E_MAIL) || SexoSelected == null || SexoSelected.GUID == Guid.Empty))
                    {
                        Mensaje = "Faltan campos por capturar";
                        Modal = true;
                        return;
                    }
                }

                if (string.IsNullOrEmpty(ValidarNumeroTlefonico()))
                {
                    DetallesSocio.GUID_SEXO = SexoSelected?.GUID;
                    if (!EnviarFechaDeNacimiento) DetallesSocio.FECHA_NACIMIENTO = null;
                    else DetallesSocio.FechaNacimiento = DateTime.Parse(DetallesSocio.FECHA_NACIMIENTO);
                    var (exito, respuesta) = await bl.ActualizaInfoSocio(Id, DetallesSocio);
                    if (exito)
                    {
                        Mensaje = respuesta;
                        Modal = true;
                    }
                    else
                    {
                        Mensaje = respuesta;
                        Modal = true;
                        DetallesSocio = await bl.DetalleSocio(Id.ToString());
                        Ranking = DetallesSocio.RANKING.ToString("0.0");
                        NombreCompleto = $"{DetallesSocio.NOMBRE} {DetallesSocio.APELLIDO_1} {DetallesSocio.APELLIDO_2}";
                    }
                }
                else
                    Modal = true;
            });
        }

        RelayCommand actualizarPasswordCommand = null;
        public RelayCommand ActualizarPasswordCommand
        {
            get => actualizarPasswordCommand ??= new RelayCommand(async () =>
            {
                var passwordCrypto = Crypto.EncodePassword(PasswordConfirm);
                var (result, mensajeResult) = await bl.ActualizarPasswordSocioAsync(Id, passwordCrypto);
                if (result)
                {
                    MensajePassword = string.Empty;
                    MensajePasswordConfirm = string.Empty;
                    PasswordConfirm = string.Empty;
                    Password = string.Empty;
                    PasswordCorrect = false;
                    ModalPassword = false;
                    Mensaje = mensajeResult;
                    Modal = true;
                }
                else
                {
                    Mensaje = mensajeResult;
                    Modal = true;
                }
            });
        }

        private RelayCommand regresarCommand = null;
        public RelayCommand RegresarCommand
        {
            get => regresarCommand ??= new RelayCommand(async () =>
            {
                await DependencyService.Get<INavigationService>().GoBack();
            });
        }

        private RelayCommand modalPasswordCommand = null;
        public RelayCommand ModalPasswordCommand
        {
            get => modalPasswordCommand ??= new RelayCommand(() =>
            {
                PasswordCorrect = false;
                Password = string.Empty;
                PasswordConfirm = string.Empty;
                ModalPassword = true;
            });
        }

        private RelayCommand ocultarModalPaswwordCommand = null;
        public RelayCommand OcultarModalPaswwordCommand
        {
            get => ocultarModalPaswwordCommand ??= new RelayCommand(() =>
            {
                MensajePassword = string.Empty;
                MensajePasswordConfirm = string.Empty;
                ModalPassword = false;
            });
        }

        private RelayCommand ocultarModalCommand = null;
        public RelayCommand OcultarModalCommand
        {
            get => ocultarModalCommand ??= new RelayCommand(() =>
            {
                Modal = false;
            });
        }
        #endregion

        #region Metodos
        string ValidarNumeroTlefonico()
        {
            Mensaje = string.Empty;
            var telefonoValido = Regex.IsMatch(DetallesSocio.TEL_NUMERO, @"^\d{10}$");
            if (!telefonoValido)
                return Mensaje = "Teléfono inválido";
            return Mensaje;
        }

        /// <summary>
        /// Método que valida letras y números a partir de una cadena.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool LetrasYNumeros(string s)
        {
            return Regex.IsMatch(s, "^[a-zA-Z0-9]+");
        }
        #endregion
    }
}

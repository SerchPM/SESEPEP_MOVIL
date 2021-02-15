using Core.MVVM.Helpers;
using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.Core.Lib.OS;
using MPS.SharedAPIModel.Clientes;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using Sysne.Core.OS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using MPS.SharedAPIModel.Operaciones;
using System.Linq;

namespace MPS.Core.Lib.ViewModels.Clientes
{
    public class PerfilViewModel : ViewModelWithBL<ClientesBL>
    {
        #region Constructor
        public PerfilViewModel()
        {
        }
        #endregion

        #region Propeidades
        private ClienteDetalle cliente = new ClienteDetalle();
        public ClienteDetalle Cliente { get => cliente; set => Set(ref cliente, value); }

        private string nombreCopleto;
        public string NombreCopleto { get => nombreCopleto; set => Set(ref nombreCopleto, value); }

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
                    if (validar && value.Length > 6)
                        MensajePassword = string.Empty;
                    else
                        MensajePassword = "La contraseña debe tener más de 6 caracteres";
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
                    if (value.Equals(Password) && validar && value.Length > 6)
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

        private string mensaje;
        public string Mensaje { get => mensaje; set => Set(ref mensaje, value); }

        private string mensajePassword;
        public string MensajePassword { get => mensajePassword; set => Set(ref mensajePassword, value); }

        private string mensajePasswordConfirm;
        public string MensajePasswordConfirm { get => mensajePasswordConfirm; set => Set(ref mensajePasswordConfirm, value); }

        private bool passwordCorrect;
        public bool PasswordCorrect { get => passwordCorrect; set => Set(ref passwordCorrect, value); }

        private bool modalPassword;
        public bool ModalPassword { get => modalPassword; set => Set(ref modalPassword, value); }

        private List<Sexo> sexos;
        public List<Sexo> Sexos { get => sexos; set => Set(ref sexos, value); }

        private Sexo sexoSelected = new Sexo();
        public Sexo SexoSelected { get => sexoSelected; set => Set(ref sexoSelected, value); }

        private bool enviarFechaDeNacimiento = true;
        public bool EnviarFechaDeNacimiento { get => enviarFechaDeNacimiento; set => Set(ref enviarFechaDeNacimiento, value); }

        private bool iOS;
        public bool IOS { get => iOS; set => Set(ref iOS, value); }
        #endregion

        #region Comandos
        private RelayCommand obtenerClienteCommand = null;
        public RelayCommand ObtenerClienteCommand
        {
            get => obtenerClienteCommand ??= new RelayCommand(async () =>
            {
                Sexos = await (new OperacionesBL()).GetSexosAsync();
                Cliente = await bl.GetClienteAsync(Guid.Parse(Settings.Current.LoginInfo.Usr.Id));
                NombreCopleto = $"{Cliente.NOMBRE} {Cliente.APELLIDO_1} {Cliente.APELLIDO_2}";
                if (Cliente.GUID_SEXO != null && !Cliente.GUID_SEXO.Equals(Guid.Empty))
                {
                    if (Sexos.Count > 0)
                        SexoSelected = Sexos.Where(w => w.GUID.Equals(Cliente.GUID_SEXO)).FirstOrDefault();
                }
                if (string.IsNullOrEmpty(Cliente.FECHA_NACIMIENTO))
                    Cliente.FechaNacimiento = DateTime.UtcNow;
                else
                    if (DateTime.TryParse(Cliente.FECHA_NACIMIENTO, out DateTime fechaNacimiento))
                    Cliente.FechaNacimiento = fechaNacimiento;
            });
        }

        private RelayCommand actualziarInfoClienteCommand = null;
        public RelayCommand ActualziarInfoClienteCommand
        {
            get => actualziarInfoClienteCommand ??= new RelayCommand(async () =>
            {
                Mensaje = string.Empty;
                if (IOS)
                {
                    if (Cliente != null && (string.IsNullOrEmpty(Cliente.NOMBRE) || string.IsNullOrEmpty(Cliente.APELLIDO_1) || string.IsNullOrEmpty(Cliente.APELLIDO_2)
                                                  || string.IsNullOrEmpty(Cliente.TELEFONO) || string.IsNullOrEmpty(Cliente.CORREO_ELECTRONICO)))
                    {
                        Mensaje = "Faltan campos por capturar";
                        Modal = true;
                        return;
                    }
                }
                else
                {
                    if (Cliente != null && (string.IsNullOrEmpty(Cliente.NOMBRE) || string.IsNullOrEmpty(Cliente.APELLIDO_1) || string.IsNullOrEmpty(Cliente.APELLIDO_2)
                                                  || string.IsNullOrEmpty(Cliente.TELEFONO) || string.IsNullOrEmpty(Cliente.CORREO_ELECTRONICO) || SexoSelected == null || SexoSelected.GUID == Guid.Empty))
                    {
                        Mensaje = "Faltan campos por capturar";
                        Modal = true;
                        return;
                    }
                }

                if (string.IsNullOrEmpty(ValidarNumeroTlefonico()))
                {
                    Cliente.GUID_SEXO = SexoSelected?.GUID;
                    if (!EnviarFechaDeNacimiento) Cliente.FECHA_NACIMIENTO = null;
                    else Cliente.FECHA_NACIMIENTO = Cliente.FechaNacimiento.ToString("MM-dd-yyyy");
                    var (result, mensajeResult) = await bl.AtualziarClienteAsync(Guid.Parse(Settings.Current.LoginInfo.Usr.Id), Cliente);
                    if (result)
                    {
                        Mensaje = mensajeResult;
                        Modal = true;
                    }
                    else
                    {
                        Mensaje = mensajeResult;
                        Modal = true;
                        Cliente = await bl.GetClienteAsync(Guid.Parse(Settings.Current.LoginInfo.Usr.Id));
                        NombreCopleto = $"{Cliente.NOMBRE} {Cliente.APELLIDO_1} {Cliente.APELLIDO_2}";
                        if (Cliente.GUID_SEXO != null && !Cliente.GUID_SEXO.Equals(Guid.Empty))
                        {
                            if (Sexos.Count > 0)
                                SexoSelected = Sexos.Where(w => w.GUID.Equals(Cliente.GUID_SEXO)).FirstOrDefault();
                        }
                        if (string.IsNullOrEmpty(Cliente.FECHA_NACIMIENTO))
                            Cliente.FechaNacimiento = DateTime.UtcNow;
                        else
                            Cliente.FechaNacimiento = DateTime.Parse(Cliente.FECHA_NACIMIENTO);
                    }
                }
                else
                    Modal = true;
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

        private RelayCommand actualziarPasswordCommand = null;
        public RelayCommand ActualziarPasswordCommand
        {
            get => actualziarPasswordCommand ??= new RelayCommand(async () =>
            {
                var passwordCrypto = Crypto.EncodePassword(PasswordConfirm);
                var (result, mensajeResult) = await bl.ActualziarPasswordAsync(Guid.Parse(Settings.Current.LoginInfo.Usr.Id), passwordCrypto);
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

        private RelayCommand regresarCommand = null;
        public RelayCommand RegresarCommand
        {
            get => regresarCommand ??= new RelayCommand(async () =>
            {
                await DependencyService.Get<INavigationService>().GoBack();
            });
        }
        #endregion

        #region Metodos
        string ValidarNumeroTlefonico()
        {
            Mensaje = string.Empty;
            var telefonoValido = Regex.IsMatch(Cliente.TELEFONO, @"^\d{10}$");
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

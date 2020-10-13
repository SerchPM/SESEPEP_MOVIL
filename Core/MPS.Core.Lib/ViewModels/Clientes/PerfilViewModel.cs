using Core.MVVM.Helpers;
using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Clientes;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
        private Cliente cliente = new Cliente();
        public Cliente Cliente { get => cliente; set => Set(ref cliente, value); }

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
                    if (validar && value.Length > 8)
                        MensajePassword = string.Empty;
                    else
                        MensajePassword = "Password invalido";
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
                        MensajePasswordConfirm = "El password no coincide";
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
        #endregion

        #region Comandos
        private RelayCommand obtenerClienteCommand = null;
        public RelayCommand ObtenerClienteCommand
        {
            get => obtenerClienteCommand ??= new RelayCommand(async () =>
            {
                Cliente = await bl.GetClienteAsync(Guid.Parse(Settings.Current.LoginInfo.Usr.Id));
                NombreCopleto = $"{Cliente.NOMBRE} {Cliente.APELLIDO_1} {Cliente.APELLIDO_2}";
            });
        }

        private RelayCommand actualziarInfoClienteCommand = null;
        public RelayCommand ActualziarInfoClienteCommand
        {
            get => actualziarInfoClienteCommand ??= new RelayCommand(async () =>
            {
                Mensaje = string.Empty;
                var validar = ValidarNumeroTlefonico();
                if (string.IsNullOrEmpty(validar))
                {
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

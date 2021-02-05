using Core.MVVM.Helpers;
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
    public class PagoViewModel : ViewModelWithBL<SociosBL>
    {
        #region Constructor
        public PagoViewModel()
        {
            Id = Guid.Parse(Settings.Current.LoginInfo.Usr.Id);
        }
        #endregion

        #region Propiedades
        private Guid id;
        public Guid Id { get => id; set { Set(ref id, value); } }

        private DatoBancario datosBancarios;
        public DatoBancario DatosBancarios { get => datosBancarios; set => Set(ref datosBancarios, value); }

        private bool modal;
        public bool Modal { get => modal; set => Set(ref modal, value); }

        private string mensaje;
        public string Mensaje { get => mensaje; set => Set(ref mensaje, value); }
        #endregion

        #region Comandos
        RelayCommand obtenerDatosBancariosCommand = null;
        public RelayCommand ObtenerDatosBancariosCommand
        {
            get => obtenerDatosBancariosCommand ??= new RelayCommand(async () =>
            {
                var (estatus, datosBancarios) = await bl.ObtenerDatosBancariosSociosAsync(Id);
                if (!estatus)
                {
                    Mensaje = "No se encontraron los datos bancarios";
                    Modal = true;
                    DatosBancarios = new DatoBancario();
                }
                else
                    DatosBancarios = datosBancarios;
            });
        }


        private RelayCommand actualizarDatosBancariosCommand = null;
        public RelayCommand ActualizarDatosBancariosCommand
        {
            get => actualizarDatosBancariosCommand ??= new RelayCommand(async () =>
            {
                if (string.IsNullOrEmpty(DatosBancarios.NUMERO_TARJETA) || string.IsNullOrEmpty(DatosBancarios.CUENTA_BANCO))
                {
                    Mensaje = "Faltan campos por capturar, verifique su información";
                    Modal = true;
                    return;
                }
                if(DatosBancarios.NUMERO_TARJETA.Length < 10)
                {
                    Mensaje = "El campo No.Cuenta/Tarjeta no es valido";
                    Modal = true;
                    return;
                }
                if (DatosBancarios.CUENTA_BANCO.Length < 10)
                {
                    Mensaje = "El campo Banco no es valido";
                    Modal = true;
                    return;
                }

                var numeroTarjeta = Crypto.Encrypt(DatosBancarios.NUMERO_TARJETA);
                var numeroCuenta = Crypto.Encrypt(DatosBancarios.CUENTA_BANCO);

                var result = await bl.ActualizarTarjetaAsync(DatosBancarios.GUID_TARJETA, numeroTarjeta);
                var resultBanco = await bl.ActualizarBancoAsync(Id, numeroCuenta);

                if(!result || !resultBanco)
                {
                    Mensaje = "Ocurrio un porblema al actualizar la datos bancarios";
                    Modal = true;
                }
                else
                {
                    Mensaje = "Datos actualizados correctamente";
                    Modal = true;
                }
            });
        }

        private RelayCommand ocultarModalCommand = null;
        public RelayCommand OcultarModalCommand
        {
            get => ocultarModalCommand ??= new RelayCommand(() =>
            {
                Modal = false;
                Mensaje = string.Empty;
            });
        }
        #endregion
    }
}

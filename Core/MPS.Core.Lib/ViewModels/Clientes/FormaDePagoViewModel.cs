using Core.MVVM.Helpers;
using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Clientes;
using Openpay;
using Openpay.Entities;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPS.Core.Lib.ViewModels.Clientes
{
    public class FormaDePagoViewModel : ViewModelWithBL<ClientesBL>
    {
        #region Constructor
        public FormaDePagoViewModel()
        {
        }
        #endregion

        #region Propeidades
        OperacionesBL operacionesBL;
        OperacionesBL OperacionesBL => operacionesBL ??= new OperacionesBL();

        private bool modalRegistro;
        public bool ModalRegistro { get => modalRegistro; set => Set(ref modalRegistro, value); }

        private bool modal;
        public bool Modal { get => modal; set => Set(ref modal, value); }

        private string mensaje;
        public string Mensaje { get => mensaje; set => Set(ref mensaje, value); }

        private List<TarjetaCliente> tarjetasCliente = new List<TarjetaCliente>();
        public List<TarjetaCliente> TarjetasCliente { get => tarjetasCliente; set => Set(ref tarjetasCliente, value); }

        private NuevaTarjeta tarjeta = new NuevaTarjeta();
        public NuevaTarjeta Tarjeta { get => tarjeta; set => Set(ref tarjeta, value); }

        private List<Tarjeta> tarjetas = new List<Tarjeta>();
        public List<Tarjeta> Tarjetas { get => tarjetas; set => Set(ref tarjetas, value); }

        private Tarjeta tarjetaselected = new SharedAPIModel.Clientes.Tarjeta();
        public Tarjeta Tarjetaselected { get => tarjetaselected; set => Set(ref tarjetaselected, value); }

        private List<int> meses = new List<int>();
        public List<int> Meses { get => meses; set => Set(ref meses, value); }

        private int mesSelected;
        public int MesSelected { get => mesSelected; set => Set(ref mesSelected, value); }

        private List<int> años = new List<int>();
        public List<int> Años { get => años; set => Set(ref años, value); }

        private int añoSelected;
        public int AñoSelected { get => añoSelected; set => Set(ref añoSelected, value); }

        private List<TipoTarjeta> tiposTarjeta;
        public List<TipoTarjeta> TiposTarjeta { get => tiposTarjeta; set => Set(ref tiposTarjeta, value); }

        private TipoTarjeta tipoTarjetaSelected = new TipoTarjeta();
        public TipoTarjeta TipoTarjetaSelected { get => tipoTarjetaSelected; set => Set(ref tipoTarjetaSelected, value); }
        #endregion

        #region Comandos

        private RelayCommand obtenerComponentesCommand = null;
        public RelayCommand ObtenerComponentesCommand
        {
            get => obtenerComponentesCommand ??= new RelayCommand(async () =>
            {
                var mesesList = new List<int>();
                var añosList = new List<int>();
                var tiposTarjetaList = new List<TipoTarjeta>();
                for (int i = 1; i <= 12; i++)
                    mesesList.Add(i);
                var año = DateTime.Now.Year;
                for (int i = año; i <= (año + 10); i++)
                    añosList.Add(i);
                tiposTarjetaList.Add(new TipoTarjeta { Id = 1, Descripcion = "DÉBITO" });
                tiposTarjetaList.Add(new TipoTarjeta { Id = 2, Descripcion = "CRÉDITO" });
                TiposTarjeta = new List<TipoTarjeta>(tiposTarjetaList);
                Meses = new List<int>(mesesList);
                Años = new List<int>(añosList);
                Tarjetas = await OperacionesBL.GetTarjetasAsync();
                TarjetasCliente = await bl.GetTarjetasClienteAsync(Guid.Parse(Settings.Current.LoginInfo.Usr.Id));
            });
        }

        private RelayCommand registrarFormaDePagoCommand = null;
        public RelayCommand RegistrarFormaDePagoCommand
        {
            get => registrarFormaDePagoCommand ??= new RelayCommand(async () =>
            {
                if (string.IsNullOrEmpty(Tarjeta.NoCuenta) || string.IsNullOrEmpty(Tarjeta.CVV) || MesSelected == 0 || AñoSelected == 0
                 || (TipoTarjetaSelected != null && TipoTarjetaSelected.Id == 0) || (Tarjetaselected != null && Tarjetaselected.GUID == Guid.Empty))
                {
                    Mensaje = "Faltan campos por capturar,\nverifique su información";
                    Modal = true;
                    return;
                }
                
                if(Tarjeta.CVV.Length < 3)
                {
                    Mensaje = "El campo CVV no es valido";
                    Modal = true;
                    return;
                }
                if (Tarjeta.NoCuenta.Length < 10)
                {
                    Mensaje = "El campo No.Cuenta/Tarjeta no es valido";
                    Modal = true;
                    return;
                }
                var conf = await OperacionesBL.InfoConstantAsync();
                if (conf != null && !string.IsNullOrEmpty(conf.VAL1) && !string.IsNullOrEmpty(conf.VAL2))
                {
                    var openpayAPI = new OpenpayAPI(conf.VAL1, conf.VAL2)
                    {
                        Production = false
                    };
                    var request = new Card
                    {
                        HolderName = Settings.Current.LoginInfo.details.nameid,
                        CardNumber = Tarjeta.NoCuenta,
                        Cvv2 = Tarjeta.CVV,
                        ExpirationMonth = MesSelected.ToString(),
                        ExpirationYear = AñoSelected.ToString().Substring((AñoSelected.ToString().Length - 2), 2),
                    };

                    var card = openpayAPI.CardService.Create(request);
                    if (card != null)
                    {
                        var errorMensaje = Utilidades.ErrorRegistroOpenpay(card.ErrorCode);
                        if (string.IsNullOrEmpty(errorMensaje))
                        {
                            if (!string.IsNullOrEmpty(card.Id))
                            {
                                var (result, mensajeResponse) = await bl.RegistrarTarjetaAsync(new NuevaTarjeta
                                {
                                    MesExpira = MesSelected,
                                    AñoExpira = AñoSelected,
                                    IdMarca = Tarjetaselected.GUID,
                                    Tipo = TipoTarjetaSelected.Id,
                                    NoCuenta = Crypto.Encrypt(Tarjeta.NoCuenta),
                                    CVV = Crypto.Encrypt(Tarjeta.CVV),
                                    IdCliente = Guid.Parse(Settings.Current.LoginInfo.Usr.Id),
                                    IdTarjetaOpenpay = card.Id,
                                    TokenIdOpenpay = card.TokenId
                                });
                                if (result)
                                {
                                    ModalRegistro = false;
                                    Mensaje = mensajeResponse;
                                    Modal = true;
                                    TarjetasCliente.Clear();
                                    TarjetasCliente = await bl.GetTarjetasClienteAsync(Guid.Parse(Settings.Current.LoginInfo.Usr.Id));
                                    Tarjeta = new NuevaTarjeta();
                                    Tarjetaselected = new Tarjeta();
                                    TipoTarjetaSelected = new TipoTarjeta();
                                    MesSelected = 0;
                                    AñoSelected = 0;
                                }
                                else
                                {
                                    Mensaje = mensajeResponse;
                                    Modal = true;
                                }
                            }
                            else
                            {
                                Mensaje = "Ocurrió un problema inesperado, intente más tarde";
                                Modal = true;
                            }
                        }
                        else
                        {
                            Mensaje = errorMensaje;
                            Modal = true;
                        }
                    }
                    else
                    {
                        Mensaje = "Ocurrió un problema inesperado, intente más tarde";
                        Modal = true;
                    }
                }
                else
                {
                    Mensaje = "Ocurrió un problema inesperado, intente más tarde";
                    Modal = true;
                }
            });
        }

        private RelayCommand agregarTarjetaCommand = null;
        public RelayCommand AgregarTarjetaCommand
        {
            get => agregarTarjetaCommand ??= new RelayCommand(() =>
            {
                ModalRegistro = true;
            });
        }

        private RelayCommand ocultarModalRegistroCommand = null;
        public RelayCommand OcultarModalRegistroCommand
        {
            get => ocultarModalRegistroCommand ??= new RelayCommand(() =>
            {
                ModalRegistro = false;
                Tarjeta = new NuevaTarjeta();
                Tarjetaselected = new Tarjeta();
                TipoTarjetaSelected = new TipoTarjeta();
                MesSelected = 0;
                AñoSelected = 0;
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

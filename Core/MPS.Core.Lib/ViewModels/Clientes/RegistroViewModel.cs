using Sysne.Core.MVVM.Patterns;
using System;
using System.Collections.Generic;
using System.Text;
using MPS.SharedAPIModel.Operaciones;
using Sysne.Core.MVVM;
using MPS.SharedAPIModel.Clientes;
using Core.MVVM.Helpers;
using Sysne.Core.OS;
using MPS.Core.Lib.OS;
using System.Threading.Tasks;
using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel;

namespace MPS.Core.Lib.ViewModels.Clientes
{
    public class RegistroViewModel : ViewModelWithBL<BL.ClientesBL>
    {
        #region Eventos
        public static event EventHandler RegistroCliente;
        #endregion

        #region Pripiedades
        private BL.OperacionesBL operacionesBL;
        public BL.OperacionesBL OperacionesBL => operacionesBL ??= operacionesBL = new BL.OperacionesBL();

        private BL.SeguridadBL seguridadBL;
        public BL.SeguridadBL SeguridadBL => seguridadBL ??= seguridadBL = new BL.SeguridadBL();

        private Cliente cliente = new Cliente();// { FECHA_NACIMIENTO = DateTime.UtcNow};
        public Cliente Cliente { get => cliente; set => Set(ref cliente, value); }


        private bool enviarFechaDeNacimiento = true;
        public bool EnviarFechaDeNacimiento { get => enviarFechaDeNacimiento; set => Set(ref enviarFechaDeNacimiento, value); }

        private NuevaTarjeta tarjeta = new NuevaTarjeta();
        public NuevaTarjeta Tarjeta { get => tarjeta; set => Set(ref tarjeta, value); }

        private List<Sexo> sexos;
        public List<Sexo> Sexos { get => sexos; set => Set(ref sexos, value); }

        private Sexo sexoSelected = new Sexo();
        public Sexo SexoSelected { get => sexoSelected; set => Set(ref sexoSelected, value); }

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

        private bool registro = true;
        public bool Registro { get => registro; set => Set(ref registro, value); }

        private bool registroTarjeta;
        public bool RegistroTarjeta { get => registroTarjeta; set => Set(ref registroTarjeta, value); }

        private string mensaje;
        public string Mensaje { get => mensaje; set => Set(ref mensaje, value); }

        private bool modal;
        public bool Modal { get => modal; set => Set(ref modal, value); }

        private bool modalRegistro;
        public bool ModalRegistro { get => modalRegistro; set => Set(ref modalRegistro, value); }

        private string subtitulo = "Datos generales";
        public string Subtitulo { get => subtitulo; set => Set(ref subtitulo, value); }

        private bool enviarDatosVancarios = true;
        public bool EnviarDatosVancarios { get => enviarDatosVancarios; set => Set(ref enviarDatosVancarios, value); }

        private bool iOS;
        public bool IOS { get => iOS; set => Set(ref iOS, value); }
        #endregion

        #region Commandos
        private RelayCommand obtenerComponentesCommand = null;
        public RelayCommand ObtenerComponentesCommand
        {
            get => obtenerComponentesCommand ??= new RelayCommand(async () =>
            {
                await SeguridadBL.IniciarSesiónToken();
                Sexos = await OperacionesBL.GetSexosAsync();
                var mesesList = new List<int>();
                var añosList = new List<int>();
                var tiposTarjetaList = new List<TipoTarjeta>();
                for (int i = 1; i <= 12; i++)
                    mesesList.Add(i);
                var año = DateTime.Now.Year;
                for (int i = año; i <= (año + 10); i++)
                    añosList.Add(i);
                tiposTarjetaList.Add(new TipoTarjeta { Id = 1, Descripcion = "Débito" });
                tiposTarjetaList.Add(new TipoTarjeta { Id = 2, Descripcion = "Crédito" });
                TiposTarjeta = new List<TipoTarjeta>(tiposTarjetaList);
                Meses = new List<int>(mesesList);
                Años = new List<int>(añosList);
                Tarjetas = await OperacionesBL.GetTarjetasAsync();
            });
        }

        private RelayCommand registrarClienteCommand = null;
        public RelayCommand RegistrarClienteCommand
        {
            get => registrarClienteCommand ??= new RelayCommand(async () =>
            {
                if (Registro)
                {
                    if (IOS)
                    {
                        if (!string.IsNullOrEmpty(Cliente.NOMBRE) && !string.IsNullOrEmpty(Cliente.APELLIDO_1) && !string.IsNullOrEmpty(Cliente.APELLIDO_2) && !string.IsNullOrEmpty(Cliente.Alias) &&
                   !string.IsNullOrEmpty(Cliente.Password) && !string.IsNullOrEmpty(Cliente.CORREO_ELECTRONICO))
                        {
                            if (string.IsNullOrEmpty(ValidarCliente()))
                            {
                                Subtitulo = "Datos bancarios";
                                Registro = false;
                                RegistroTarjeta = true;
                            }
                            else
                                Modal = true;
                        }
                        else
                        {
                            Mensaje = "Faltan campos por capturar";
                            Modal = true;
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Cliente.NOMBRE) && !string.IsNullOrEmpty(Cliente.APELLIDO_1) && !string.IsNullOrEmpty(Cliente.APELLIDO_2) && !string.IsNullOrEmpty(Cliente.Alias) &&
                            SexoSelected != null && SexoSelected.GUID != Guid.Empty && !string.IsNullOrEmpty(Cliente.Password) && !string.IsNullOrEmpty(Cliente.CORREO_ELECTRONICO))
                        {
                            if (string.IsNullOrEmpty(ValidarCliente()))
                            {
                                Subtitulo = "Datos bancarios";
                                Registro = false;
                                RegistroTarjeta = true;
                            }
                            else
                                Modal = true;
                        }
                        else
                        {
                            Mensaje = "Faltan campos por capturar";
                            Modal = true;
                        }
                    }
                   
                }
                else
                {
                    if (EnviarDatosVancarios)
                    {
                        if (string.IsNullOrEmpty(Tarjeta.NoCuenta) || string.IsNullOrEmpty(Tarjeta.CVV) || MesSelected == 0 || AñoSelected == 0
                                                || (TipoTarjetaSelected != null && TipoTarjetaSelected.Id == 0) || (Tarjetaselected != null && Tarjetaselected.GUID == Guid.Empty))
                        {
                            Mensaje = "Faltan campos por capturar";
                            Modal = true;
                            return;
                        }

                        if (Tarjeta.CVV.Length < 3)
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
                    }
                    
                    Cliente.ModeloDispositivo = Helpers.Settings.Current.ModeloDispositivo;
                    Cliente.Password = Crypto.EncodePassword(Cliente.Password);
                    Cliente.VercionApp = "0";
                    Cliente.SEXO = SexoSelected?.GUID.ToString();
                    if (!EnviarFechaDeNacimiento) Cliente.FECHA_NACIMIENTO = null;
                    Cliente.IdMetodoPago = Guid.Parse("4F717133-1F98-47CE-A874-DC902392E706");
                    Cliente.API_KEY_MOVIL = Settings.Current.PlayerId;
                    var (result, (mensaje, idCliente)) = await bl.RegistrarClienteAsync(Cliente);
                    if (result && EnviarDatosVancarios)
                    {
                        Tarjeta.MesExpira = MesSelected;
                        Tarjeta.AñoExpira = AñoSelected;
                        Tarjeta.IdMarca = Tarjetaselected.GUID;
                        Tarjeta.Tipo = TipoTarjetaSelected.Id;
                        Tarjeta.NoCuenta = Crypto.Encrypt(tarjeta.NoCuenta);
                        Tarjeta.CVV = Crypto.Encrypt(tarjeta.CVV);
                        Tarjeta.IdCliente = idCliente;
                        var resultTarjeta = await bl.RegistrarTarjetaAsync(Tarjeta);
                        ModalRegistro = true;
                    }
                    else
                    {
                        Mensaje = mensaje;
                        Modal = true;
                    }
                }
            });
        }

        private RelayCommand<bool> regresarPasoCommand = null;
        public RelayCommand<bool> RegresarPasoCommand
        {
            get => regresarPasoCommand ??= new RelayCommand<bool>((paso) =>
            {
                if (!paso)
                {
                    Registro = true;
                    RegistroTarjeta = false;
                }
            });
        }

        private RelayCommand navegarLoginCommand = null;
        public RelayCommand NavegarLoginCommand
        {
            get => navegarLoginCommand ??= new RelayCommand(async () =>
            {
                await DependencyService.Get<INavigationService>().Home();
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

        private RelayCommand registrarClienteOneSignalCommand = null;
        public RelayCommand RegistrarClienteOneSignalCommand
        {
            get => registrarClienteOneSignalCommand ??= new RelayCommand(async () =>
            {
                if (Xamarin.Forms.Device.RuntimePlatform != Xamarin.Forms.Device.UWP)
                    RegistroCliente?.Invoke(this, new EventArgs());
                else
                    await RegistrarDispositivoUWPCliente();
            });
        }
        #endregion

        #region Metodos
        string ValidarCliente()
        {
            Mensaje = string.Empty;
            Cliente.CORREO_ELECTRONICO = Cliente.CORREO_ELECTRONICO.Trim();
            var formatoEmail = System.Text.RegularExpressions.Regex.IsMatch(Cliente.CORREO_ELECTRONICO, @"^([a-zA-Z0-9_\-\.]+)@([a-zA-Z0-9_\-\.]+)\.([a-zA-Z]{2,5})$");
            if (Cliente.Password.Length < 6)
                return Mensaje = "La contraseña debe tener más de 6 caracteres";
            if (!formatoEmail)
                return Mensaje = "Formato de correo incorrecto";
            if (!string.IsNullOrEmpty(Cliente.TELEFONO))
            {
                var telefonoValido = System.Text.RegularExpressions.Regex.IsMatch(Cliente.TELEFONO, @"^\d{10}$");
                if (!telefonoValido)
                    return Mensaje = "Teléfono inválido.";
            }

            return Mensaje;
        }

        public async Task<bool> RegistrarDispositivoUWPCliente()
        {
            var (result, playerId) = await (new OperacionesBL()).RegistrarDispositivoUWPAsync(Settings.Current.AppId, Settings.Current.ChannelUriUWP, Settings.Current.ModeloDispositivo);
            if (result)
                Settings.Current.PlayerId = playerId;
            return true;
        }
        #endregion
    }
}

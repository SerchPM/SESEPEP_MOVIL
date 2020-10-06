using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Clientes;
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

        private List<int> tarjetasCliente = new List<int>();
        public List<int> TarjetasCliente { get => tarjetasCliente; set => Set(ref tarjetasCliente, value); }

        private NuevaTarjeta tarjeta = new NuevaTarjeta();
        public NuevaTarjeta Tarjeta { get => tarjeta; set => Set(ref tarjeta, value); }

        private List<Tarjeta> tarjetas = new List<Tarjeta>();
        public List<Tarjeta> Tarjetas { get => tarjetas; set => Set(ref tarjetas, value); }

        private Tarjeta tarjetaselected = new SharedAPIModel.Clientes.Tarjeta();
        public Tarjeta Tarjetaselected
        { 
            get => tarjetaselected; 
            set => Set(ref tarjetaselected, value);
        }

        private List<int> meses = new List<int>();
        public List<int> Meses { get => meses; set => Set(ref meses, value); }

        private int mesSelected;
        public int MesSelected { get => mesSelected; set => Set(ref mesSelected, value); }

        private List<int> años = new List<int>();
        public List<int> Años { get => años; set => Set(ref años, value); }

        private int añoSelected;
        public int AñoSelected { get => añoSelected; set => Set(ref añoSelected, value); }
        #endregion

        #region Comandos

        private RelayCommand obtenerComponentesCommand = null;
        public RelayCommand ObtenerComponentesCommand
        {
            get => obtenerComponentesCommand ??= new RelayCommand(async () =>
            {
                var mesesList = new List<int>();
                var añosList = new List<int>();
                for (int i = 1; i <= 31; i++)
                    mesesList.Add(i);
                var año = DateTime.Now.Year;
                for (int i = año; i <= (año + 10); i++)
                    añosList.Add(i);
                Meses = new List<int>(mesesList);
                Años = new List<int>(añosList);
                MesSelected = Meses.FirstOrDefault();
                AñoSelected = Años.FirstOrDefault();
                Tarjetas = await OperacionesBL.GetTarjetasAsync();
                TarjetasCliente = await bl.GetTarjetasClienteAsync(Guid.Parse(Settings.Current.LoginInfo.Usr.Id));
            });
        }

        private RelayCommand registrarFormaDePagoCommand = null;
        public RelayCommand RegistrarFormaDePagoCommand
        {
            get => registrarFormaDePagoCommand ??= new RelayCommand(async () =>
            {
                Tarjeta.MesExpira = MesSelected;
                Tarjeta.AñoExpira = AñoSelected;
                Tarjeta.IdTarjeta = Tarjetaselected.GUID;
                ModalRegistro = false;
                Mensaje = "La tarjeta se agrego correctamente";
                Modal = true;
                TarjetasCliente.Clear();
                TarjetasCliente = await bl.GetTarjetasClienteAsync(Guid.Parse(Settings.Current.LoginInfo.Usr.Id));
                MesSelected = Meses.FirstOrDefault();
                AñoSelected = Años.FirstOrDefault();
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
                MesSelected = Meses.FirstOrDefault();
                AñoSelected = Años.FirstOrDefault();
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

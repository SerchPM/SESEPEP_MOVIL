using Core.MVVM.Helpers;
using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.Core.Lib.OS;
using MPS.SharedAPIModel;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using Sysne.Core.OS;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.ViewModels
{
    public class LoginViewModel : ViewModelWithBL<SeguridadBL>
    {

        #region Constructor
        public LoginViewModel()
        {
            Settings.Current.PaginaActual = "SolicitarServicioPage";
            if (!string.IsNullOrEmpty(Settings.Current.Usuario) && !string.IsNullOrEmpty(Settings.Current.Contraseña))
            {
                Usuario = Settings.Current.Usuario;
                Contraseña = Settings.Current.Contraseña;
            }
        }
        #endregion

        #region Propiedades
        private string usuario;
        public string Usuario { get => usuario; set => Set(ref usuario, value); }

        private string contraseña;
        public string Contraseña { get => contraseña; set => Set(ref contraseña, value); }

        private string mensaje;
        public string Mensaje { get => mensaje; set => Set(ref mensaje, value); }

        private string versionApp;
        public string VersionApp { get => versionApp; set => Set(ref versionApp, value); }
        #endregion

        #region Comandos
        RelayCommand loginSocioCommand = null;
        public RelayCommand LoginSocioCommand
        {
            get => loginSocioCommand ??= new RelayCommand(async () =>
            {
                Mensaje = string.Empty;
                if(!string.IsNullOrEmpty(Usuario) && !string.IsNullOrEmpty(Contraseña))
                {
                    var passwordCrypto = Crypto.EncodePassword(Contraseña);
                    var (Válido, mensaje) = await bl.IniciarSesiónSocio(Usuario, Contraseña, passwordCrypto);
                    if (Válido)
                        await DependencyService.Get<INavigationService>().NavigateTo(PagesKeys.SolicitarServicio);
                    else
                        Mensaje = mensaje;
                }
            }, () => Validate(this, false)
                , dependencies: (this, new[] { nameof(Usuario), nameof(Contraseña) }));
        }

        RelayCommand loginClienteCommand = null;
        public RelayCommand LoginClienteCommand
        {
            get => loginClienteCommand ??= new RelayCommand(async () =>
            {
                Mensaje = string.Empty;
                if (!string.IsNullOrEmpty(Usuario) && !string.IsNullOrEmpty(Contraseña))
                {
                    var passwordCrypto = Crypto.EncodePassword(Contraseña);
                    var (Válido, mensaje) = await bl.IniciarSesiónCliente(Usuario, Contraseña, passwordCrypto);
                    if (Válido)
                        await DependencyService.Get<INavigationService>().NavigateTo(PagesKeys.SolicitarServicio);
                    else
                        Mensaje = mensaje;
                }
            }, () => Validate(this, false)
                , dependencies: (this, new[] { nameof(Usuario), nameof(Contraseña) }));
        }

        private RelayCommand registrarClienteCommand = null;
        public RelayCommand RegistrarClienteCommand
        {
            get => registrarClienteCommand ??= new RelayCommand(async () =>
            {
                await DependencyService.Get<INavigationService>().NavigateTo(PagesKeys.Registro);
            });
        }
        #endregion
    }
}
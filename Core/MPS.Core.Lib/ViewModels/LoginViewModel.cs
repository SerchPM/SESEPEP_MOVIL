using Core.MVVM.Helpers;
using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using MPS.Core.Lib.OS;
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
        public LoginViewModel()
        {
            if (!string.IsNullOrEmpty(Settings.Current.Usuario) && !string.IsNullOrEmpty(Settings.Current.Contraseña))
            {
                Usuario = Settings.Current.Usuario;
                Contraseña = Settings.Current.Contraseña;
            }
        }

        private string usuario;
        public string Usuario { get => usuario; set => Set(ref usuario, value); }

        private string contraseña;
        public string Contraseña { get => contraseña; set => Set(ref contraseña, value); }

        private string mensaje;
        public string Mensaje { get => mensaje; set => Set(ref mensaje, value); }

        RelayCommand loginCommand = null;
        public RelayCommand LoginCommand
        {
            get => loginCommand ??= new RelayCommand(async () =>
            {
                Mensaje = string.Empty;
                var passwordCrypto = Crypto.EncodePassword(Contraseña);
                var (Válido, Info) = await bl.IniciarSesión(Usuario, Contraseña, passwordCrypto);
                if (Válido)
                    await DependencyService.Get<INavigationService>().NavigateTo(PagesKeys.SolicitarServicio);
                else
                    Mensaje = "Usuario y/o contraseña incorrectos";
            }, () => Validate(this, false)
                , dependencies: (this, new[] { nameof(Usuario), nameof(Contraseña) }));
        }
    }
}
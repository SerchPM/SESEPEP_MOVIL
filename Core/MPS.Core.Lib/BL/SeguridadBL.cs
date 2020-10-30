using MPS.Core.Lib.ApiClient;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel;
using MPS.SharedAPIModel.Seguridad;
using Sysne.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.BL
{
    public class SeguridadBL : ViewModelBase
    {
        public static event EventHandler Autentificado;

        public async Task<(bool Válido,LoginResponse Info)> IniciarSesión(string usuario, string contraseña, string contraseñaCrýpto, bool mantenerSesiónActiva = true)
        {
            var (StatusCode, LoginInfo) = await (new SeguridadApi()).LoginAsync(usuario, contraseñaCrýpto);
            var válido = StatusCode == System.Net.HttpStatusCode.OK;
            if (válido)
            {
                if (mantenerSesiónActiva)
                {
                    Settings.Current.Usuario = usuario;
                    Settings.Current.Contraseña = contraseña;
                }
                Settings.Current.LoginInfo = LoginInfo;
                if (Xamarin.Forms.Device.RuntimePlatform != Xamarin.Forms.Device.UWP)
                    Autentificado?.Invoke(this, new EventArgs());
            }
            return (válido, LoginInfo);
        }

        public async Task<(bool Válido, LoginResponse Info)> IniciarSesiónSocio(string usuario, string contraseña, string contraseñaCrýpto, bool mantenerSesiónActiva = true)
        {
            var (StatusCode, LoginInfo) = await (new SeguridadApi()).LoginAsync(usuario, contraseñaCrýpto);
            var válido = StatusCode == System.Net.HttpStatusCode.OK;
            if (válido)
            {
                if (mantenerSesiónActiva)
                {
                    Settings.Current.Usuario = usuario;
                    Settings.Current.Contraseña = contraseña;
                }
                Settings.Current.LoginInfo = LoginInfo;
                if (Xamarin.Forms.Device.RuntimePlatform != Xamarin.Forms.Device.UWP)
                    Autentificado?.Invoke(this, new EventArgs());
            }
            return (válido, LoginInfo);
        }
    }
}

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
        #region Constructor
        public static event EventHandler Autentificado;
        #endregion

        #region Metodos
        public async Task<(bool Válido,string mensaje)> IniciarSesiónSocio(string usuario, string contraseña, string contraseñaCrýpto, bool mantenerSesiónActiva = true)
        {
            var (StatusCode, LoginInfo) = await (new SeguridadApi()).LoginAsync(usuario, contraseñaCrýpto);
            var válido = StatusCode == System.Net.HttpStatusCode.OK;
            if (válido)
            {
                if (LoginInfo.Usr.Estatus.Equals((int)EstatusEnum.Inactivo))
                    return (false, "Tu cuenta ha sido de baja");

                if (!LoginInfo.Usr.Rol.Equals((int)TipoRolEnum.Socio))
                    return (false, "Socio invalido");

                if(!LoginInfo.Usr.Fase.Equals((int)TipoFaseSocioEnum.Finalizado))
                    return (false, "Aún no has finalizado tu proceso de registro");

                if (mantenerSesiónActiva)
                {
                    Settings.Current.Usuario = usuario;
                    Settings.Current.Contraseña = contraseña;
                }
                Settings.Current.LoginInfo = LoginInfo;
                if (Xamarin.Forms.Device.RuntimePlatform != Xamarin.Forms.Device.UWP)
                    Autentificado?.Invoke(this, new EventArgs());
                else
                    await RegistrarDispositivoUWPSocio();
                return (válido, string.Empty);
            }
            else
                return (válido, "Usuario y/o contraseña incorrectos");
        }

        public async Task<(bool Válido, string mensaje)> IniciarSesiónCliente(string usuario, string contraseña, string contraseñaCrýpto, bool mantenerSesiónActiva = true)
        {
            var (StatusCode, LoginInfo) = await (new SeguridadApi()).LoginAsync(usuario, contraseñaCrýpto);
            var válido = StatusCode == System.Net.HttpStatusCode.OK;
            if (válido)
            {
                if (LoginInfo.Usr.Estatus.Equals((int)EstatusEnum.Inactivo))
                    return (false, "Tu cuenta ha sido de baja");

                if (!LoginInfo.Usr.Rol.Equals((int)TipoRolEnum.Cliente))
                    return (false, "Cliente invalido");

                if (!LoginInfo.Usr.Fase.Equals((int)TipoFaseClienteEnum.Completado))
                    return (false, "Aún no has finalizado tu proceso de registro");

                if (mantenerSesiónActiva)
                {
                    Settings.Current.Usuario = usuario;
                    Settings.Current.Contraseña = contraseña;
                }
                Settings.Current.LoginInfo = LoginInfo;
                if (Xamarin.Forms.Device.RuntimePlatform != Xamarin.Forms.Device.UWP)
                    Autentificado?.Invoke(this, new EventArgs());
                else
                    await RegistrarDispositivoUWPCliente();
                return (válido, string.Empty);
            }
            else
                return (válido, "Usuario y/o contraseña incorrectos");
        }

        public async Task<(bool Válido, LoginResponse Info)> IniciarSesiónToken()
        {
            var (StatusCode, LoginInfo) = await (new SeguridadApi()).LoginAsync("uncorreo@undominio.com", "PASSWORD123");
            var válido = StatusCode == System.Net.HttpStatusCode.OK;
            if (válido)
                Settings.Current.LoginInfo = LoginInfo;
            return (válido, LoginInfo);
        }

        public async Task<bool> RegistrarDispositivoUWPSocio()
        {
            var (result, playerId) = await (new OperacionesBL()).RegistrarDispositivoUWPAsync(Settings.Current.AppId, Settings.Current.ChannelUriUWP, Settings.Current.ModeloDispositivo);
            if (result)
            {
                await (new OperacionesBL()).RegistrarDispositivoAsync(new Dispositivo
                {
                    Id = Guid.Parse(Settings.Current.LoginInfo.Usr.Id),
                    Modelo = Settings.Current.ModeloDispositivo,
                    SO = Settings.Current.SO,
                    TipoDispositivo = Settings.Current.TipoDispositivo,
                    TipoUsuario = (int)TipoUsuarioEnum.Socio,
                    VercionApp = "0",
                    TimeZona = "-28800",
                    PlayerId = playerId
                });
            }
           
            return true;
        }

        public async Task<bool> RegistrarDispositivoUWPCliente()
        {
            var (result, playerId) = await (new OperacionesBL()).RegistrarDispositivoUWPAsync(Settings.Current.AppId, Settings.Current.ChannelUriUWP, Settings.Current.ModeloDispositivo);
            if (result)
            {
                await (new OperacionesBL()).RegistrarDispositivoAsync(new Dispositivo
                {
                    Id = Guid.Parse(Settings.Current.LoginInfo.Usr.Id),
                    Modelo = Settings.Current.ModeloDispositivo,
                    SO = Settings.Current.SO,
                    TipoDispositivo = Settings.Current.TipoDispositivo,
                    TipoUsuario = (int)TipoUsuarioEnum.Cliente,
                    VercionApp = "0",
                    TimeZona = "-28800",
                    PlayerId = playerId
                });
            }
            return true;
        }
        #endregion
    }
}

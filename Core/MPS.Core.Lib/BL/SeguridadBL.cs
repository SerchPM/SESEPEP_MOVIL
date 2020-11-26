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
                else
                    await RegistrarDispositivoUWP();
            }
            return (válido, LoginInfo);
        }

        public async Task<(bool Válido, LoginResponse Info)> IniciarSesiónToken()
        {
            var (StatusCode, LoginInfo) = await (new SeguridadApi()).LoginAsync("uncorreo@undominio.com", "PASSWORD123");
            var válido = StatusCode == System.Net.HttpStatusCode.OK;
            if (válido)
                Settings.Current.LoginInfo = LoginInfo;
            return (válido, LoginInfo);
        }

        public async Task<bool> RegistrarDispositivoUWP()
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
                    TipoUsuario = Settings.Current.AppId.Equals("66758264-d740-4a12-b963-d3eec52e9e64") ? (int)TipoUsuarioEnum.Cliente : (int)TipoUsuarioEnum.Socio,
                    VercionApp = "0",
                    TimeZona = "-28800",
                    PlayerId = playerId
                });
            }
           
            return true;
        }
    }
}

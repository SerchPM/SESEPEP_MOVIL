using MPS.Core.Lib.ApiClient;
using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Seguridad;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.BL
{
    public class SeguridadBL
    {
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
            }
            return (válido, LoginInfo);
        }
    }
}

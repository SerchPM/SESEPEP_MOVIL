using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Seguridad;
using Sysne.Core.ApiClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.ApiClient
{
    public class SeguridadApi : WebApiClient
    {
        public SeguridadApi() : base(Settings.Current.WebAPIUrl, "Login") { }

        public async Task<(HttpStatusCode StatusCode,LoginResponse LoginInfo)> LoginAsync(string usuario, string contraseña)
        {
            var res = await CallFormUrlEncoded<LoginResponse>("LoginAsync", HttpMethod.Post,
                ("P_USUARIO",usuario), 
                ("P_PWD", contraseña));

            return res;
        }
    }
}

using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Seguridad;
using MPS.SharedAPIModel.Socios;
using Sysne.Core.ApiClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.ApiSocio
{
    public class SociosApi : WebApiClient
    {
        public SociosApi() : base(Settings.Current.WebAPIUrl, "Socios") { }

        public async Task<(HttpStatusCode StatusCode, Respuesta Respuesta)> CrearSocioAsync(NuevoSocio nuevoSocio)
        {
            var res = await CallFormUrlEncoded<Respuesta>("CrearSocio", HttpMethod.Post,
                ("P_NOMBRE", nuevoSocio.P_NOMBRE),
                ("P_APELLIDO_1", nuevoSocio.P_APELLIDO_1),
                ("P_APELLIDO_2", nuevoSocio.P_APELLIDO_2),
                ("P_FECHA_NACIMIENTO", nuevoSocio.P_FECHA_NACIMIENTO),
                ("P_GUID_CIUDAD_OPERACIÓN", nuevoSocio.P_GUID_CIUDAD_OPERACIÓN.ToString()),
                ("P_GUID_PAIS", nuevoSocio.P_GUID_PAIS.ToString()),
                ("P_GUID_ESTADO_PROVINCIA", nuevoSocio.P_GUID_ESTADO_PROVINCIA.ToString()),
                ("P_GUID_MUNICIPIO", nuevoSocio.P_GUID_MUNICIPIO.ToString()),
                ("P_GUID_COLONIA", nuevoSocio.P_GUID_COLONIA.ToString()),
                ("P_CALLE", nuevoSocio.P_CALLE.ToString()),
                ("P_NO_EXT", nuevoSocio.P_NO_EXT.ToString()),
                ("P_NO_INT", nuevoSocio.P_NO_INT.ToString()),
                ("P_ALIAS", nuevoSocio.P_ALIAS),
                ("P_PWD", nuevoSocio.P_PWD),
                ("P_E_MAIL", nuevoSocio.P_E_MAIL),
                ("P_TEL_NUMERO", nuevoSocio.P_TEL_NUMERO.ToString()));
            return res;
        }

        public async Task<(HttpStatusCode StatusCode, Respuesta Respuesta)> ActualizaSocioAsync(Guid socio, NuevoSocio info, string img)
        {
            var res = await CallFormUrlEncoded<Respuesta>("ActualizaInfoSocio", HttpMethod.Post,
                ("P_GUID_SOCIO", socio.ToString()),
                ("P_NOMBRE", info.P_NOMBRE),
                ("P_APELLIDO_1", info.P_APELLIDO_1),
                ("P_APELLIDO_2", info.P_APELLIDO_2),
                ("P_FECHA_NACIMIENTO", info.P_FECHA_NACIMIENTO),
                ("P_SEXO", info.P_GUID_SEXO.ToString()),
                ("P_TEL_NUMERO", info.P_TEL_NUMERO.ToString()),
                ("P_IMAGEN", img));
            return res;
        }
    }
}

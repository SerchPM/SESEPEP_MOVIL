using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel;
using MPS.SharedAPIModel.Clientes;
using MPS.SharedAPIModel.Operaciones;
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
    public class OperacionesApi : WebApiClient
    {
        #region Constructor
        public OperacionesApi() : base(Settings.Current.WebAPIUrl, "Operaciones") { }
        #endregion

        #region Metodos

        /// <summary>
        /// Obtiene los diferentes tipos de servicios disponibles
        /// </summary>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, List<Servicio> catalogo)> GetServiciosAsync() =>
            await CallPostAsync<List<Servicio>>("ConsultaUnCatalogo", ("P_CATALOGO", "CAT_TIPO_SOLICITUD"));

        /// <summary>
        /// Obtiene los diferentes tipos tarjetas disponibles
        /// </summary>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, List<Tarjeta> catalogo)> GetTarjetasAsync() =>
            await CallPostAsync<List<Tarjeta>>("ConsultaUnCatalogo", ("P_CATALOGO", "CAT_MARCAS_TARJETAS"));

        /// <summary>
        /// Registra un nuevo dispositivo en OneSignal
        /// </summary>
        /// <param name="dispositivo"></param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, SharedAPIModel.Clientes.Response response)> RegistrarDispostivoAsync(Dispositivo dispositivo)
        {
            var res = await CallFormUrlEncoded<SharedAPIModel.Clientes.Response>("AgregarNuevoDispositivo", HttpMethod.Post,
                ("P_GUID_USUARIO", dispositivo.Id.ToString()),
                ("P_TIPO_USUARIO", dispositivo.TipoUsuario.ToString()),
                ("P_TIMEZONE", dispositivo.TimeZona),
                ("P_GAME_VERSION", dispositivo.VercionApp),
                ("P_DEVICE_TYPE", dispositivo.TipoDispositivo),
                ("P_DEVICE_MODEL", dispositivo.Modelo),
                ("P_DEVICE_OS", dispositivo.SO),
                ("P_KEY", dispositivo.PlayerId));
            return res;
        }

        /// <summary>
        /// Obtiene una lista de sexos
        /// </summary>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, List<Sexo> catalogo)> GetSexosAsync() =>
             await CallPostAsync<List<Sexo>>("ConsultaUnCatalogo", ("P_CATALOGO", "CAT_SEXO"));
        #endregion
    }
}

using MPS.Core.Lib.ApiClient;
using MPS.SharedAPIModel;
using MPS.SharedAPIModel.Clientes;
using MPS.SharedAPIModel.Operaciones;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.BL
{
    public class OperacionesBL
    {
        #region Propiedades
        private OperacionesApi operacionesApi;
        public OperacionesApi OperacionesApi => operacionesApi ??= new OperacionesApi();
        #endregion

        #region Metodos
        /// <summary>
        /// Obtiene los diferentes tipos tarjetas disponibles
        /// </summary>
        /// <returns></returns>
        public async Task<List<Tarjeta>> GetTarjetasAsync()
        {
            var (statusCode, resultado) = await OperacionesApi.GetTarjetasAsync();
            if (statusCode == HttpStatusCode.OK)
                return resultado;
            else
                return new List<Tarjeta>();
        }

        /// <summary>
        /// Registra o actualiza el dispositivo logeado
        /// </summary>
        /// <param name="dispositivo"></param>
        /// <returns></returns>
        public async Task<SharedAPIModel.Clientes.Response> RegistrarDispositivoAsync(Dispositivo dispositivo)
        {
            var (statusCode, resultado) = await OperacionesApi.RegistrarDispostivoAsync(dispositivo);
            if (statusCode == HttpStatusCode.OK)
                return resultado;
            else
                return new SharedAPIModel.Clientes.Response();
        }

        /// <summary>
        /// Obtiene una lista de sexos
        /// </summary>
        /// <returns></returns>
        public async Task<List<Sexo>> GetSexosAsync()
        {
            var (statusCode, resultado) = await OperacionesApi.GetSexosAsync();
            if (statusCode == HttpStatusCode.OK)
                return resultado;
            else
                return new List<Sexo>();
        }

        /// <summary>
        /// Registra un nuevo dispositivo UWP en OneSignal
        /// </summary>
        /// <param name="appId">Identificadro del grupo den OneSignal</param>
        /// <param name="uriChannel">Canal de comunicacion WNS</param>
        /// <param name="modelo">Modelo del dispositivo</param>
        public async Task<(bool, string)> RegistrarDispositivoUWPAsync(string appId, string uriChannel, string modelo)
        {
            var (statusCode, resultado) = await OperacionesApi.RegistrarDispositivoUWPAsync(appId, uriChannel, modelo);
            if (statusCode == HttpStatusCode.OK && resultado != null && resultado.Item1.Equals(200) && !string.IsNullOrEmpty(resultado.Item2.Id))
                return (true, resultado.Item2.Id);
            else
                return (false, string.Empty);
        }

        /// <summary>
        /// Obtiene los key's de Openpay
        /// </summary>
        /// <returns></returns>
        public async Task<InfoConstant> InfoConstantAsync()
        {
            var (statusCode, resultado) = await OperacionesApi.GetInfoConstant();
            if (statusCode == HttpStatusCode.OK)
                return resultado;
            else
                return new InfoConstant();
        }

        #endregion
    }
}

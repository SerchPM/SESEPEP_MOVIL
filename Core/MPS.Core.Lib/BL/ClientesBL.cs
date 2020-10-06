using MPS.Core.Lib.ApiClient;
using MPS.SharedAPIModel.Clientes;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.BL
{
    public class ClientesBL
    {
        #region Propiedades
        private ClientesApi clientesApi;
        public ClientesApi ClientesApi => clientesApi ??= new ClientesApi();
        #endregion

        #region Metodos
        /// <summary>
        /// Obtiene los diferentes tipos tarjetas disponibles
        /// </summary>
        /// <returns></returns>
        public async Task<List<int>> GetTarjetasClienteAsync(Guid idCliente)
        {
            var (statusCode, resultado) = await ClientesApi.GetTarjetasClienteAsync(idCliente);
            if (statusCode == HttpStatusCode.OK)
                return resultado;
            else
                return new List<int>();
        }
        #endregion
    }
}

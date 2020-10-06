using MPS.Core.Lib.Helpers;
using Sysne.Core.ApiClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.ApiClient
{
    public class ClientesApi : WebApiClient
    {
        #region Constructor
        public ClientesApi() : base(Settings.Current.WebAPIUrl, "Clientes") { }
        #endregion

        #region Métodos
        /// <summary>
        /// 
        /// </summary>
        /// <param name="idCliente"></param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, List<int> tarjetas)> GetTarjetasClienteAsync(Guid idCliente) =>
            await CallPostAsync<List<int>>("ConsultaTarjetasClientes", ("P_GUID_CLIENTE", idCliente));
        #endregion
    }
}

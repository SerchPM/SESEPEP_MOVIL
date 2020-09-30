using MPS.Core.Lib.Helpers;
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
        #endregion
    }
}

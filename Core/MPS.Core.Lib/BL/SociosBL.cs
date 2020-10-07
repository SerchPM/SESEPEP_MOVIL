using MPS.Core.Lib.ApiSocio;
using MPS.SharedAPIModel.Clientes;
using MPS.SharedAPIModel.Seguridad;
using MPS.SharedAPIModel.Socios;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.BL
{
    public class SociosBL
    {
        #region Propiedades
        private SociosApi sociosApi;
        public SociosApi SociosApi => sociosApi ??= new SociosApi();
        #endregion

        public async Task<(bool Valido, DetalleSocio Detalles)> DetalleSocio(string Id)
        {
            var (StatusCode, Detalles) = await SociosApi.DetalleSocioAsync(Id);
            var valido = StatusCode == System.Net.HttpStatusCode.OK;
            if (valido)
                return (valido, Detalles);
            else
            {
                return (false, new DetalleSocio());
            }
        }

        public async Task<(bool Valido, Respuesta Respuesta)> ActualizaInfoSocio(string Id, DetalleSocio Info)
        {
            var (StatusCode, Response) = await SociosApi.ActualizaSocioAsync(Id, Info);
            var valido = StatusCode == System.Net.HttpStatusCode.OK;
            if (valido)
                return (valido, Response);
            else
                return (false, new Respuesta());
        }

        /// <summary>
        /// Obtiene el historico de solicitudos por identificador dado de un cliente
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <param name="desde">Fecha de filtro inicial</param>
        /// <param name="hasta">Fecha de filtro final</param>
        /// <returns></returns>
        public async Task<List<Historial>> GetHistoricoSolicitudesAsync(Guid idCliente, DateTime desde, DateTime hasta)
        {
            var (statusCode, resultado) = await SociosApi.GetHistoricoSolicitudesAsync(idCliente, desde, hasta);
            if (statusCode == HttpStatusCode.OK)
                return resultado;
            else
                return new List<Historial>();
        }
    }
}

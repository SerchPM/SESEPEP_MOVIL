using MPS.Core.Lib.ApiSocio;
using MPS.SharedAPIModel.Clientes;
using MPS.SharedAPIModel.Seguridad;
using MPS.SharedAPIModel.Socios;
using MPS.SharedAPIModel.Solicitud;
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

        /// <summary>
        /// Obtiene el detalle del socio
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Actualiza la información del socio
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Info"></param>
        /// <returns></returns>
        public async Task<(bool Valido, Respuesta Respuesta)> ActualizaInfoSocio(Guid Id, DetalleSocio Info)
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
        public async Task<List<HistorialSolicitudes>> GetHistoricoSolicitudesAsync(Guid idCliente, DateTime desde, DateTime hasta)
        {
            var (statusCode, resultado) = await SociosApi.GetHistoricoSolicitudesAsync(idCliente, desde, hasta);
            if (statusCode == HttpStatusCode.OK)
                return resultado;
            else
                return new List<HistorialSolicitudes>();
        }

        /// <summary>
        /// Actualiza el estatus de la solicitud
        /// </summary>
        /// <param name="socio"></param>
        /// <param name="solicitud"></param>
        /// <param name="status"></param>
        /// <returns></returns>
        public async Task<SolicitudResponse> ActualizaSolicitud(Guid socio, Guid solicitud, int status)
        {
            var (statusCode, resultado) = await SociosApi.ActualizarSolicitud(socio, solicitud, status);
            if (statusCode == HttpStatusCode.OK)
                return resultado;
            else
                return new SolicitudResponse();
        }
    }
}

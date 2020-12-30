using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Solicitud;
using Sysne.Core.ApiClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.ApiClient
{
    public class SolicitudApi : WebApiClient
    {
        #region Constructor
        public SolicitudApi() : base(Settings.Current.WebAPIUrl, "Solicitudes") { }
        #endregion

        #region Metodos
        /// <summary>
        /// Registra una nueva solicitud
        /// </summary>
        /// <param name="solicitud">Objeto con la informacion de la solicitud</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, SolicitudResponse solicitudInfo)> RegistrarSolicitudAsync(Solicitud solicitud)
        {
            var res = await CallFormUrlEncoded<SolicitudResponse>("CrearSolicitud", HttpMethod.Post,
                ("P_GUID_TIPO_SOLICITUD", solicitud.IdTipoSolicitud.ToString()),
                ("P_TIPO_SERVICIO", solicitud.IdTipoServicio.ToString()),
                ("P_FECHA_SOLICITUD", solicitud.FechaSolicitud.ToDateTimeFormat24H()),
                ("P_HORAS_PACTADAS", solicitud.HorasSolicidatas.ToString()),
                ("P_GUID_CLIENTE", solicitud.IdCliente.ToString()),
                ("P_GUID_SOCIO", solicitud.IdSocio.ToString()),
                ("P_TOTAL_PAGADO", solicitud.TotalPago.ToString()),
                ("P_GUID_MONEDA", solicitud.IdMoneda.ToString()),
                ("P_GUID_CUENTA_ORIGEN", solicitud.IdCuentaOrigen.ToString()),
                ("P_GUID_CUENTA_DESTINO", solicitud.IdCuentaDestino.ToString()),
                ("P_TEXTO_ORIGEN", solicitud.Origen),
                ("P_TEXTO_DESTINO", solicitud.Destino),
                ("P_TIEMPO_GENERAR_SOLICITUD", solicitud.TiempoGenerarSolicitud.ToString()),
                ("P_VERSION_APP", solicitud.VersionApp),
                ("P_MOVIL", solicitud.Movil),
                ("P_UBICACION_1_LAT", solicitud.Latitud.ToString()),
                ("P_UBICACION_1_LON", solicitud.Longitud.ToString()),
                ("P_UBICACION_2_LAT", solicitud.Latitud2.ToString()),
                ("P_UBICACION_2_LON", solicitud.Longitud2.ToString()),
                ("P_NO_ELEMENTOS", solicitud.NoElementos.ToString()));
            return res;
        }

        /// <summary>
        /// Registra a un socio que fue solicitado para un nuevo servicio personalizado
        /// </summary>
        /// <param name="socioAsignado">Objero con la informacion del socio y solicitud</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, AsignarSolicitudResponse asignarSolicitudInfo)> AsignarSocioAsync(SocioAsignado socioAsignado) =>
            await CallPostAsync<AsignarSolicitudResponse>("ActualizarSolicitudEstatus", ("P_GUID_SOLICITUD", socioAsignado.IdSolicitud), ("P_ESTATUS", socioAsignado.Estatus), ("P_GUID_SOCIO", socioAsignado.IdSocio));

        public async Task<(HttpStatusCode StatusCode, SolicitudActivaResponse Respuesta)> SolicitudActiva(string noSocio) =>
            await CallPostAsync<SolicitudActivaResponse>("ConsultaSolicitudesGeneral", ("P_PARAMETRO", noSocio));

        /// <summary>
        /// Calcula el costo del servicio mediante las horas de trabajo y tipo de servicio
        /// </summary>
        /// <param name="idTipoServicio">Identificador del servicio</param>
        /// <param name="horas">Horas estimadas de trabajo</param>
        /// <param name="tipoSolicitud">Tipo de solicitud(1=Express, 2=Personalizada)</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode statusCode, List<ResponseCosto> response)> CalcularCostoServicioAsync(Guid idTipoServicio, int horas, int tipoSolicitud) =>
            await CallPostAsync<List<ResponseCosto>>("GenerarTotalPorPagar", ("P_GUID_TIPO_SOLICITUD", idTipoServicio), ("P_HORAS_SOLICITADAS", horas), ("P_TIPO_SOLICITUD", tipoSolicitud));
        #endregion
    }
}

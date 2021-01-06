using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Clientes;
using MPS.SharedAPIModel.Seguridad;
using MPS.SharedAPIModel.Socios;
using MPS.SharedAPIModel.Solicitud;
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
        #region Constructor
        public SociosApi() : base(Settings.Current.WebAPIUrl, "Socios") { }
        #endregion

        #region Métodos
        /// <summary>
        /// Registra un nuevo socio
        /// </summary>
        /// <param name="nuevoSocio"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Actualiza la información del socio
        /// </summary>
        /// <param name="socio"></param>
        /// <param name="info"></param>
        /// <param name="img"></param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, Respuesta Respuesta)> ActualizaSocioAsync(Guid socio, DetalleSocio info)
        {
            var res = await CallFormUrlEncoded<Respuesta>("ActualizaInfoSocio", HttpMethod.Post,
                ("P_GUID_SOCIO", socio.ToString()),
                ("P_FECHA_NACIMIENTO", info.FECHA_NACIMIENTO),
                ("P_NOMBRE", info.NOMBRE),
                ("P_APELLIDO_1", info.APELLIDO_1),
                ("P_APELLIDO_2", info.APELLIDO_2),
                ("P_SEXO", info.GUID_SEXO.ToString()),
                ("P_TEL_NUMERO", info.TEL_NUMERO));
            return res;
        }

        /// <summary>
        /// Actualiza el password del socio
        /// </summary>
        /// <param name="idSocio">Identoficador del socio</param>
        /// <param name="password">Passowrd a actualizar</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, Respuesta Respuesta)> ActualizarPasswordSocioAsync(Guid idSocio, string password)
        {
            var res = await CallFormUrlEncoded<Respuesta>("ActualizaInfoSocio", HttpMethod.Post,
                ("P_GUID_SOCIO", idSocio.ToString()),
                ("P_PWD", password));
            return res;
        }


        /// <summary>
        /// Devuelve los detalles de un socio en particular
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, DetalleSocio DetalleSocio)> DetalleSocioAsync(string Id) =>
        await CallPostAsync<DetalleSocio>("ConsultaSocioDetalle", ("P_GUID_SOCIO", Id));

        /// <summary>
        /// Obtiene al personal que cuente con las carecteristicas del servico
        /// </summary>
        /// <param name="idTipoServicio">Identificador del servicio</param>
        /// <param name="fecha">Fecha y hora del servicio solicitado</param>
        /// <param name="horasSolicitadas">Horas que solicita de servicio el cliente</param>
        /// <param name="filtro">Filtro para una busqueda especifica de un socio(s)</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, List<Socio> catalogo)> GetSociosAsync(Guid idTipoServicio, DateTime fecha, int horasSolicitadas, string filtro) =>
             await CallPostAsync<List<Socio>>("ConsultaSociosDisponibles", ("P_GUID_TIPO_SOLICITUD", idTipoServicio), ("P_FECHAHORA_SOLICITUD", fecha.ToDateTimeFormat24H()), ("P_HORAS_SOLICITADAS", horasSolicitadas), ("P_PARAMETRO_BUSQUEDA", filtro));

        /// <summary>
        /// Obtiene al personal que cuente con las carecteristicas del servico y que esten mas sercanos a la ubicacion establecidas
        /// </summary>
        /// <param name="latitud">Latitud de la ubicacion establecida</param>
        /// <param name="longitud">Longitud de la ubicacion establecida</param>
        /// <param name="idTipoServicio">Identificador del servicio</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, List<Socio> catalogo)> GetSociosCercanosAsync(double latitud, double longitud, Guid idTipoServicio) =>
            await CallPostAsync<List<Socio>>("ConsultaSociosCercanos", ("P_LATITUD", latitud), ("P_LONGITUD", longitud), ("P_GUID_TIPO_SOLICITUD", idTipoServicio));

        /// <summary>
        /// Obtiene el historico de solicitudos por identificador dado de un cliente
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <param name="desde">Fecha de filtro inicial</param>
        /// <param name="hasta">Fecha de filtro final</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, List<HistorialSolicitudes> catalogo)> GetHistoricoSolicitudesAsync(Guid idCliente,DateTime desde, DateTime hasta) =>
            await CallPostAsync<List<HistorialSolicitudes>>("SocioSolicitudes", ("P_GUID_SOCIO", idCliente), ("P_FECHA_INICIO", desde.ToString("MM-dd-yyyy")), ("P_FECHA_FIN", hasta.ToString("MM-dd-yyyy")));

        /// <summary>
        /// Obtiene los datos bancarios de un socio mediente su identificador
        /// </summary>
        /// <param name="idSocio">Identificador del socio</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, List<DatoBancario> catalogo)> ObtenerDatosBancariosSociosAsync(Guid idSocio) =>
            await CallPostAsync<List<DatoBancario>>("ConsultaDatosBancariosSocios", ("P_GUID_SOCIO", idSocio));

        /// <summary>
        /// Actualiza el numero de tarjeta del socio
        /// </summary>
        /// <param name="idTarjeta">Identificador de la tarjeta</param>
        /// <param name="noTarjeta">No. de tarjeta a actualizar</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, ActualizacionResponse result)> ActualizarTarjetaAsync(Guid idTarjeta, string noTarjeta) =>
            await CallPostAsync<ActualizacionResponse>("ActualizaTarjetaSocio", ("P_GUID_TARJETA", idTarjeta), ("P_NUMERO_TARJETA", noTarjeta));

        /// <summary>
        /// Actualiza el numero de cuenta bancaria del socio
        /// </summary>
        /// <param name="idSocio">Odentificador del socio</param>
        /// <param name="noCuenta">No. Cuenta a actualizar</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, ActualizacionResponse result)> ActualizarBancoAsync(Guid idSocio, string noCuenta) =>
            await CallPostAsync<ActualizacionResponse>("ActualizaBancoSocio", ("P_GUID_SOCIO", idSocio), ("P_CUENTA_BANCO", noCuenta));

        /// <summary>
        /// Obtiene los servicios pendientes por identificador dado
        /// </summary>
        /// <param name="idSocio">Identificador del socio</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode statusCode, List<SolicitudPendiente> solicitudes)> ObtenerSolicitudesPendientesAsync(Guid idSocio) =>
            await CallPostAsync<List<SolicitudPendiente>>("ConsultaSolicitudesEncoladas", ("P_GUID_SOCIO", idSocio));

        /// <summary>
        /// Verifica si el socio cuanta con la atencion de un servicio activo
        /// </summary>
        /// <param name="idSocio">Identificador del socio</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode statusCode, SolicitudPendiente solicitud)> ServicioEnAtencionAysnc(Guid idSocio) =>
            await CallPostAsync<SolicitudPendiente>("ConsultaSolicitudActualSocio", ("P_GUID_SOCIO", idSocio));
        #endregion
    }
}

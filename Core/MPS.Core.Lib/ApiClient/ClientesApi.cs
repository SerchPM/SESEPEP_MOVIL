using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Clientes;
using MPS.SharedAPIModel.Seguridad;
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
    public class ClientesApi : WebApiClient
    {
        #region Constructor
        public ClientesApi() : base(Settings.Current.WebAPIUrl, "Clientes") { }
        #endregion

        #region Métodos
        /// <summary>
        /// Obtiene las tarjetas que tiene registradas el cliente 
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, List<TarjetaCliente> tarjetas)> GetTarjetasClienteAsync(Guid idCliente) =>
            await CallPostAsync<List<TarjetaCliente>>("ConsultaTarjetasClientes", ("P_GUID_CLIENTE", idCliente));

        /// <summary>
        /// Registra una nueva tarjeta
        /// </summary>
        /// <param name="tarjeta">Objeto con la informacion de la tarjeta</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, Response tarjetaInfo)> RegistrarTarjetaAsync(NuevaTarjeta tarjeta)
        {
            var res = await CallFormUrlEncoded<Response>("CrearClienteTarjeta", HttpMethod.Post,
                ("P_GUID_CLIENTE", tarjeta.IdCliente.ToString()),
                ("P_NUMERO", tarjeta.NoCuenta.ToString()),
                ("P_ANIO", tarjeta.AñoExpira.ToString()),
                ("P_MES", tarjeta.MesExpira.ToString()),
                ("P_GUID_MARCA", tarjeta.IdMarca.ToString()),
                ("P_CVV", tarjeta.CVV.ToString()),
                ("P_TIPO", tarjeta.Tipo.ToString()));
            return res;
        }

        /// <summary>
        /// Obtiene los datos generales del cliente
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, Cliente tarjetas)> GetClienteAsync(Guid idCliente) =>
            await CallPostAsync<Cliente>("ConsultaDetalle", ("P_GUID_CLIENTE", idCliente));

        /// <summary>
        /// Actualiza la informacion del cliente
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <param name="cliente">Objeto con la informacion del cliente</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, Respuesta operacionInfo)> ActualziarClienteAsync(Guid idCliente, Cliente cliente)
        {
            var res = await CallFormUrlEncoded<Respuesta>("ActualizaInfoCliente", HttpMethod.Post,
                ("P_GUID_SOCIO", idCliente.ToString()),
                ("P_FECHA_NACIMIENTO", cliente.FECHA_NACIMIENTO.Value.ToString("MM-dd-yyyy")),
                ("P_NOMBRE", cliente.NOMBRE),
                ("P_APELLIDO_1", cliente.APELLIDO_1),
                ("P_APELLIDO_2", cliente.APELLIDO_2),
                ("P_SEXO", cliente.GUID_SEXO.ToString()),
                ("P_TEL_NUMERO", cliente.TELEFONO));
            return res;
        }

        /// <summary>
        /// Actualiza el password del cliente
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <param name="password">Nuevo password del cliente</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, Respuesta operacionInfo)> ActualziarPasswordAsync(Guid idCliente, string password)
        {
            var res = await CallFormUrlEncoded<Respuesta>("ActualizaInfoCliente", HttpMethod.Post,
                           ("P_GUID_SOCIO", idCliente.ToString()),
                           ("P_PWD", password));
            return res;
        }

        /// <summary>
        /// Obtiene los diferentes servicion solicitudados por cliente
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <param name="desde">Fecha de inicio para filtrar las solicitudes</param>
        /// <param name="hasta">Fecha final para filtrar las solicitudes</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, List<ClienteSolicitud> tarjetas)> GetSolicitudesAsync(Guid idCliente, DateTime desde, DateTime hasta) =>
            await CallPostAsync<List<ClienteSolicitud>>("ConsultaClienteSolicitudes", ("P_GUID_CLIENTE", idCliente), ("P_FECHA_INICIO", desde.ToString("MM-dd-yyyy")), ("P_FECHA_FIN", hasta.ToString("MM-dd-yyyy")));

        /// <summary>
        /// Registra 
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public async Task<(HttpStatusCode StatusCode, Response tarjetaInfo)> RegistrarClienteAsync(Cliente cliente)
        {
            var res = await CallFormUrlEncoded<Response>("CrearCliente", HttpMethod.Post,
                ("P_NOMBRE", cliente.NOMBRE),
                ("P_APELLIDO_1", cliente.APELLIDO_1),
                ("P_APELLIDO_2", cliente.APELLIDO_2),
                ("P_CORREO_ELECTRONICO", cliente.CORREO_ELECTRONICO),
                ("P_PWD", cliente.Password),
                ("P_TELEFONO", cliente.TELEFONO),
                ("P_ALIAS", cliente.Alias),
                ("P_VERSION_APP", cliente.VercionApp),
                ("P_MODELO_CEL", cliente.ModeloDispositivo),
                ("P_FECHA_NACIMIENTO", cliente.FECHA_NACIMIENTO.ToDateTimeFormat24H()),
                ("P_GUID_SEXO", cliente.SEXO),
                ("P_GUID_METODO_PAGO_PRED", cliente.IdMetodoPago.ToString()));
            return res;
        }

        /// <summary>
        /// Registra la calificacion que realiza el cliente hacia el socio al finalizar el servicio
        /// </summary>
        /// <param name="idSoicitud">Identificador de la solicitud a finalizar</param>
        /// <param name="calificacion">Calificacion asignada</param>
        /// <param name="observaciones">Observaciones del socio</param>
        /// <returns></returns>
        public async Task<(HttpStatusCode statusCode, AsignarSolicitudResponse solicitud)> CalificarSocioAysnc(Guid idSoicitud, int calificacion, string observaciones) =>
            await CallPostAsync<AsignarSolicitudResponse>("ClienteCalificaSolicitud", ("P_GUID_SOLICITUD", idSoicitud), ("P_CALIFICACION", calificacion), ("P_OBSERVACIONES", observaciones));
        #endregion
    }
}

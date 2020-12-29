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
        public async Task<DetalleSocio> DetalleSocio(string Id)
        {
            var (StatusCode, Detalles) = await SociosApi.DetalleSocioAsync(Id);
            var valido = StatusCode == System.Net.HttpStatusCode.OK;
            if (valido)
                return Detalles;
            else
                return new DetalleSocio();
        }

        /// <summary>
        /// Actualiza la información del socio
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="Info"></param>
        /// <returns></returns>
        public async Task<(bool Valido, string Respuesta)> ActualizaInfoSocio(Guid Id, DetalleSocio Info)
        {
            var (StatusCode, resultado) = await SociosApi.ActualizaSocioAsync(Id, Info);
            if (StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resultado.ESTATUS) && resultado.ESTATUS.Equals("OK"))
                return (true, "Datos actualizados correctamente");
            else
                return (false, "Ocurrio un problema al actualizar la informacion, intente más tarde");
        }

        /// <summary>
        /// Actualiza el password del socio
        /// </summary>
        /// <param name="idSocio">Identoficador del socio</param>
        /// <param name="password">Passowrd a actualizar</param>
        /// <returns></returns>
        public async Task<(bool Valido, string Respuesta)> ActualizarPasswordSocioAsync(Guid idSocio, string password)
        {
            var (StatusCode, resultado) = await SociosApi.ActualizarPasswordSocioAsync(idSocio, password);
            if (StatusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resultado.ESTATUS) && resultado.ESTATUS.Equals("OK"))
                return (true, "Password actualizados correctamente");
            else
                return (false, "Ocurrio un problema al actualizar el password, intente mas tarde");
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

        /// <summary>
        /// Obtiene los datos bancarios de un socio mediente su identificador
        /// </summary>
        /// <param name="idSocio">Identificador del socio</param>
        /// <returns></returns>
        public async Task<(bool, DatoBancario)> ObtenerDatosBancariosSociosAsync(Guid idSocio)
        {
            var (statusCode, resultado) = await SociosApi.ObtenerDatosBancariosSociosAsync(idSocio);
            if (statusCode == HttpStatusCode.OK && resultado.Count > 0)
                return (true, resultado[0]);
            else
                return (false, new DatoBancario());
        }

        /// <summary>
        /// Actualiza el numero de tarjeta del socio
        /// </summary>
        /// <param name="idTarjeta">Identificador de la tarjeta</param>
        /// <param name="noTarjeta">No. de tarjeta a actualizar</param>
        /// <returns></returns>
        public async Task<bool> ActualizarTarjetaAsync(Guid idTarjeta, string noTarjeta)
        {
            var (statusCode, resultado) = await SociosApi.ActualizarTarjetaAsync(idTarjeta, noTarjeta);
            if (statusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resultado.ESTATUS) && resultado.ESTATUS.Equals("OK"))
                return true;
            else
                return false;
        }

        /// <summary>
        /// Actualiza el numero de cuenta bancaria del socio
        /// </summary>
        /// <param name="idSocio">Odentificador del socio</param>
        /// <param name="noCuenta">No. Cuenta a actualizar</param>
        /// <returns></returns>
        public async Task<bool> ActualizarBancoAsync(Guid idSocio, string noCuenta)
        {
            var (statusCode, resultado) = await SociosApi.ActualizarBancoAsync(idSocio, noCuenta);
            if (statusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resultado.ESTATUS) && resultado.ESTATUS.Equals("OK"))
                return true;
            else
                return false;
        }

    }
}

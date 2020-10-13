﻿using MPS.Core.Lib.Helpers;
using MPS.SharedAPIModel.Clientes;
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
        public async Task<(HttpStatusCode StatusCode, TarjetaResponse tarjetaInfo)> RegistrarTarjetaAsync(NuevaTarjeta tarjeta)
        {
            var res = await CallFormUrlEncoded<TarjetaResponse>("CrearClienteTarjeta", HttpMethod.Post,
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
                ("P_FECHA_NACIMIENTO", cliente.FECHA_NACIMIENTO.ToString()),
                ("P_NOMBRE", cliente.NOMBRE),
                ("P_APELLIDO_1", cliente.APELLIDO_1),
                ("P_APELLIDO_2", cliente.APELLIDO_2),
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
        #endregion
    }
}
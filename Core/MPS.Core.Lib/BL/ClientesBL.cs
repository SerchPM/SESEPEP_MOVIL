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
        /// Obtiene las tarjetas que tiene registradas el cliente 
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <returns></returns>
        public async Task<List<TarjetaCliente>> GetTarjetasClienteAsync(Guid idCliente)
        {
            var (statusCode, resultado) = await ClientesApi.GetTarjetasClienteAsync(idCliente);
            if (statusCode == HttpStatusCode.OK)
            {
                int orden = 1;
                foreach (var tarjeta in resultado)
                {
                    int tamaño = tarjeta.TARJETA.Length;
                    string digitos = string.Empty;
                    if(tamaño >= 4)
                    {
                        digitos = tarjeta.TARJETA.Substring((tamaño - 4), 4);
                        tarjeta.TARJETA = $"XXXX-XXXX-XXXX-{digitos}";
                    }
                    else
                        tarjeta.TARJETA = $"XXXX-XXXX-XXXX-{tarjeta.TARJETA}";
                    tarjeta.Orden = orden++;
                }
                return resultado;
            }
            else
                return new List<TarjetaCliente>();
        }

        /// <summary>
        /// Registra una nueva tarjeta
        /// </summary>
        /// <param name="tarjeta">Objeto con la informacion de la tarjeta</param>
        /// <returns></returns>
        public async Task<(bool, string)> RegistrarTarjetaAsync(NuevaTarjeta tarjeta)
        {
            var (statusCode, resultado) = await ClientesApi.RegistrarTarjetaAsync(tarjeta);
            if (statusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resultado.ESTATUS) && resultado.ESTATUS.Equals("OK"))
                return (true, "La tarjeta se registro correctamente");            
            else
                return (false, "Ocurrio un error al agregar la tarjeta, intente mas tarde");
        }

        /// <summary>
        /// Obtiene los diferentes servicion solicitudados por cliente
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <param name="desde">Fecha de inicio para filtrar las solicitudes</param>
        /// <param name="hasta">Fecha final para filtrar las solicitudes</param>
        /// <returns></returns>
        public async Task<List<ClienteSolicitud>> GetSolicitudesAsync(Guid idCliente, DateTime desde, DateTime hasta)
        {
            var (statusCode, resultado) = await ClientesApi.GetSolicitudesAsync(idCliente, desde, hasta);
            if (statusCode == HttpStatusCode.OK)
                return resultado;            
            else
                return new List<ClienteSolicitud>();
        }
        #endregion
    }
}

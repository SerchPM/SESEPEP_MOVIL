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
        /// Obtiene los datos generales del cliente
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <returns></returns>
        public async Task<Cliente> GetClienteAsync(Guid idCliente)
        {
            var (statusCode, resultado) = await ClientesApi.GetClienteAsync(idCliente);
            if (statusCode == HttpStatusCode.OK)
                return resultado;
            else
                return new Cliente();
        }

        /// <summary>
        /// Actualiza la informacion del cliente
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <param name="cliente">Objeto con la informacion del cliente</param>
        /// <returns></returns>
        public async Task<(bool, string)> AtualziarClienteAsync(Guid idCliente, Cliente cliente)
        {
            var (statusCode, resultado) = await ClientesApi.ActualziarClienteAsync(idCliente, cliente);
            if (statusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resultado.ESTATUS) && resultado.ESTATUS.Equals("OK"))
                return (true, "Datos actualizados correctamente");
            else
                return (false, "Ocurrio un problema al actualizar la informacion, intente más tarde");
        }

        /// <summary>
        /// Actualiza el password del cliente
        /// </summary>
        /// <param name="idCliente">Identificador del cliente</param>
        /// <param name="password">Nuevo password del cliente</param>
        /// <returns></returns>
        public async Task<(bool, string)> ActualziarPasswordAsync(Guid idCliente, string password)
        {
            var (statusCode, resultado) = await ClientesApi.ActualziarPasswordAsync(idCliente, password);
            if (statusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resultado.ESTATUS) && resultado.ESTATUS.Equals("OK"))
                return (true, "Password actualizados correctamente");
            else
                return (false, "Ocurrio un problema al actualizar el password, intente mas tarde");
        }
        #endregion
    }
}

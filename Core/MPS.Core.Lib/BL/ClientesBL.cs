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
                return (true, "La tarjeta se registro correctamente.");            
            else
                return (false, "Ocurrio un error al agregar la tarjeta, intente mas tarde.");
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
            if (statusCode == HttpStatusCode.OK && resultado.Count > 0)
                return resultado;            
            else
                return new List<ClienteSolicitud>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="cliente"></param>
        /// <returns></returns>
        public async Task<(bool, (string, Guid))> RegistrarClienteAsync(Cliente cliente)
        {
            var (statusCode, resultado) = await ClientesApi.RegistrarClienteAsync(cliente);
            if (statusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resultado.ESTATUS) && resultado.ESTATUS.Equals("OK"))
                return (true, (string.Empty, resultado.GUID.Value));
            else if (statusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resultado.ESTATUS) && resultado.ESTATUS.Equals("ERROR"))
                return (false, ("El correo que intenta registrar ya existe,\nintente con un nuevo correo.", Guid.Empty));
            else
                return (false, ("Error de registro intente más tarde.", Guid.Empty));
        }

        /// <summary>
        /// Registra la calificacion que realiza el cliente hacia el socio al finalizar el servicio
        /// </summary>
        /// <param name="idSoicitud">Identificador de la solicitud a finalizar</param>
        /// <param name="calificacion">Calificacion asignada</param>
        /// <param name="observaciones">Observaciones del socio</param>
        /// <returns></returns>
        public async Task<bool> CalificarSocioAysnc(Guid idSoicitud, int calificacion, string observaciones)
        {
            var (statusCode, resultado) = await ClientesApi.CalificarSocioAysnc(idSoicitud, calificacion, observaciones);
            if (statusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resultado.ESTATUS) && resultado.ESTATUS.Equals("OK"))
                return true;
            else
                return false;
        }
        #endregion
    }
}

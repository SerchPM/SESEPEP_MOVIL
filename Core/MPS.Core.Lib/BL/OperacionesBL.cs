﻿using MPS.Core.Lib.ApiClient;
using MPS.SharedAPIModel.Clientes;
using MPS.SharedAPIModel.Operaciones;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.BL
{
    public class OperacionesBL
    {
        #region Propiedades
        private OperacionesApi operacionesApi;
        public OperacionesApi OperacionesApi => operacionesApi ??= new OperacionesApi();
        #endregion

        #region Metodos
        /// <summary>
        /// Obtiene los diferentes tipos tarjetas disponibles
        /// </summary>
        /// <returns></returns>
        public async Task<List<Tarjeta>> GetTarjetasAsync()
        {
            var (statusCode, resultado) = await OperacionesApi.GetTarjetasAsync();
            if (statusCode == HttpStatusCode.OK)
                return resultado;
            else
                return new List<Tarjeta>();
        }
        #endregion
    }
}

using MPS.Core.Lib.ApiClient;
using MPS.Core.Lib.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.BL
{
    class MapBL
    {
        #region Atributos
        private GoogleReverseGeocodingService geocodingService;
        public GoogleReverseGeocodingService GeocodingService => geocodingService ??= new GoogleReverseGeocodingService();
        #endregion

        #region Metodos
        /// <summary>
        /// Obtiene la ubicacion de la direccion dado.
        /// </summary>
        /// <param name="address">Detalle de la direccion</param>
        /// <returns></returns>
        public async Task<List<double>> ObtenerPoints(string address)
        {
            List<double> coordenadas = new List<double>();
            var puntos = await GeocodingService.ObtenerLatLongBing(address);
            foreach (var item in puntos.ResourceSets)
            {
                foreach (var itemResourceSet in item.Resources)
                {
                    foreach (var itemResources in itemResourceSet.GeocodePoints)
                    {
                        coordenadas = itemResources.Coordinates;
                    }
                    return coordenadas;
                }
            }
            return new List<double>();
        }

        /// <summary>
        /// Obtiene la direccion medinate la direccion dada.
        /// </summary>
        /// <param name="lat">Latitud</param>
        /// <param name="lon">Longitud</param>
        /// <returns></returns>
        public async Task<string> ObtenerDireccion(double lat, double lon)
        {
            string address = string.Empty;
            var resultado = await GeocodingService.ObtenerLugarBing(lat, lon);
            foreach (var item in resultado.ResourceSets)
            {
                foreach (var itemResourceSet in item.Resources)
                {
                    address = itemResourceSet.Name;
                    return address;
                }
            }
            return address;
        }


        public async Task<(string Estado, string Pais)> ObtenerEstadoPais(double latitud, double longitud)
        {
            (string estado, string pais) = (string.Empty, string.Empty);
            var resultadoResponse = await GeocodingService.ObtenerLugarBing(latitud, longitud);
            if (resultadoResponse != null)
            {
                foreach (var itemResource in resultadoResponse.ResourceSets)
                {
                    foreach (var item in itemResource.Resources)
                    {
                        var addressFull = item.Address.FormattedAddress;
                        var country = item.Address.CountryRegion;
                        pais = country;
                        addressFull = addressFull.Trim();
                        string[] addresSplit = addressFull.Split(',');
                        for (int i = 0; i < addresSplit.Length; i++)
                        {
                            estado = addresSplit[addresSplit.Length - 1];
                        }
                    }
                }
            }
            return (estado, pais);
        }

        #endregion
    }
}

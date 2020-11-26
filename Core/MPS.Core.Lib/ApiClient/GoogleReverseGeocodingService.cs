using MPS.Core.Lib.Helpers;
using MPS.Core.Lib.Model;
using MPS.SharedAPIModel;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.ApiClient
{
    public class GoogleReverseGeocodingService : HttpClient
    {
        #region Constructor
        public GoogleReverseGeocodingService()
        {
            BaseAddress = new Uri(UrlBaseWebApiBing);
            DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("text/json"));
        }
        #endregion

        #region Propiedades
        public static string UrlBaseWebApiBing { get; set; } = "https://dev.virtualearth.net/REST/v1/";
        #endregion

        /// <summary>
        /// Obtiene la ubicacion de la direccion dado.
        /// </summary>
        /// <param name="ubicacionDetalle">Detalle de la direccion.</param>
        /// <returns></returns>
        public async Task<Response> ObtenerLatLongBing(string ubicacionDetalle)
        {
            var response = await GetAsync($"Locations?q={ubicacionDetalle}%{Settings.Current.Pais}&key=Ap0hAAAxqevSasMWuiYBHkF1YPTuT1Is2upKWzpxlbsUczUiRUbIktC4Jg5hvdHu");
            if (response.IsSuccessStatusCode)
            {
                Response result = JsonConvert.DeserializeObject<Response>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
            return new Response();
        }

        /// <summary>
        /// Obtiene la direccion medinate la direccion dada.
        /// </summary>
        /// <param name="lat">Latitud.</param>
        /// <param name="lon">Longitud.</param>
        /// <returns></returns>
        public async Task<Response> ObtenerLugarBing(double lat, double lon)
        {
            HttpResponseMessage response = await GetAsync($"Locations/{lat},{lon}?&key=Ap0hAAAxqevSasMWuiYBHkF1YPTuT1Is2upKWzpxlbsUczUiRUbIktC4Jg5hvdHu");
            if (response.IsSuccessStatusCode)
            {
                Response result = JsonConvert.DeserializeObject<Response>(response.Content.ReadAsStringAsync().Result);
                return result;
            }
            return new Response();
        }
    }
}

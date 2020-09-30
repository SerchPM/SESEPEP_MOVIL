using MPS.Core.Lib.ApiClient;
using MPS.SharedAPIModel.Operaciones;
using MPS.SharedAPIModel.Solicitud;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.BL
{
    public class SolicitudBL
    {
        #region Propiedades
        private SolicitudApi solicitudApi;
        public SolicitudApi SolicitudApi => solicitudApi ??= new SolicitudApi();

        private OperacionesApi operacionesApi;
        public OperacionesApi OperacionesApi => operacionesApi ??= new OperacionesApi();

        //private SociosApi sociosApi;
        //public SociosApi SociosApi => sociosApi ??= new SociosApi();
        #endregion

        #region Metodos
        /// <summary>
        /// Registra una nueva solicitud
        /// </summary>
        /// <param name="solicitud">Objeto con la informacion de la solicitud</param>
        /// <returns></returns>
        public async Task<SolicitudResponse> RegistrarSolicitudAsync(Solicitud solicitud)
        {
            var (statusCode, resultado) = await SolicitudApi.RegistrarSolicitudAsync(solicitud);
            if (statusCode == HttpStatusCode.OK)
                return resultado;
            else
                return new SolicitudResponse();
        }

        /// <summary>
        /// Obtiene los diferentes tipos de servicios disponibles
        /// </summary>
        /// <returns></returns>
        public async Task<List<Servicio>> ObtenerServiciosAsync()
        {
            List<Servicio> servicios = new List<Servicio>();
            var (statusCode, resultado) = await OperacionesApi.GetServiciosAsync();
            if (statusCode == HttpStatusCode.OK)
            {
                foreach (var servicio in resultado)
                {
                    if (servicio.NOMBRE.Contains("INDUSTRIAL"))
                    {
                        servicio.Imagen = "industrial.png";
                        servicio.ImagenSeleccionada = "industrialSel.png";
                    }
                    else if (servicio.NOMBRE.Contains("PERSONAL"))
                    {
                        servicio.Imagen = "personal.png";
                        servicio.ImagenSeleccionada = "personalSel.png";
                    }
                    else if (servicio.NOMBRE.Contains("INTRAMUROS"))
                    {
                        servicio.Imagen = "intramuros.png";
                        servicio.ImagenSeleccionada = "intramurosSel.png";
                    }
                    else if (servicio.NOMBRE.Contains("EVENTOS"))
                    {
                        servicio.Imagen = "eventoSocial.png";
                        servicio.ImagenSeleccionada = "eventoSocialSel.png";
                    }
                    else if (servicio.NOMBRE.Contains("MERCANCIAS"))
                    {
                        servicio.Imagen = "mercancias.png";
                        servicio.ImagenSeleccionada = "mercanciasSel.png";
                    }
                    else if (servicio.NOMBRE.Contains("VALORES"))
                    {
                        servicio.Imagen = "valores.png";
                        servicio.ImagenSeleccionada = "valoresSel.png";
                    }
                    else if (servicio.NOMBRE.Contains("CIBERNETICA"))
                    {
                        servicio.Imagen = "cibernetica.png";
                        servicio.ImagenSeleccionada = "ciberneticaSel.png";
                    }
                    else if (servicio.NOMBRE.Contains("FERIAS"))
                    {
                        servicio.Imagen = "iglesia.png";
                        servicio.ImagenSeleccionada = "iglesiaon.png";
                    }
                }
                return resultado;
            }
            else
                return servicios;
        }

        //public async Task<List<Socios>> ObtenerSociosAsync(Guid tipoServicio)
        //{
        //    var (statusCode, resultado) = await SociosApi.GetServiciosAsync(tipoServicio);
        //    if (statusCode == HttpStatusCode.OK)
        //    {
        //        foreach (var personal in resultado)
        //        {
        //            var especialidadDiv = personal.SERVICIOS.Split('-');
        //            personal.SERVICIOS = string.Empty;
        //            int posicion = 0;
        //            foreach (var especialidad in especialidadDiv)
        //            {
        //                if (posicion > 0)
        //                    personal.SERVICIOS += $"-{especialidad}\n";
        //                posicion++;
        //            }

        //        }
        //        return resultado;
        //    }
        //    else
        //        return new List<Socios>();
        //}
        #endregion
    }
}

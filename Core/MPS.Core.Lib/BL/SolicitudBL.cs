using MPS.Core.Lib.ApiClient;
using MPS.Core.Lib.ApiSocio;
using MPS.SharedAPIModel.Operaciones;
using MPS.SharedAPIModel.Socios;
using MPS.SharedAPIModel.Solicitud;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

        private SociosApi sociosApi;
        public SociosApi SociosApi => sociosApi ??= new SociosApi();
        #endregion

        #region Metodos
        /// <summary>
        /// Registra una nueva solicitud
        /// </summary>
        /// <param name="solicitud">Objeto con la informacion de la solicitud</param>
        /// <returns></returns>
        public async Task<(Guid ,(bool, string))> RegistrarSolicitudAsync(Solicitud solicitud)
        {
            var (statusCode, resultado) = await SolicitudApi.RegistrarSolicitudAsync(solicitud);
            if (statusCode == HttpStatusCode.OK && !string.IsNullOrEmpty(resultado.ESTATUS) && resultado.ESTATUS.Equals("OK"))
                return (resultado.GUID, (true, "Solicitud registrada exitosamente"));
            else
                return (Guid.Empty, (false, "Ocurrio un error al registar la solicitud, intente mas tarde"));
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

        /// <summary>
        /// Obtiene al personal que cuente con las carecteristicas del servico
        /// </summary>
        /// <param name="idTipoServicio">Identificador del servicio</param>
        /// <param name="fecha">Fecha y hora del servicio solicitado</param>
        /// <param name="horasSolicitadas">Horas que solicita de servicio el cliente</param>
        /// <returns></returns>
        public async Task<List<Socio>> ObtenerSociosCercanosAsync(double latitud, double longitud, Guid tipoServicio)
        {
            var (statusCode, resultado) = await SociosApi.GetSociosCercanosAsync(latitud, longitud, tipoServicio);
            if (statusCode == HttpStatusCode.OK)
            {
                foreach (var personal in resultado)
                {
                    if (!string.IsNullOrEmpty(personal.SERVICIOS))
                    {
                        var especialidadDiv = personal.SERVICIOS.Split('-');
                        personal.SERVICIOS = string.Empty;
                        int posicion = 0;
                        foreach (var especialidad in especialidadDiv)
                        {
                            if (posicion > 0)
                                personal.SERVICIOS += $"-{especialidad}\n";
                            posicion++;
                        }
                    }
                }
                return resultado;
            }
            else
                return new List<Socio>();
        }

        /// <summary>
        /// Obtiene al personal que cuente con las carecteristicas del servico
        /// </summary>
        /// <param name="idTipoServicio">Identificador del servicio</param>
        /// <param name="fecha">Fecha y hora del servicio solicitado</param>
        /// <param name="horasSolicitadas">Horas que solicita de servicio el cliente</param>
        /// <param name="filtro">Filtro para una busqueda especifica de un socio(s)</param>
        /// <returns></returns>
        public async Task<List<Socio>> ObtenerSociosAsync(Guid idTipoServicio, DateTime fecha, int horasSolicitadas, string filtro)
        {
            var (statusCode, resultado) = await SociosApi.GetSociosAsync(idTipoServicio, fecha, horasSolicitadas, filtro);
            if (statusCode == HttpStatusCode.OK)
            {
                foreach (var personal in resultado)
                {
                    if (!string.IsNullOrEmpty(personal.SERVICIOS))
                    {
                        var especialidadDiv = personal.SERVICIOS.Split('-');
                        personal.SERVICIOS = string.Empty;
                        int posicion = 0;
                        foreach (var especialidad in especialidadDiv)
                        {
                            if (posicion > 0)
                                personal.SERVICIOS += $"-{especialidad}\n";
                            posicion++;
                        }
                    }
                    if (string.IsNullOrEmpty(personal.IMAGEN))
                        personal.IMAGEN = string.Empty;
                    personal.Seleccionado = "checkoff.png";
                }
                return resultado;
            }
            else
                return new List<Socio>();
        }
       
        /// <summary>
        /// Registra a un socio que fue solicitado para un nuevo servicio personalizado
        /// </summary>
        /// <param name="socioAsignado">Objero con la informacion del socio y solicitud</param>
        /// <returns></returns>
        public async Task<(bool, string)> AsignarSocioAsync(SocioAsignado socioAsignado)
        {
            var (statusCode, resultado) = await SolicitudApi.AsignarSocioAsync(socioAsignado);
            if (statusCode == HttpStatusCode.OK && resultado.Count > 0 && !string.IsNullOrEmpty(resultado[0].ESTATUS) && resultado[0].ESTATUS.Equals("OK"))
                return (true, resultado.FirstOrDefault().DESCRIPCION);
            else
                return (false, resultado.FirstOrDefault().DESCRIPCION);
        }

        /// <summary>
        /// Consulta si hay una solicitud activa
        /// </summary>
        /// <param name="noSocio"></param>
        /// <returns></returns>
        public async Task<(bool, SolicitudActivaResponse)> SolicitudActiva(string noSocio)
        {
            var (statusCode, resultado) = await SolicitudApi.SolicitudActiva(noSocio);
            var valido = statusCode == System.Net.HttpStatusCode.OK;
            if(valido)
                return (valido, resultado);
            else
                return (false, new SolicitudActivaResponse());
        }

        /// <summary>
        /// Calcula el costo del servicio mediante las horas de trabajo y tipo de servicio
        /// </summary>
        /// <param name="idTipoServicio">Identificador del servicio</param>
        /// <param name="horas">Horas estimadas de trabajo</param>
        /// <param name="tipoSolicitud">Tipo de solicitud(1=Express, 2=Personalizada)</param>
        public async Task<int> CalcularCostoServicioAsync(Guid idTipoServicio, int horas, int tipoSolicitud)
        {
            var (statusCode, resultado) = await SolicitudApi.CalcularCostoServicioAsync(idTipoServicio, horas, tipoSolicitud);
            if (statusCode == HttpStatusCode.OK && resultado.Count > 0)
                return resultado[0].COSTO_TOTAL;
            else
                return 0;
        }

        /// <summary>
        /// Registra la ubicacion actual del cliente/socio por solicitud.
        /// </summary>
        /// <param name="idSolicitud">Identificador de la solicitud.</param>
        /// <param name="latitud">Latitud del socio.</param>
        /// <param name="longitud">Longitud del socio.</param>
        /// <param name="latitudC">Latitud del cliente.</param>
        /// <param name="longitudC">Longitud del cliente.</param>
        /// <returns></returns>
        public async Task<bool> MandarUbicacionAsync(Guid idSolicitud, string latitud, string longitud, string latitudC, string longitudC)
        {
            var (statusCode, response) = await SolicitudApi.MandarUbicacionAsync(idSolicitud, latitud, longitud, latitudC, longitudC);
            if (statusCode == HttpStatusCode.OK && response.Count > 0 && !string.IsNullOrEmpty(response[0].ESTATUS) && response[0].ESTATUS.Equals("OK"))
                return true;
            else
                return false;
        }
        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Clientes
{
    public class ServicioSolicitado
    {
        public string ActualLat { get; set; }
        public string CalificacionSocio { get; set; }
        public int ClaveTipoServicio { get; set; }
        public string FechaSolicitud { get; set; }
        public string FolioSolicitud { get; set; }
        public Guid IdSocio { get; set; }
        public Guid IdSolicitud { get; set; }
        public Guid IdTipoSolicitud { get; set; }
        public string NombreSocio { get; set; }
        public string NombreServicio { get; set; }
        public string TipoServicio { get; set; }
        public int TipoNotificacion { get; set; }
    }
}

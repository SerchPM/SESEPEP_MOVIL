using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Socios
{
    public class SolicitudServicio
    {
        public int ClaveTipoServicio { get; set; }
        public string FechaSolicitud { get; set; }
        public string FolioSolicitud { get; set; }
        public Guid IdCliente { get; set; }
        public Guid IdSolicitud { get; set; }
        public Guid IdTipoSolicitud { get; set; }
        public string NombreCliente { get; set; }
        public string NombreServicio { get; set; }
        public string TipoServicio { get; set; }
        public string Ubicacion { get; set; }
    }
}

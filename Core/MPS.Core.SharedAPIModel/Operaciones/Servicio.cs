using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Operaciones
{
    public class Servicio
    {
        public Guid Guid { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public int ESTATUS { get; set; }
        public string Imagen { get; set; }
        public string ImagenSeleccionada { get; set; }
    }
}

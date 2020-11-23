using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Operaciones
{
    public class Sexo
    {
        public Guid GUID { get; set; }
        public string NOMBRE { get; set; }
        public string DESCRIPCION { get; set; }
        public int ESTATUS { get; set; }
        public override string ToString()
        {
            return NOMBRE;
        }
    }
}

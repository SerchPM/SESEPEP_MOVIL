using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Clientes
{
    public class Tarjeta
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

    public class NuevaTarjeta
    {
        public int NoCuenta { get; set; }
        public int MesExpira { get; set; }
        public int AñoExpira { get; set; }
        public Guid IdTarjeta { get; set; }
        public int CVV { get; set; }
        public int Orden { get; set; }
    }
}

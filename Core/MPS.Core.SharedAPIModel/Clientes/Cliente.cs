using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Clientes
{
    public class Cliente
    {
        public string NO_CLIENTE { get; set; }
        public string NOMBRE { get; set; }
        public string APELLIDO_1 { get; set; }
        public string APELLIDO_2 { get; set; }
        public DateTime FECHA_NACIMIENTO { get; set; }
        public int EDAD { get; set; }
        public string CORREO_ELECTRONICO { get; set; }
        public string TELEFONO { get; set; }
        public double? RANKING { get; set; }
        public int? ESTATUS_1 { get; set; }
        public DateTime? FECHA_ULTIMA_APERTURA_APP { get; set; }
        public string IMAGEN { get; set; }
        public string SEXO { get; set; }
        public DateTime? ULTIMA_SOLICITUD { get; set; }
        public Guid? GUID_ULTIMA_SOLICITUD { get; set; }
        public string TARJETA { get; set; }
        public string MARCA_TARJETA { get; set; }
        public string Alias { get; set; }
        public string ModeloDispositivo { get; set; }
        public Guid IdMetodoPago { get; set; }
        public string Password { get; set; }
        public string VercionApp { get; set; }

    }
}

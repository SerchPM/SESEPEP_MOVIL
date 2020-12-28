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
        public Guid IdCliente { get; set; }
        public string NoCuenta { get; set; }
        public int MesExpira { get; set; }
        public int AñoExpira { get; set; }
        public Guid IdMarca { get; set; }
        public string CVV { get; set; }
        public int Tipo { get; set; }
        public int Orden { get; set; }
    }

    public class TarjetaCliente
    {
        public string MARCA { get; set; }
        public string TARJETA { get; set; }
        public string TIPO_TARJETA { get; set; }
        public bool PRINCIPAL { get; set; }
        public int Orden { get; set; }
    }

    public class TipoTarjeta
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }

        public override string ToString()
        {
            return Descripcion;
        }
    }

    public class Response
    {
        public string ESTATUS { get; set; }
        public string DESCRIPCION { get; set; }
        public Guid? GUID { get; set; }
        public string FOLIO { get; set; }
        public DateTime FECHA { get; set; }
        public string ATRIBUTO1 { get; set; }
    }
}

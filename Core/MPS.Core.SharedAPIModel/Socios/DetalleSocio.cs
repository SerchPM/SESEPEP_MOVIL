using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Socios
{
    public class DetalleSocio
    {
        public string NOMBRE { get; set; }
        public string APELLIDO_1 { get; set; }
        public string APELLIDO_2 { get; set; }
        public string FECHA_NACIMIENTO { get; set; }
        public int EDAD { get; set; }
        public object CURP { get; set; }
        public object RFC { get; set; }
        public int ESTATUS_1 { get; set; }
        public string E_MAIL { get; set; }
        public string TEL_NUMERO { get; set; }
        public string CODIGO_POSTAL { get; set; }
        public string ESTADO { get; set; }
        public string MUNICIPIO { get; set; }
        public string LOCALIDAD { get; set; }
        public string COLONIA { get; set; }
        public string CALLE { get; set; }
        public string NO_EXT { get; set; }
        public string NO_INT { get; set; }
        public string REFERENCIA { get; set; }
        public string IMAGEN { get; set; }
        public string TIPO_IMAGEN { get; set; }
        public float RANKING { get; set; }
        public object ULTIMA_SOLICITUD { get; set; }
        public string NO_CLIENTE { get; set; }
        public object FECHA_INICIO_SOLICITUD { get; set; }
        public object FECHA_FIN_SOLICITUD { get; set; }
        public object ESTATUS_SOLICITUD { get; set; }
        public string GUID_SEXO { get; set; }
        public string P_PWD { get; set; }
    }
}

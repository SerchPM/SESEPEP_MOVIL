using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Socios
{
    public class NuevoSocio
    {
        public string P_NOMBRE { get; set; }
        public string P_APELLIDO_1 { get; set; }
        public string P_APELLIDO_2 { get; set; }
        public string P_FECHA_NACIMIENTO { get; set; }
        public Guid P_GUID_SEXO { get; set; }
        public string P_CURP { get; set; }
        public string P_RFC { get; set; }
        public Guid P_GUID_CIUDAD_OPERACIÓN { get; set; }
        public Guid P_GUID_PAIS { get; set; }
        public Guid P_GUID_CP { get; set; }
        public Guid P_GUID_ESTADO_PROVINCIA { get; set; }
        public Guid P_GUID_MUNICIPIO { get; set; }
        public Guid P_GUID_COLONIA { get; set; }
        public string P_CALLE { get; set; }
        public int P_NO_EXT { get; set; }
        public long P_NO_INT { get; set; }
        public string P_REFERENCIA { get; set; }
        public string P_ALIAS { get; set; }
        public string P_PWD { get; set; }
        public string P_E_MAIL { get; set; }
        public long P_TEL_NUMERO { get; set; }
    }
}

using Sysne.Core.MVVM;
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
        public DateTime? FECHA_NACIMIENTO { get; set; }
        public int? EDAD { get; set; }
        public string CORREO_ELECTRONICO { get; set; }
        public string TELEFONO { get; set; }
        public double? RANKING { get; set; }
        public int? ESTATUS_1 { get; set; }
        public DateTime? FECHA_ULTIMA_APERTURA_APP { get; set; }
        public string IMAGEN { get; set; }
        public string SEXO { get; set; }
        public Guid GUID_SEXO { get; set; }
        public DateTime? ULTIMA_SOLICITUD { get; set; }
        public Guid? GUID_ULTIMA_SOLICITUD { get; set; }
        public string TARJETA { get; set; }
        public string MARCA_TARJETA { get; set; }
        public string Alias { get; set; }
        public string ModeloDispositivo { get; set; }
        public Guid IdMetodoPago { get; set; }
        public string Password { get; set; }
        public string VercionApp { get; set; }
        public string API_KEY_MOVIL { get; set; }
    }

    public class ClienteDetalle : ViewModelBase
    {
        private string no_cliente;
        public string NO_CLIENTE { get => no_cliente; set => Set(ref no_cliente, value); }
        private string nombre;
        public string NOMBRE { get => nombre; set => Set(ref nombre, value); }
        private string apellido_1;
        public string APELLIDO_1 { get => apellido_1; set => Set(ref apellido_1, value); }
        private string apellido_2;
        public string APELLIDO_2 { get => apellido_2; set => Set(ref apellido_2, value); }
        private string fecha_Nacimiento;
        public string FECHA_NACIMIENTO { get => fecha_Nacimiento; set => Set(ref fecha_Nacimiento, value); }
        private int? edad;
        public int? EDAD { get => edad; set => Set(ref edad, value); }
        private string correo_electronico;
        public string CORREO_ELECTRONICO { get => correo_electronico; set => Set(ref correo_electronico, value); }
        private string telefono;
        public string TELEFONO { get => telefono; set => Set(ref telefono, value); }
        private double? reanking;
        public double? RANKING { get => reanking; set => Set(ref reanking, value); }
        private int? estatus_1;
        public int? ESTATUS_1 { get => estatus_1; set => Set(ref estatus_1, value); }
        private DateTime? fecha_ultima_apertura_app;
        public DateTime? FECHA_ULTIMA_APERTURA_APP { get => fecha_ultima_apertura_app; set => Set(ref fecha_ultima_apertura_app, value); }
        private string imagen;
        public string IMAGEN { get => imagen; set => Set(ref imagen, value); }
        private string sexo;
        public string SEXO { get => sexo; set => Set(ref sexo, value); }
        private Guid? guid_sexo;
        public Guid? GUID_SEXO { get => guid_sexo; set => Set(ref guid_sexo, value); }
        private string ultima_solicitud;
        public string ULTIMA_SOLICITUD { get => ultima_solicitud; set => Set(ref ultima_solicitud, value); }
        private Guid? guid_ultima_solicitud;
        public Guid? GUID_ULTIMA_SOLICITUD { get => guid_ultima_solicitud; set => Set(ref guid_ultima_solicitud, value); }
        private string tarjeta;
        public string TARJETA { get => tarjeta; set => Set(ref tarjeta, value); }
        private string marca_tarjeta;
        public string MARCA_TARJETA { get => marca_tarjeta; set => Set(ref marca_tarjeta, value); }
        private string api_key_movil;
        public string API_KEY_MOVIL { get => api_key_movil; set => Set(ref api_key_movil, value); }
        private DateTime fechaNacimiento;
        public DateTime FechaNacimiento { get => fechaNacimiento; set => Set(ref fechaNacimiento, value); }
    }
}

using Sysne.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Socios
{
    public class SolicitudPendiente : ViewModelBase
    {
        private Guid guid_solicitud;
        public Guid GUID_SOLICITUD { get => guid_solicitud; set => Set(ref guid_solicitud, value); }
        private string no_solicitud;
        public string NO_SOLICITUD { get => no_solicitud; set => Set(ref no_solicitud, value); }
        private Guid guid_cliente;
        public Guid GUID_CLIENTE { get => guid_cliente; set => Set(ref guid_cliente, value); }
        private string no_cliete;
        public string NO_CLIENTE { get => no_cliete; set => Set(ref no_cliete, value); }
        private Guid guid_tipo_solicitid;
        public Guid GUID_TIPO_SOLICITUD { get => guid_tipo_solicitid; set => Set(ref guid_tipo_solicitid, value); }
        private string tipo_solicitud;
        public string TIPO_SOLICITUD { get => tipo_solicitud; set => Set(ref tipo_solicitud, value); }
        private DateTime? fecha_solicitud;
        public DateTime? FECHA_SOLICITUD { get => fecha_solicitud; set => Set(ref fecha_solicitud, value); }
        private decimal? costo_socio;
        public decimal? COSTO_SOCIO { get => costo_socio; set => Set(ref costo_socio, value); }
        private int? horas_pactadas;
        public int? HORAS_PACTADAS { get => horas_pactadas; set => Set(ref horas_pactadas, value); }
        private int? tipo_servicio;
        public int? TIPO_SERVICIO { get => tipo_servicio; set => Set(ref tipo_servicio, value); }
        private int? estatus_solicitud;
        public int? ESTATUS_SOLICITUD { get => estatus_solicitud; set => Set(ref estatus_solicitud, value); }
        private string ubicacion_servicio;
        public string UBICACION_SERVICIO { get => ubicacion_servicio; set => Set(ref ubicacion_servicio, value); }
        private string nombre_completo;
        public string NOMBRE_COMPLETO { get => nombre_completo; set => Set(ref nombre_completo, value); }
        private double? ranking;
        public double? RANKING { get => ranking; set => Set(ref ranking, value); }
    }
}

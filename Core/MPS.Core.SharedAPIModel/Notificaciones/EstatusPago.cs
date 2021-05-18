namespace MPS.SharedAPIModel.Notificaciones
{
    public class EstatusPago
    {
        public int ClaveTipoServicio { get; set; }
        public string Monto { get; set; }
        public string NoAutorizacion { get; set; }
        public string Banco { get; set; }
        public string NoTarjeta { get; set; }
        public string Descripcion { get; set; }
        public string NombreServicio { get; set; }
        public string Status { get; set; }
        public int Codigo { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.Core.Lib.Helpers
{
    public enum TipoUsuarioEnum
    {
        Cliente = 1,
        Socio = 2
    }

    public enum TipoSolicitudEnum
    {
        Express = 1,
        Personalizada = 2
    }

    public enum EstatusSolicitudEnum
    {
        Inicial,
        Aceptado,
        EnCurso,
        Atendiendo,
        Finalizado,
        Alerta
    }

    public enum TipoNotificacionEnum
    {
        ClienteSolicita = 1,
        SocioAcepta = 2,
        TiempoLlegada = 3,
        Atendiendo = 4,
        Finalizado = 5,
        Alerta = 6
    }

    public enum TipoRolEnum
    {
        Cliente = 1,
        Socio = 2,
        Usuario = 3,
        Administrador = 4,
        Partner = 5,
        Servicios = 6,
    }

    public enum TipoFaseClienteEnum
    {
        Prospecto,
        Verificado,
        Completado
    }

    public enum TipoFaseSocioEnum
    {
        Prospecto = 0,
        Verificado = 1,
        Servicios = 2,
        Documentacion = 4,
        Cita = 5,
        Examen = 6,
        Dictaminacion = 7,
        Finalizado = 8
    }

    public enum EstatusEnum
    {
        Inactivo,
        Activo
    }
}

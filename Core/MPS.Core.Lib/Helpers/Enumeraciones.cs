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
       Finalizado = 4
    }
}

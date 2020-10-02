using MPS.Core.Lib.ApiSocio;
using MPS.SharedAPIModel.Seguridad;
using MPS.SharedAPIModel.Socios;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.BL
{
    public class SociosBL
    {
        public async Task<(bool Valido, DetalleSocio Detalles)> DetalleSocio(string Id)
        {
            var (StatusCode, Detalles) = await (new SociosApi()).DetalleSocioAsync(Id);
            var valido = StatusCode == System.Net.HttpStatusCode.OK;
            if (valido)
                return (valido, Detalles);
            else
            {
                return (false, new DetalleSocio());
            }
        }

        public async Task<(bool Valido, Respuesta Respuesta)> ActualizaInfoSocio(string Id, DetalleSocio Info)
        {
            var (StatusCode, Response) = await (new SociosApi()).ActualizaSocioAsync(Id, Info);
            var valido = StatusCode == System.Net.HttpStatusCode.OK;
            if (valido)
                return (valido, Response);
            else
                return (false, new Respuesta());
        }
    }
}

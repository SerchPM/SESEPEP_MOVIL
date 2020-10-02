using MPS.Core.Lib.ApiSocio;
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
    }
}

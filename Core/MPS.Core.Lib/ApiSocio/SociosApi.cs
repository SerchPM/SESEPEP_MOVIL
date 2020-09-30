using MPS.Core.Lib.Helpers;
using Sysne.Core.ApiClient;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MPS.Core.Lib.ApiSocio
{
    public class SociosApi : WebApiClient
    {
        public SociosApi() : base(Settings.Current.WebAPIUrl, "SolicitudesSocios") { }

    }
}

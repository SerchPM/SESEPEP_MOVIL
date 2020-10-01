using MPS.Core.Lib.Helpers;
using Sysne.Core.ApiClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.Core.Lib.ApiClient
{
    public class ClientesApi : WebApiClient
    {
        #region Constructor
        public ClientesApi() : base(Settings.Current.WebAPIUrl, "Clientes") { }
        #endregion

        #region Métodos
        #endregion
    }
}

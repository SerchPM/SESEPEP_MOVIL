using MPS.Core.Lib.ApiClient;
using MPS.Core.Lib.Helpers;
using MPS.Core.Lib.OS;
using MPS.Tests.ApiClientTest.OS;
using Sysne.Core.OS;
using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace MPS.Tests.ApiClientTest
{
    class Program
    {
        static async Task Main()
        {
            DependencyService.Register<SettingsStorage, ISettingsStorage>();

            Console.WriteLine(Settings.Current.WebAPIUrl);

            var api = new SeguridadApi();
            var res = await api.LoginAsync("correo@loquesea.com", "PASSWORD123");

            Console.WriteLine(res);
        }
    }
}

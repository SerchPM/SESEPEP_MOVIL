using MPS.AppSocio.Views.Views;
using MPS.Core.Lib.OS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MPS.AppSocio.Views.OS
{
    internal class NavigationService : Core.Lib.OS.INavigationService
    {
        internal INavigation Navigation { get; set; }

        public async Task GoBack() => await Navigation.PopAsync(true);

        public async Task Home() => await Navigation.PopToRootAsync(true);

        public async void NavigatePop() => await Navigation.PopAsync();

        public async Task NavigateTo(string pageKey)
        {
            if (pageKey == PagesKeys.Login)
            {
                await Navigation.PopToRootAsync(true);
                return;
            }

            var últimaPágina = Navigation.NavigationStack.LastOrDefault();
            var vieneDelLogin = (últimaPágina != null && últimaPágina is Login);

            var paginaPorNavegar = pageKey switch
            {
                PagesKeys.SolicitarServicio => typeof(SolicitarServicio),
                PagesKeys.Historial => typeof(Historial),
                PagesKeys.FormaDePago => typeof(FormaDePago),
                PagesKeys.Perfil => typeof(Perfil),
                _ => typeof(SolicitarServicio)
            };

            var ultimaPagina = Navigation.NavigationStack.Where(p => p.GetType() == paginaPorNavegar).FirstOrDefault();
            if (ultimaPagina == null)
            {
                switch (pageKey)
                {
                    case PagesKeys.Login:
                        await Navigation.PopToRootAsync(true); break;
                    case PagesKeys.SolicitarServicio:
                        await Navigation.PushAsync(new SolicitarServicio(), vieneDelLogin); break;
                    case PagesKeys.Historial:
                        await Navigation.PushAsync(new Historial(), false); break;
                    case PagesKeys.FormaDePago:
                        await Navigation.PushAsync(new FormaDePago(), false); break;
                    case PagesKeys.Perfil:
                        await Navigation.PushAsync(new Perfil(), false); break;
                }
            }
            else
            {
                Navigation.RemovePage(ultimaPagina);
                await Navigation.PushAsync(ultimaPagina, true);
            }
            if (vieneDelLogin) Navigation.RemovePage(últimaPágina);
        }

        public async Task NavigateTo(string pageKey, params object[] parameter)
        {
            switch (pageKey)
            {
                case PagesKeys.Login:
                    await Navigation.PushAsync(new SolicitarServicio()/*(parameter)*/, true); break;
                case PagesKeys.SolicitarServicio:
                    await Navigation.PushAsync(new SolicitarServicio()/*(parameter)*/, true); break;
            }
        }

        public async void NavigateToUrl(string url) => await Xamarin.Essentials.Launcher.OpenAsync(new Uri(url));

        public Task PopModal() => throw new NotImplementedException();

        public Task PushModal(string pageKey) => null;
    }
}

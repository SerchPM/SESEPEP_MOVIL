using MPS.Core.Lib.OS;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace MPS.AppCliente.Views.OS
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

            var paginaPorNavegar = pageKey switch
            {
                PagesKeys.Main => typeof(MainPage),
                _ => typeof(MainPage)
            };

            var ultimaPagina = Navigation.NavigationStack.Where(p => p.GetType() == paginaPorNavegar).FirstOrDefault();
            if (ultimaPagina == null)
            {
                switch (pageKey)
                {
                    case PagesKeys.Login:
                        await Navigation.PopToRootAsync(true); break;
                    case PagesKeys.Main:
                        await Navigation.PushAsync(new MainPage(), true); break;
                }
            }
        }

        public async Task NavigateTo(string pageKey, params object[] parameter)
        {
            switch (pageKey)
            {
                case PagesKeys.Login:
                    await Navigation.PushAsync(new MainPage()/*(parameter)*/, true); break;
                case PagesKeys.Main:
                    await Navigation.PushAsync(new MainPage()/*(parameter)*/, true); break;
            }
        }

        public async void NavigateToUrl(string url) => await Xamarin.Essentials.Launcher.OpenAsync(new Uri(url));

        public Task PopModal() => throw new NotImplementedException();

        public Task PushModal(string pageKey) => null;
    }
}
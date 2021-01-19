using MPS.Core.Lib.Helpers;
using MPS.Core.Lib.OS;
using Sysne.Core.MVVM;
using Sysne.Core.OS;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.Core.Lib.ViewModels
{
    public class HeaderViewModel:ViewModelBase
    {
        public HeaderViewModel()
        {
            if (!string.IsNullOrEmpty(Settings.Current.LoginInfo.details.nameid))
                NombreSocio = Settings.Current.LoginInfo.details.nameid;

            Ranking = Settings.Current.LoginInfo.Usr.Ranking;
        }

        private string nombreSocio;
        public string NombreSocio { get => nombreSocio; set { Set(ref nombreSocio, value); } }

        private double ranking;
        public double Ranking { get => ranking; set => Set(ref ranking, value); }

        RelayCommand<string> navegarACommand = null;
        public RelayCommand<string> NavegarACommand
        {
            get => navegarACommand ??= new RelayCommand<string>(async (string p) =>
            {
                if (!string.IsNullOrEmpty(p) && !string.IsNullOrEmpty(Settings.Current.PaginaActual) && !Settings.Current.PaginaActual.Equals(p))
                {
                    await DependencyService.Get<INavigationService>().NavigateTo(p);
                    if (!p.Equals("Perfil"))
                        Settings.Current.PaginaActual = p;
                }
            }, (string p) => true);
        }
    }
}

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
        private string nombreSocio;
        public string NombreSocio { get => nombreSocio; set { Set(ref nombreSocio, value); } }

        RelayCommand nombreCommand = null;
        public RelayCommand NombreCommand
        {
            get => nombreCommand ?? (nombreCommand = new RelayCommand(() =>
            {
                NombreSocio = Settings.Current.LoginInfo.details.nameid;
            }));
        }

        RelayCommand<string> navegarACommand = null;
        public RelayCommand<string> NavegarACommand
        {
            get => navegarACommand ??= new RelayCommand<string>(async (string p) =>
            {
                await DependencyService.Get<INavigationService>().NavigateTo(p);
            }, (string p) => true);
        }
    }
}

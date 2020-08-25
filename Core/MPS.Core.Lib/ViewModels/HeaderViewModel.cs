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

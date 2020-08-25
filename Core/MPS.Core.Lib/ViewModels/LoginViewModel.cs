using MPS.Core.Lib.OS;
using Sysne.Core.MVVM;
using Sysne.Core.OS;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.Core.Lib.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        RelayCommand loginCommand = null;
        public RelayCommand LoginCommand
        {
            get => loginCommand ??= new RelayCommand(async () =>
            {
                await DependencyService.Get<INavigationService>().NavigateTo(PagesKeys.SolicitarServicio);
            }, () => Validate(this, false)
                , dependencies: (this, new[] { "" /*nameof(Usuario), nameof(Contraseña)*/ }));
        }
    }
}

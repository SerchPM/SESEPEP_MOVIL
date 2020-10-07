using MPS.Core.Lib.BL;
using MPS.Core.Lib.Helpers;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.Core.Lib.ViewModels.Socios
{
    public class PagoViewModel : ViewModelWithBL<SociosBL>
    {
        PagoViewModel()
        {
            Id = Settings.Current.LoginInfo.Usr.Id;
        }

        string id;
        public string Id { get => id; set { Set(ref id, value); } }

        string cuenta;
        public string Cuenta { get => cuenta; set { Set(ref cuenta, value); } }

        string banco;
        public string Banco { get => banco; set { Set(ref banco, value); } }

        RelayCommand getInfoCommand = null;
        public RelayCommand GetInfoCommand
        {
            get => getInfoCommand ?? (getInfoCommand = new RelayCommand(async () =>
            {

            }));
        }

        RelayCommand<List<string>> updateInfoCommand = null;
        public RelayCommand<List<string>> UpdateInfoCommand
        {
            get => updateInfoCommand ?? (updateInfoCommand = new RelayCommand<List<string>>(async (List<string> info) =>
            {

            }));
        }
    }
}

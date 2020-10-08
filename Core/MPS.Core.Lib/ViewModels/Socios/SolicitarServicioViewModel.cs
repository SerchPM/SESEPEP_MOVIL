using MPS.Core.Lib.BL;
using Sysne.Core.MVVM;
using Sysne.Core.MVVM.Patterns;
using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.Core.Lib.ViewModels.Socios
{
    public class SolicitarServicioViewModel : ViewModelWithBL<SociosBL>
    {
        public SolicitarServicioViewModel()
        {

        }

        bool modal;
        public bool Modal {get => modal; set{Set(ref modal, value); } }
        string mensaje;
        public string Mensaje { get => mensaje; set { Set(ref mensaje, value); } }

        private RelayCommand ocultarModalCommand = null;
        public RelayCommand OcultarModalCommand
        {
            get => ocultarModalCommand ??= new RelayCommand(() =>
            {
                Modal = false;
                Mensaje = string.Empty;
            });
        }
    }
}

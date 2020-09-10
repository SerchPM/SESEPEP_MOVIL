using MPS.Core.Lib.Model;
using Sysne.Core.MVVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MPS.Core.Lib.ViewModels
{
    public class SolicitudDeServicioViewModel : ViewModelBase
    {
        public SolicitudDeServicioViewModel()
        {
            ServicioSeleccionado = Servicios.FirstOrDefault(s => s.Nombre.Contains("Intramuros"));
        }

        #region Properties
        public List<Servicio> Servicios => Servicio.Listado.ToList();

        private Servicio servicioSeleccionado;
        public Servicio ServicioSeleccionado { get => servicioSeleccionado; set => Set(ref servicioSeleccionado, value); }


        private bool esExpress = true;
        public bool EsExpress
        {
            get => esExpress;
            set
            {
                Set(ref esExpress, value);
                RaisePropertyChanged(() => EsPersonalizado);
            }
        }

        public bool EsPersonalizado { get => !EsExpress; }
        #endregion

        #region Commands

        RelayCommand<string> cambiarPrioridadCommand = null;
        public RelayCommand<string> CambiarPrioridadCommand
        {
            get => cambiarPrioridadCommand ??= new RelayCommand<string>((string p) =>
            {
                EsExpress = p == "Express";
            }, (string p) => true);
        }
        #endregion
    }
}

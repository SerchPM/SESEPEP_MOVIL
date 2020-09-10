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
            ServicioSeleccionado = Servicios.FirstOrDefault(s=>s.Nombre.Contains("Intramuros"));
        }

        public List<Servicio> Servicios => Servicio.Listado.ToList();


        private Servicio servicioSeleccionado;
        public Servicio ServicioSeleccionado { get => servicioSeleccionado; set => Set(ref servicioSeleccionado, value); }
    }
}

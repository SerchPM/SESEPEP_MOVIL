using MPS.SharedAPIModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Sysne.Core.OS
{
    public interface IOS
    {
        /// <summary>
        /// Establece el color del status bar (AppBar) en cada plataforma
        /// </summary>
        /// <param name="color"></param>
        void SetStatusBarColor(string color);
        void HideNavigation(bool show);
        // string NombreArchivo { get; set; }
        void ShowToast(string text);

        Task<Geoposicion> ObtenerGeoposicion(bool precision);

    }
}

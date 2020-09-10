using System;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Text;

namespace MPS.Core.Lib.Model
{
    public class Servicio
    {
        Servicio(int código, string nombre, string imagen, string imagenSeleccionada)
        {
            Código = código; Nombre = nombre; Imagen = imagen; ImagenSeleccionada = imagenSeleccionada;
        }

        public int Código { get; set; }
        public string Nombre { get; set; }
        public string Imagen { get; set; }
        public string ImagenSeleccionada { get; set; }

        static IEnumerable<Servicio> listado = null;
        public static IEnumerable<Servicio> Listado
        {
            get => listado ?? new List<Servicio>()
            {
                new Servicio(1,"Seguridad Personal", "personal.png","personalSel.png"),
                new Servicio(2,"Seguridad Intramuros", "intramuros.png","intramurosSel.png"),
                new Servicio(3,"Seguridad Mercancías", "mercancias.png","mercanciasSel.png"),
                new Servicio(4,"Seguridad Valores", "valores.png","valoresSel.png"),
                new Servicio(5,"Seguridad Industrial", "industrial.png","industrialSel.png"),
                new Servicio(6,"Seguridad Evento social", "eventoSocial.png","eventoSocialSel.png"),
                new Servicio(7,"Seguridad Cibernética", "cibernetica.png","ciberneticaSel.png")
            };
        }
    }
}
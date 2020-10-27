using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel
{
    public class Mensaje
    {
        public Mensaje(Dictionary<string, object> valores)
        {
            if (valores.ContainsKey("MensajePrincipal"))
                mensajePrincipal = valores["MensajePrincipal"].ToString();
        }
        string mensajePrincipal;
        public string MensajePrincipal { get => mensajePrincipal; set => mensajePrincipal = value; }
    }
}

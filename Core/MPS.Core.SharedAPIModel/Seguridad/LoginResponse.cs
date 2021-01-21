using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.SharedAPIModel.Seguridad
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Details
    {
        public string jti { get; set; }
        public string nameid { get; set; }
        public string nombre { get; set; }
        public string apellidos { get; set; }
        public string email { get; set; }
        public string role { get; set; } //http://schemas.microsoft.com/ws/2008/06/identity/claims/role 
        public int nbf { get; set; }
        public int exp { get; set; }
        public string iss { get; set; }
        public string aud { get; set; }
    }

    public class Usr
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public string Email { get; set; }
        public int Rol { get; set; }
        public int Estatus { get; set; }
        public int Fase { get; set; }
        public double Ranking { get; set; }
    }

    public class LoginResponse
    {
        public string token { get; set; }
        public Details details { get; set; }
        public Usr Usr { get; set; }
    }
}

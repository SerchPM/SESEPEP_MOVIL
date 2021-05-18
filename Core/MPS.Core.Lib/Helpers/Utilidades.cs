using System;
using System.Collections.Generic;
using System.Text;

namespace MPS.Core.Lib.Helpers
{
    public class Utilidades
    {
        public static (double latitud, double longitud) LimpiarCadenaUbicacion(string ubicacion)
        {
            double latitud = 0;
            double longitud = 0;
            var cadenaLimpia = ubicacion;
            try
            {
                cadenaLimpia = cadenaLimpia.Replace("POINT (", "");
                cadenaLimpia = cadenaLimpia.Replace(")", "");
                var datos = cadenaLimpia.Split(' ');
                latitud = double.Parse(datos[1]);
                longitud = (double.Parse(datos[0]));
            }
            catch
            {
                return (latitud, longitud);
            }
            return (latitud, longitud);
        }

        public static string ErrorRegistroOpenpay(int errorCode)
        {
            string errorMensaje = string.Empty;
            switch (errorCode)
            {
                case 1003:
                    errorMensaje = "La operación no se pudo completar por que el valor de uno o más de los campos no es correcto.";
                    break;
                case 1004:
                    errorMensaje = "Un servicio necesario para el procesamiento de la transacción no se encuentra disponible.";
                    break;
                case 1005:
                    errorMensaje = "Uno de los recursos requeridos no existe.";
                    break;
                case 1006:
                    errorMensaje = "Ya existe una transacción con el mismo ID de orden.";
                    break;
                case 1007:
                    errorMensaje = "La transferencia de fondos entre una cuenta de banco o tarjeta y la cuenta de Openpay no fue aceptada.";
                    break;
                case 1008:
                    errorMensaje = "Una de las cuentas requeridas en la petición se encuentra desactivada.";
                    break;
                case 1012:
                    errorMensaje = "El monto transacción esta fuera de los limites permitidos.";
                    break;
                case 1013:
                    errorMensaje = "La operación no esta permitida para el recurso.";
                    break;
                case 1014:
                    errorMensaje = "La cuenta esta inactiva.";
                    break;
                case 1015:
                    errorMensaje = "No se ha obtenido respuesta de la solicitud realizada al servicio.";
                    break;
                case 1016:
                    errorMensaje = "El mail del comercio ya ha sido procesada.";
                    break;
                case 1017:
                    errorMensaje = "El gateway no se encuentra disponible en ese momento.";
                    break;
                case 1018:
                    errorMensaje = "El número de intentos de cargo es mayor al permitido.";
                    break;
                case 1020:
                    errorMensaje = "El número de dígitos decimales es inválido para esta moneda.";
                    break;
                case 2001:
                case 2003:
                    errorMensaje = "La cuenta de banco con esta CLABE ya se encuentra registrada.";
                    break;
                case 2004:
                    errorMensaje = "El número de tarjeta es invalido.";
                    break;
                case 2005:
                    errorMensaje = "La fecha de expiración de la tarjeta es anterior a la fecha actual.";
                    break;
                case 2009:
                    errorMensaje = "El código de seguridad de la tarjeta(CVV) es inválido.";
                    break;
                case 2011:
                    errorMensaje = "Tipo de tarjeta no soportada.";
                    break;
                case 3001:
                    errorMensaje = "La tarjeta fue declinada por el banco.";
                    break;
                case 3002:
                    errorMensaje = "La tarjeta ha expirado.";
                    break;
                case 3003:
                    errorMensaje = "La tarjeta no tiene fondos suficientes.";
                    break;
                case 3004:
                    errorMensaje = "La tarjeta ha sido identificada como una tarjeta robada.";
                    break;
                case 3005:
                    errorMensaje = "La tarjeta ha sido rechazada por el sistema antifraude.";
                    break;
                case 3010:
                    errorMensaje = "El banco ha restringido la tarjeta.";
                    break;
                default:
                    break;
            }
            return errorMensaje;
        }
    }
}

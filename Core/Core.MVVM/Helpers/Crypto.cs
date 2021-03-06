using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Core.MVVM.Helpers
{
    public class Crypto
    {
        public static string EncodePassword(string originalPassword) //Una vía
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = sha256.ComputeHash(encoding.GetBytes(originalPassword));

            return Convert.ToBase64String(stream);
        }

        public static string Encrypt(string strText, string strEncrKey = "MrN_35712369") //Dos vías
        { 
            byte[] IV = new byte[] { 0x00, 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07 }; 
            try 
            { 
                byte[] bykey = System.Text.Encoding.UTF8.GetBytes(strEncrKey.Substring(0, 8)); 
                byte[] InputByteArray = System.Text.Encoding.UTF8.GetBytes(strText); 
                DESCryptoServiceProvider des = new DESCryptoServiceProvider(); 
                MemoryStream ms = new MemoryStream(); 
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(bykey, IV), CryptoStreamMode.Write); 
                cs.Write(InputByteArray, 0, InputByteArray.Length); cs.FlushFinalBlock(); 
                return Convert.ToBase64String(ms.ToArray()); 
            } 
            catch (Exception ex) 
            { 
                return ex.Message; 
            } 
        }
    }
}

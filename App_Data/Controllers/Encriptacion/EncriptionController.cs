using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace WebSecureBookings
{
    public class EncriptionController
    {
        private static byte[] salt = Encoding.ASCII.GetBytes("ThisIsASecretSalt");
        public string Encrypt(string password)
        {
            byte[] encryptedBytes;

            using (RijndaelManaged aes = new RijndaelManaged())
            {
                Rfc2898DeriveBytes key = new Rfc2898DeriveBytes(password, salt);
                aes.Key = key.GetBytes(aes.KeySize / 8);
                aes.IV = key.GetBytes(aes.BlockSize / 8);

                ICryptoTransform encryptor = aes.CreateEncryptor(aes.Key, aes.IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        // No hay necesidad de escribir ningún texto en este caso.
                        // Solo estamos encriptando la contraseña.
                        cs.Write(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password));
                        cs.FlushFinalBlock();
                        encryptedBytes = ms.ToArray();
                    }
                }
            }

            return Convert.ToBase64String(encryptedBytes);
        }

    }

}
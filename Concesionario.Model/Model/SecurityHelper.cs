using System;
using System.Security.Cryptography;
using System.Text;

namespace ConcesionarioWEBFORM1111.Model
{
    public static class SecurityHelper
    {
        /// <summary>
        /// Genera el hash SHA256 de una contraseÃ±a en texto plano.
        /// </summary>
        /// <param name="password">ContraseÃ±a en texto plano</param>
        /// <returns>Hash SHA256 en formato hexadecimal</returns>
        public static string HashPassword(string password)
        {
            if (string.IsNullOrEmpty(password))
            {
                return string.Empty;
            }

            using (SHA256 sha256 = SHA256.Create())
            {
                // Convertir la contraseÃ±a a un array de bytes
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir el array de bytes a una cadena hexadecimal
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        /// <summary>
        /// Verifica si una contraseÃ±a en texto plano coincide con un hash existente.
        /// </summary>
        /// <param name="password">ContraseÃ±a en texto plano ingresada por el usuario</param>
        /// <param name="hash">Hash almacenado en la base de datos</param>
        /// <returns>True si coinciden, False en caso contrario</returns>
        public static bool VerifyPassword(string password, string hash)
        {
            string hashOfInput = HashPassword(password);
            // Usar comparaciÃ³n ordinal ignorando mayÃºsculas/minÃºsculas para evitar problemas,
            // aunque el hash SHA256 se genera en minÃºsculas en el mÃ©todo HashPassword.
            return string.Equals(hashOfInput, hash, StringComparison.OrdinalIgnoreCase);
        }
    }
}


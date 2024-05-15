namespace kers;
using System.Security.Cryptography;
using System.Text;

public class Core
{
    public static string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            // Хешируем пароль, преобразовывая его в массив байтов
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            // Преобразуем массив байтов в строку в шестнадцатеричном формате
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2")); // x2 форматирует каждый байт как двузначное шестнадцатеричное число
            }
            return builder.ToString();
        }
    }
}
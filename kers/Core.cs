using System.Linq;
using kers.Models;

namespace kers;
using System.Security.Cryptography;
using System.Text;

public class Core
{
    public static string HashPassword(string password)
    {
        using (SHA256 sha256Hash = SHA256.Create())
        {
            byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }

    public static bool CheckSeat(Trip selectTrip)
    {
        int countPassenger = Service.GetDbConnection().Tickets.Where(t => t.Fktripid == selectTrip.Id).Count();
        Auto selectedAuto = Service.GetDbConnection().Autos.FirstOrDefault(a => a.Id == selectTrip.Autoid);
        if (countPassenger < selectedAuto.Maxcountpassneger)
        {
            return true;
        }
        return false;
    }
}
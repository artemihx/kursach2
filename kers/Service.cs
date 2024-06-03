using kers.Models;

namespace kers;

public class Service
{
    private static Gr621PoaseContext? _db;

    public static Gr621PoaseContext GetDbConnection()
    {
        if (_db == null)
        {
            _db = new Gr621PoaseContext();
        }

        return _db;
    }
}
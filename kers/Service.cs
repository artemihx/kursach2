using kers.Models;

namespace kers;

public class Service
{
    private static PostgresContext? _db;

    public static PostgresContext GetDbConnection()
    {
        if (_db == null)
        {
            _db = new PostgresContext();
        }

        return _db;
    }
}
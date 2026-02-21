using NolMed.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.database
{
    public class DatabaseFunctions
    {
        public static bool AuthenticateUser(string passwordHash, string username)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                var user = database.Employees.FirstOrDefault(s => s.Username == username);
                if (user?.Password == passwordHash)
                {
                    return true;
                }
                return false;
            }
        }

        public static bool FindUsername(string username)
        {
            using(DatabaseContext database = new DatabaseContext())
            {
                var user = database.Employees.FirstOrDefault(u => u.Username == username);
                if (user != null)
                {
                    return true;
                }
                return false;
            }
        }
    }
}

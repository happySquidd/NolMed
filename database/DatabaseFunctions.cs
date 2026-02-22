using NolMed.model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BC = BCrypt.Net;

namespace NolMed.database
{
    public class DatabaseFunctions
    {
        public static bool AuthenticateUser(string password, string username)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                var user = database.Employees.FirstOrDefault(u => u.Username == username);
                Debug.WriteLine($"---- user password: {user.Password}");
                bool match = BC.BCrypt.Verify(password, user.Password);
                if (match)
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

        public static void RegisterUser(Employee newUser)
        {
            using(DatabaseContext database = new DatabaseContext())
            {
                database.Employees.Add(newUser);
                Debug.WriteLine("------------------- Added User");
                database.SaveChanges();
            }
            Debug.WriteLine("-------------------- Exited user add");
        }
    }
}

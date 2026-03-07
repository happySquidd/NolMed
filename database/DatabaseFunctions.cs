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
        public static bool AuthenticateUser(string password, string username, bool debug = false)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                var user = database.Employees.FirstOrDefault(u => u.Username == username);
                Debug.WriteLine($"---- user password: {user.Password}");
                if (debug) { return true; }
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

        public static void AssignPatientRoom(Patient? patient, int room_number)
        {
            using(DatabaseContext database = new DatabaseContext())
            {
                Room selectedRoom = database.Rooms.FirstOrDefault(r => r.RoomNumber == room_number);
                selectedRoom.PatientId = patient.Id;
                database.SaveChanges();
            }
        }

        public static List<Room> GetAllRooms()
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                List<Room> allRooms = new List<Room>();
                allRooms = database.Rooms.ToList();
                return allRooms;
            }
        }

        public static List<RoomOverviewBox> GetRoomsWithPatientNames()
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                List<RoomOverviewBox> rooms = new List<RoomOverviewBox>();
                var allRooms = GetAllRooms();
                foreach (Room room in allRooms)
                {
                    RoomOverviewBox roomInfo = new RoomOverviewBox();
                    // if no patient is assigned continue the loop
                    if (room.PatientId == null) { roomInfo.RoomNumber = room.RoomNumber; rooms.Add(roomInfo); continue; }
                    // grab patient by room patient id
                    Patient patientInfo = database.Patients.FirstOrDefault(p => p.Id == room.PatientId);
                    string FirstAndLastName = patientInfo.FirstName + " " + patientInfo.LastName;
                    // fill room info
                    roomInfo.PatientName = FirstAndLastName;
                    roomInfo.RoomNumber = room.RoomNumber;
                    rooms.Add(roomInfo);
                }
                return rooms;
            }
        }

        public static void RegisterPatient(string first_name, string last_name, DateOnly dob, string? blood_type = null, string? inpatient = null, int? mrn = null)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                Patient newPatient = new Patient
                {
                    FirstName = first_name,
                    LastName = last_name,
                    Dob = dob
                };
                database.Patients.Add(newPatient);
                database.SaveChanges();
            }
        }

        public static Patient FindPatient(string last_name, DateOnly dob)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                return database.Patients.FirstOrDefault(p => p.LastName == last_name && p.Dob == dob);
            }
        }

        public static void RemovePatientFromRoom(int room_number)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                var room = database.Rooms.FirstOrDefault(r => r.RoomNumber == room_number);
                room.PatientId = null;
                database.SaveChanges();
            }
        }
    }
}

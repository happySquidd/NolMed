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
            using (DatabaseContext database = new DatabaseContext())
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
            using (DatabaseContext database = new DatabaseContext())
            {
                database.Employees.Add(newUser);
                Debug.WriteLine("------------------- Added User");
                database.SaveChanges();
            }
            Debug.WriteLine("-------------------- Exited user add");
        }

        public static void AssignPatientRoom(Patient? patient, int room_id)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                Room selectedRoom = database.Rooms.FirstOrDefault(r => r.Id == room_id);
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
                var allRooms = database.Rooms.Where(r => r.RoomName != "Emergency room").ToList();
                foreach (Room room in allRooms)
                {
                    RoomOverviewBox roomInfo = new RoomOverviewBox();
                    // if no patient is assigned continue the loop
                    if (room.PatientId == null) { roomInfo.RoomNumber = room.RoomNumber; roomInfo.RoomName = room.RoomName; rooms.Add(roomInfo); continue; }
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

        public static Patient FindPatientById(int patientId)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                return database.Patients.FirstOrDefault(p => p.Id == patientId);
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

        public static bool PatientExists(string first_name, string last_name, DateOnly dob)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                var patient = database.Patients.FirstOrDefault(p => p.FirstName == first_name && p.LastName == last_name && p.Dob == dob);
                return patient != null ? true : false;
            }
        }

        public static void UpdatePatientInfo(Patient patient, string blood_type, int insurance_number, string insurance_name, string street, string city, string state, int zip, string country)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                // update patient blood type
                var patientToUpdate = database.Patients.FirstOrDefault(p => p.Id == patient.Id);
                patientToUpdate.Blood = blood_type;
                // update patient insurance info, if insurance doesn't exist create new insurance entry
                var patientInsurance = database.Insurance.FirstOrDefault(i => i.PatientId == patient.Id);
                if (patientInsurance != null)
                {
                    patientInsurance.Name = insurance_name;
                    patientInsurance.Number = insurance_number;
                }
                else
                {
                    Insurance newInsurance = new Insurance
                    {
                        PatientId = patient.Id,
                        Name = insurance_name,
                        Number = insurance_number
                    };
                    database.Insurance.Add(newInsurance);
                }
                // update patient address info, if address doesn't exist create new address entry
                var patientAddress = database.Billing.FirstOrDefault(a => a.PatientId == patient.Id);
                if (patientAddress != null)
                {
                    patientAddress.Street = street;
                    patientAddress.City = city;
                    patientAddress.State = state;
                    patientAddress.Zip = zip;
                    patientAddress.Country = country;
                }
                else
                {
                    Billing newAddress = new Billing
                    {
                        Street = street,
                        City = city,
                        State = state,
                        Zip = zip,
                        Country = country,
                        PatientId = patient.Id
                    };
                    database.Billing.Add(newAddress);
                }
                database.SaveChanges();
            }
        }

        public static Insurance GetPatientInsurance(Patient patient)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                var insurance = database.Insurance.FirstOrDefault(i => i.PatientId == patient.Id);
                return insurance;
            }
        }

        public static Billing GetPatientBilling(Patient patient)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                var billing = database.Billing.FirstOrDefault(b => b.PatientId == patient.Id);
                return billing;
            }
        }

        public static List<ErOverviewBox> GetEmergencyRoomsInfo()
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                var emergencyRooms = database.Rooms.Where(r => r.RoomName == "Emergency room").ToList();
                List<ErOverviewBox> erRoomsOverview = new List<ErOverviewBox>();
                foreach (Room room in emergencyRooms)
                {
                    ErOverviewBox erInfo = new ErOverviewBox();
                    // if no patient is assigned continue the loop
                    if (room.PatientId == null) { erInfo.RoomNumber = room.RoomNumber; erRoomsOverview.Add(erInfo); continue; }
                    // grab patient by room patient id
                    Patient patientInfo = database.Patients.FirstOrDefault(p => p.Id == room.PatientId);
                    string FirstAndLastName = patientInfo.FirstName + " " + patientInfo.LastName;
                    // get vitals
                    Visit visit = database.Visits.FirstOrDefault(v => v.PatientId == patientInfo.Id);
                    Vitals vitals = database.CurrentVitals.FirstOrDefault(v => v.VisitId == visit.Id);
                    // fill room info
                    erInfo.HeartRate = vitals.Bpm;
                    erInfo.Temperature = vitals.Temperature;
                    erInfo.PatientName = FirstAndLastName;
                    erInfo.RoomNumber = room.RoomNumber;
                    erRoomsOverview.Add(erInfo);
                }
                return erRoomsOverview;
            }
        }

        public static Patient GetPatient(string first_name, string last_name, DateOnly dob)
        {
            using (DatabaseContext database = new DatabaseContext())
            {
                var patient = database.Patients.FirstOrDefault(p => p.FirstName == first_name && p.LastName == last_name && p.Dob == dob);
                return patient;
            }
        }
    }
}

using Microsoft.EntityFrameworkCore;
using NolMed.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NolMed.database
{
    class DatabaseContext : DbContext
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Visit> Visits { get; set; }
        public DbSet<Vitals> CurrentVitals { get; set; }
        public DbSet<Insurance> Insurances { get; set; }
        public DbSet<Billing> Bililngs { get; set; }
        public DbSet<Drug> Drugs { get; set; }
        public DbSet<Allergy> Allergies { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<EmployeeAction> EmployeeActions { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = "Server=localhost;Database=NolMedNetworkDb;Integrated Security=True;TrustServerCertificate=True;";
                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}

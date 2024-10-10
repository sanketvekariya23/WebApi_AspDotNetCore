using Login_Register.Model;
using Login_Registor.Model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Login_Registor.Data
{
    public class DefaultContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                base.OnConfiguring(optionsBuilder.UseSqlServer(GetSqlServerConnection()));
            }
        } 
        public DbSet<User> Users { get; set; }
        public DbSet<Student> Student { get; set; }   
        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Salary> Salary { get; set; }
        public DbSet<Designation> Designations { get; set; }
        public DbSet<ExceptionInfo> ExceptionInfos { get; set; }
        public DbSet<Doctors> Doctors { get; set; }
        public DbSet<Appoinment> Appoinment { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Appoinment>().Property(a => a.PaidFees).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Appoinment>().Property(a => a.TotalFees).HasColumnType("decimal(18,2)");
            modelBuilder.Entity<Product>().Property(a => a.Price).HasColumnType("decimal(18,2)");
        }
        private static string GetSqlServerConnection()
        {
            SqlConnectionStringBuilder connectionBuilder = new()
            {
                ConnectTimeout = 0,
                DataSource = "DESKTOP-EBJT3NE\\SQLEXPRESS",
                UserID = "sa",
                Password = "Avni@003",
                InitialCatalog = "Sanket",
                TrustServerCertificate = true,
                MultipleActiveResultSets = true,
                IntegratedSecurity = true
            };
            return connectionBuilder.ConnectionString;
        }
    }
}
using EmployeeManagement.ViewModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Models
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }        
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Produits> Produits { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Remove Delete ONCASCADING
            foreach (var foreignKey in modelBuilder.Model.GetEntityTypes().SelectMany(e=>e.GetForeignKeys()))
            {
                foreignKey.DeleteBehavior = DeleteBehavior.Restrict;
            }
            modelBuilder.Entity<Employee>().HasData(
                new Employee()
                {
                    Id = 1,
                    Name = "Yassine ENNAJEM",
                    Departement = Departement.Networking,
                    Email = "YassineEnnajem68@gmail.com",
                    PhotoPath = "/images/emp.png",
                },
                new Employee()
                {
                    Id = 2,
                    Name = "Noureddine ABABAR",
                    Departement = Departement.IT,
                    Email = "SIMO68@gmail.com",
                    PhotoPath = "/images/emp.png"
                },
                new Employee()
                {
                    Id = 3,
                    Name = "Wiame Taki",
                    Departement = Departement.HR,
                    Email = "Wiame@gmail.com",
                    PhotoPath = "/images/emp.png"
                },
                new Employee()
                {
                    Id = 10,
                    Name = "Wiame Taki",
                    Departement = Departement.HR,
                    Email = "Wiame@gmail.com",
                    PhotoPath = "/images/emp.png",

                }
                ) ;
            
            //new Produits()
            //{
            //    id = 1,
            //    Photo = "/images/emp.png",
            //    Description = "test",
            //    Name = "Galaxy A32S",
            //    Prix = 500.5
            //}
        }
    }
}

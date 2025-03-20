﻿using Client_Invoice_System.Models;
using Microsoft.EntityFrameworkCore;

namespace Client_Invoice_System.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<OwnerProfile> Owners { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<ActiveClient> ActiveClients { get; set; }
        public DbSet<Resource> Resources { get; set; }
        public DbSet<ClientProfileCrossTable> ClientProfileCrosses { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<CountryCurrency> CountryCurrencies { get; set; }  // ✅ Added

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CountryCurrency>().HasData(
       new CountryCurrency { Id = 1, CountryName = "United States", CurrencyName = "US Dollar", Symbol = "$", CurrencyCode = "USD" },
       new CountryCurrency { Id = 2, CountryName = "United Kingdom", CurrencyName = "Pound Sterling", Symbol = "£", CurrencyCode = "GBP" },
       new CountryCurrency { Id = 3, CountryName = "European Union", CurrencyName = "Euro", Symbol = "€", CurrencyCode = "EUR" },
       new CountryCurrency { Id = 4, CountryName = "Japan", CurrencyName = "Japanese Yen", Symbol = "¥", CurrencyCode = "JPY" },
       new CountryCurrency { Id = 5, CountryName = "India", CurrencyName = "Indian Rupee", Symbol = "₹", CurrencyCode = "INR" },
       new CountryCurrency { Id = 6, CountryName = "Canada", CurrencyName = "Canadian Dollar", Symbol = "C$", CurrencyCode = "CAD" },
       new CountryCurrency { Id = 7, CountryName = "Australia", CurrencyName = "Australian Dollar", Symbol = "A$", CurrencyCode = "AUD" }
   );
            // ✅ ActiveClient & Client Relationship
            modelBuilder.Entity<ActiveClient>()
                .HasOne(ac => ac.Client)
                .WithOne(c => c.ActiveClient)
                .HasForeignKey<ActiveClient>(ac => ac.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Client>()
                .Property(c => c.PhoneNumber)
                .HasDefaultValue("N/A");
            modelBuilder.Entity<Client>()
                .Property(c => c.ClientIdentifier)
                .HasDefaultValueSql("NEWID()");

            // ✅ Client & Resources Relationship
            modelBuilder.Entity<Client>()
                .HasMany(c => c.Resources)
                .WithOne(r => r.Client)
                .HasForeignKey(r => r.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ Employee & Resources Relationship
            modelBuilder.Entity<Employee>()
                .HasMany(e => e.Resources)
                .WithOne(r => r.Employee)
                .HasForeignKey(r => r.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ Client Profile Cross Table (Many-to-Many)
            modelBuilder.Entity<ClientProfileCrossTable>()
                .HasKey(cpc => new { cpc.ClientId, cpc.EmployeeId });

            modelBuilder.Entity<ClientProfileCrossTable>()
                .HasOne(cpc => cpc.Client)
                .WithMany(c => c.ClientProfileCrosses)
                .HasForeignKey(cpc => cpc.ClientId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ClientProfileCrossTable>()
                .HasOne(cpc => cpc.Employee)
                .WithMany()
                .HasForeignKey(cpc => cpc.EmployeeId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ CountryCurrency Relationships
            modelBuilder.Entity<Client>()
                .HasOne(c => c.CountryCurrency)
                .WithMany()
                .HasForeignKey(c => c.CountryCurrencyId)
                .OnDelete(DeleteBehavior.Restrict); // Prevent deletion if referenced

            modelBuilder.Entity<OwnerProfile>()
                .HasOne(o => o.CountryCurrency)
                .WithMany()
                .HasForeignKey(o => o.CountryCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Invoice>()
                .HasOne(i => i.CountryCurrency)
                .WithMany()
                .HasForeignKey(i => i.CountryCurrencyId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}

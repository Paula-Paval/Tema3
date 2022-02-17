using System;
using Cinerva.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Cinerva.Data
{
    public class CinervaDbContext:DbContext
    {
        public CinervaDbContext()
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Country> Countries { get; set; }

        public DbSet<City> Cities { get; set; }
        public DbSet<PropertyType> PropertyTypes { get; set; }
        public DbSet<Property> Properties { get; set; }

        public DbSet<PropertyImage> PropertyImages { get; set; }
        public DbSet<GeneralFeature> GeneralFeatures { get; set; }
        public DbSet<PropertyFacility> PropertyFacilities { get; set; } 
        public DbSet<RoomCategory> RoomCategories { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<RoomFeature> RoomFeatures { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<RoomReservation> RoomReservations { get; set; }

        public CinervaDbContext(DbContextOptions<CinervaDbContext> options):base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=cinerva;Integrated Security=True")
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasOne(u => u.Role)
                .WithMany(r => r.Users)
                .HasForeignKey(u => u.RoleId);

            modelBuilder.Entity<City>()
                .HasOne(c => c.Country)
                .WithMany(x => x.Cities)
                .HasForeignKey(c => c.CountryId);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.PropertyType)
                .WithMany(t => t.Properties)
                .HasForeignKey(p => p.PropetyTypeId);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.City)
                .WithMany(c => c.Properties)
                .HasForeignKey(p => p.CityId);

            modelBuilder.Entity<Property>()
                .HasOne(p => p.User)
                .WithMany(u => u.Properties)
                .HasForeignKey(p => p.AdministratorId);

            modelBuilder.Entity<PropertyImage>()
                .HasOne(i => i.Property)
                .WithMany(p => p.PropertyImages)
                .HasForeignKey(i => i.PropetyId);

            modelBuilder.Entity<Property>()
                .HasMany(p => p.GeneralFeatures)
                .WithMany(g => g.Properties)
                .UsingEntity<PropertyFacility>(                    
                    x => x.HasOne(f => f.GeneralFeature)
                        .WithMany(g => g.PropertyFacilities)
                        .HasForeignKey(f => f.GeneralFeatureId),
                    x => x.HasOne(f => f.Property)
                        .WithMany(p => p.PropertyFacilities)
                        .HasForeignKey(f => f.PropertyId),
                    x => x.HasKey(f => new { f.PropertyId, f.GeneralFeatureId })
                );

            modelBuilder.Entity<Room>().Property(r => r.RoomCategoryId).HasColumnName("RoomCategory");

            modelBuilder.Entity<Room>()
                .HasOne(r => r.RoomCategory)
                .WithMany(c => c.Rooms)
                .HasForeignKey(r => r.RoomCategoryId);

            modelBuilder.Entity<Room>()
                    .HasOne(r => r.Property)
                    .WithMany(p => p.Rooms)
                    .HasForeignKey(r => r.PropertyId);

            modelBuilder.Entity<Room>()
             .HasMany(r => r.RoomFeatures)
             .WithMany(g => g.Rooms)
             .UsingEntity<RoomFacility>(
                 x => x.HasOne(f => f.RoomFeature)
                     .WithMany(g => g.RoomFacilities)
                     .HasForeignKey(f => f.RoomFeatureId),
                 x => x.HasOne(f => f.Room)
                     .WithMany(p => p.RoomFacilities)
                     .HasForeignKey(f => f.RoomId),
                 x => x.HasKey(f => new { f.RoomId, f.RoomFeatureId })
             );

            modelBuilder.Entity<Review>().HasKey(r => new { r.UserId, r.PropertyId});

            modelBuilder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Review>()
               .HasOne(r => r.Property)
               .WithMany(p => p.Reviews)
               .HasForeignKey(r => r.PropertyId);

            modelBuilder.Entity<Reservation>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reservations)
                .HasForeignKey(r => r.UserId);

            modelBuilder.Entity<Room>()
                .HasMany(r => r.Reservations)
                .WithMany(rez => rez.Rooms)
                .UsingEntity<RoomReservation>(
                    x => x.HasOne(y => y.Reservation)
                        .WithMany(rez => rez.RoomReservations)
                       .HasForeignKey(y => y.ReservationId),
                    x => x.HasOne(y => y.Room)
                        .WithMany(r => r.RoomReservations)
                        .HasForeignKey(y => y.RoomId)
                    );
        }

    }
}

using Microsoft.EntityFrameworkCore;
using utcAltkomDevices.DbServices.DbModelsBuilders;
using utcAltkomDevices.Models;

namespace utcAltkomDevices.DbServices
{
    public class UtcContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<User> Users { get; set; }

        public UtcContext(DbContextOptions<UtcContext> options) : base(options)
        {
            // Make sure the DB is present, if not create it. You cant migrate/update/downgrade it though
            //this.Database.EnsureCreated();

          // this.Database.Migrate();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //You can do this one by one like this:
            modelBuilder.Entity<Device>()
                .Property(p => p.Name)
                .HasMaxLength(100)
                .IsUnicode(false);

            //Or better yet, just create a builder class:
            modelBuilder
                .ApplyConfiguration(new CustomerConfiguration())
                .ApplyConfiguration(new DeviceConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }
}

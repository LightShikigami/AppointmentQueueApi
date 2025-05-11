using AppointmentQueueApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentQueueApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Appointment> Appointments { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}

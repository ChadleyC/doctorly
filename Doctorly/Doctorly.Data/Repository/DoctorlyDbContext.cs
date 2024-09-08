using Doctorly.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Doctorly.Data.Repository;

public class DoctorlyDbContext : DbContext
{
    public DoctorlyDbContext(DbContextOptions<DoctorlyDbContext> options) : base(options)
    {
    }
    public DbSet<Event> Events { get; set; }
    public DbSet<Attendee> Attendees { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Event>().HasKey(x => x.Id);
        modelBuilder.Entity<Attendee>().HasKey(x => x.Id);
        modelBuilder.Entity<Event>().HasMany<Attendee>(x => x.Attendees);
    }
}
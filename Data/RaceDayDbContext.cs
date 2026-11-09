
using Microsoft.EntityFrameworkCore;
using RaceDay.Api.Models;
using RaceRoute = RaceDay.Api.Models. Route; 
namespace RaceDay.Api.Data;

// Connects the RaceDay models to the SQL Server database.
public class RaceDayDbContext : DbContext
{
    public RaceDayDbContext(DbContextOptions<RaceDayDbContext> options)
        : base(options)
    {
    }

    // Database tables.
    public DbSet<AppUser> Users => Set<AppUser>();
    public DbSet<Event> Events => Set<Event>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<RaceDay.Api.Models.Route> Routes { get; set;}
    public DbSet<Enrolment> Enrolments => Set<Enrolment>();
    public DbSet<Result> Results => Set<Result>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Each user email must be unique.
        modelBuilder.Entity<AppUser>()
            .HasIndex(u => u.Email)
            .IsUnique();

        // One organiser can create many events.
        modelBuilder.Entity<Event>()
            .HasOne(e => e.Organiser)
            .WithMany()
            .HasForeignKey(e => e.OrganiserId)
            .OnDelete(DeleteBehavior.Restrict);

        // One event can have many categories.
        modelBuilder.Entity<Category>()
            .HasOne(c => c.Event)
            .WithMany(e => e.Categories)
            .HasForeignKey(c => c.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        // One event can have many routes.
        modelBuilder.Entity<RaceRoute>()
            .HasOne(r => r.Event)
            .WithMany(e => e.Routes)
            .HasForeignKey(r => r.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        // One participant can have many enrolments.
        modelBuilder.Entity<Enrolment>()
            .HasOne(e => e.Participant)
            .WithMany()
            .HasForeignKey(e => e.ParticipantId)
            .OnDelete(DeleteBehavior.Restrict);

        // One event can have many enrolments.
        modelBuilder.Entity<Enrolment>()
            .HasOne(e => e.Event)
            .WithMany()
            .HasForeignKey(e => e.EventId)
            .OnDelete(DeleteBehavior.Restrict);

        // One category can have many enrolments.
        modelBuilder.Entity<Enrolment>()
            .HasOne(e => e.Category)
            .WithMany()
            .HasForeignKey(e => e.CategoryId)
            .OnDelete(DeleteBehavior.Restrict);

        // An enrolment can have at most one result.
        modelBuilder.Entity<Result>()
            .HasOne(r => r.Enrolment)
            .WithOne()
            .HasForeignKey<Result>(r => r.EnrolmentId)
            .OnDelete(DeleteBehavior.Cascade);

        // Store event distances with two decimal places.
        modelBuilder.Entity<Event>()
            .Property(e => e.Distance)
            .HasPrecision(8, 2);

        modelBuilder.Entity<RaceRoute>()
            .Property(r => r.Distance)
            .HasPrecision(8, 2);
    }
}

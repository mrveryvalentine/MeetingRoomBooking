using MeetingRoomBooking.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBooking.Api.Data;

public class BookingDbContext(DbContextOptions<BookingDbContext> options)
    : DbContext(options)
{
    public DbSet<Room> Rooms => Set<Room>();

    public DbSet<Booking> Bookings => Set<Booking>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BookingDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
using MeetingRoomBooking.Api.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MeetingRoomBooking.Api.Data.Configurations;

public class RoomConfiguration : IEntityTypeConfiguration<Room>
{
    public void Configure(EntityTypeBuilder<Room> builder)
    {
        builder.ToTable("Rooms");

        builder.HasKey(r => r.Id);

        builder.Property(r => r.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(r => r.Capacity)
            .IsRequired();

        builder.HasData(
            new Room
            {
                Id = 1,
                Name = "Meeting Room 1",
                Capacity = 4
            },
            new Room
            {
                Id = 2,
                Name = "Meeting Room 2",
                Capacity = 8
            },
            new Room
            {
                Id = 3,
                Name = "Meeting Room 3",
                Capacity = 20
            },
            new Room
            {
                Id = 4,
                Name = "Meeting Room 4",
                Capacity = 10
            },
            new Room
            {
                Id = 5,
                Name = "Meeting Room 5",
                Capacity = 15
            });
    }
}
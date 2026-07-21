using MeetingRoomBooking.Api.Data;
using MeetingRoomBooking.Api.DTOs.Rooms;
using MeetingRoomBooking.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBooking.Api.Services;

public class RoomService : IRoomService
{
    private readonly BookingDbContext _context;

    public RoomService(BookingDbContext context)
    {
        _context = context;
    }

    public async Task<List<RoomResponse>> GetAllAsync()
    {
        return await _context.Rooms
            .AsNoTracking()
            .OrderBy(r => r.Name)
            .Select(r => new RoomResponse(
                r.Id,
                r.Name,
                r.Capacity))
            .ToListAsync();
    }
}
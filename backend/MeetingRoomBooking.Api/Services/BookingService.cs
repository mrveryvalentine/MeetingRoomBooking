using MeetingRoomBooking.Api.Data;
using MeetingRoomBooking.Api.DTOs.Bookings;
using MeetingRoomBooking.Api.Exceptions;
using MeetingRoomBooking.Api.Models;
using MeetingRoomBooking.Api.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace MeetingRoomBooking.Api.Services;

public class BookingService : IBookingService
{
    private readonly BookingDbContext _context;

    public BookingService(BookingDbContext context)
    {
        _context = context;
    }

    public async Task<BookingResponse> CreateAsync(CreateBookingRequest request)
    {
        await ValidateBookingAsync(
            request.RoomId,
            request.StartTime,
            request.EndTime);

        var booking = new Booking
        {
            MeetingTitle = request.MeetingTitle,
            Organizer = request.Organizer,
            RoomId = request.RoomId,
            StartTime = request.StartTime,
            EndTime = request.EndTime
        };

        await _context.Bookings.AddAsync(booking);

        await _context.SaveChangesAsync();

        var createdBooking = await _context.Bookings
            .AsNoTracking()
            .Include(b => b.Room)
            .SingleAsync(b => b.Id == booking.Id);

        return MapToResponse(createdBooking);
    }

    public async Task DeleteAsync(int id)
    {
        var booking = await _context.Bookings.FindAsync(id);

        if (booking is null)
        {
            throw new BookingNotFoundException();
        }

        _context.Bookings.Remove(booking);

        await _context.SaveChangesAsync();
    }

    public async Task<List<BookingResponse>> GetAllAsync()
    {
        return await _context.Bookings
            .AsNoTracking()
            .Include(b => b.Room)
            .OrderBy(b => b.StartTime)
            .Select(b => new BookingResponse
            {
                Id = b.Id,
                MeetingTitle = b.MeetingTitle,
                Organizer = b.Organizer,
                RoomId = b.RoomId,
                RoomName = b.Room!.Name,
                StartTime = b.StartTime,
                EndTime = b.EndTime
            })
            .ToListAsync();
    }

    public async Task<BookingResponse?> GetByIdAsync(int id)
    {
        return await _context.Bookings
            .AsNoTracking()
            .Include(b => b.Room)
            .Where(b => b.Id == id)
            .Select(b => new BookingResponse
            {
                Id = b.Id,
                MeetingTitle = b.MeetingTitle,
                Organizer = b.Organizer,
                RoomId = b.RoomId,
                RoomName = b.Room!.Name,
                StartTime = b.StartTime,
                EndTime = b.EndTime
            })
            .SingleOrDefaultAsync();
    }

    public async Task UpdateAsync(
        int id,
        UpdateBookingRequest request)
    {
        var booking = await _context.Bookings.FindAsync(id);

        if (booking is null)
        {
            throw new BookingNotFoundException();
        }

        await ValidateBookingAsync(
            request.RoomId,
            request.StartTime,
            request.EndTime,
            id);

        booking.MeetingTitle = request.MeetingTitle;
        booking.Organizer = request.Organizer;
        booking.RoomId = request.RoomId;
        booking.StartTime = request.StartTime;
        booking.EndTime = request.EndTime;

        await _context.SaveChangesAsync();
    }

    private async Task ValidateBookingAsync(
        int roomId,
        DateTime startTime,
        DateTime endTime,
        int? bookingId = null)
    {
        if (startTime >= endTime)
        {
            throw new InvalidBookingException(
                "Start time must be before end time.");
        }

        var roomExists = await _context.Rooms
            .AnyAsync(r => r.Id == roomId);

        if (!roomExists)
        {
            throw new RoomNotFoundException();
        }

        var hasOverlap = await HasOverlapAsync(
            roomId,
            startTime,
            endTime,
            bookingId);

        if (hasOverlap)
        {
            throw new BookingConflictException();
        }
    }

    private async Task<bool> HasOverlapAsync(
        int roomId,
        DateTime startTime,
        DateTime endTime,
        int? bookingId = null)
    {
        return await _context.Bookings
            .AnyAsync(b =>
                b.RoomId == roomId &&
                (!bookingId.HasValue || b.Id != bookingId.Value) &&
                startTime < b.EndTime &&
                endTime > b.StartTime);
    }

    private static BookingResponse MapToResponse(Booking booking)
    {
        return new BookingResponse
        {
            Id = booking.Id,
            MeetingTitle = booking.MeetingTitle,
            Organizer = booking.Organizer,
            RoomId = booking.RoomId,
            RoomName = booking.Room?.Name ?? string.Empty,
            StartTime = booking.StartTime,
            EndTime = booking.EndTime
        };
    }
}
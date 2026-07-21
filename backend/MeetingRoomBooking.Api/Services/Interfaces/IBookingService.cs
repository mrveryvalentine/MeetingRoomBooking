using MeetingRoomBooking.Api.DTOs.Bookings;

namespace MeetingRoomBooking.Api.Services.Interfaces;

public interface IBookingService
{
    Task<List<BookingResponse>> GetAllAsync();

    Task<BookingResponse?> GetByIdAsync(int id);

    Task<BookingResponse> CreateAsync(CreateBookingRequest request);

    Task UpdateAsync(int id, UpdateBookingRequest request);

    Task DeleteAsync(int id);
}
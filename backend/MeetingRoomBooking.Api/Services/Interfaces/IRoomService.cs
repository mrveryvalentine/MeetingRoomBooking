using MeetingRoomBooking.Api.DTOs.Rooms;

namespace MeetingRoomBooking.Api.Services.Interfaces;

public interface IRoomService
{
    Task<List<RoomResponse>> GetAllAsync();
}
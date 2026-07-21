namespace MeetingRoomBooking.Api.DTOs.Rooms;

public sealed record RoomResponse(
    int Id,
    string Name,
    int Capacity);
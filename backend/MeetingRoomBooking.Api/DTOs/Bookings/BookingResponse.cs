namespace MeetingRoomBooking.Api.DTOs.Bookings;

public class BookingResponse
{
    public int Id { get; set; }

    public string MeetingTitle { get; set; } = string.Empty;

    public string Organizer { get; set; } = string.Empty;

    public int RoomId { get; set; }

    public string RoomName { get; set; } = string.Empty;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }
}
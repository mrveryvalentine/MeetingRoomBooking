namespace MeetingRoomBooking.Api.Models;

public class Booking
{
    public int Id { get; set; }

    public string MeetingTitle { get; set; } = string.Empty;

    public string Organizer { get; set; } = string.Empty;

    public int RoomId { get; set; }

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public Room? Room { get; set; }
}
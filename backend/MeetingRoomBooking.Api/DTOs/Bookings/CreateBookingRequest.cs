using System.ComponentModel.DataAnnotations;

namespace MeetingRoomBooking.Api.DTOs.Bookings;

public class CreateBookingRequest
{
    [Required]
    [StringLength(200)]
    [RegularExpression(@".*\S.*", ErrorMessage = "Meeting title is required.")]
    public string MeetingTitle { get; set; } = string.Empty;

    [Required]
    [StringLength(100)]
    [RegularExpression(@".*\S.*", ErrorMessage = "Organizer is required.")]
    public string Organizer { get; set; } = string.Empty;

    [Required]
    public int RoomId { get; set; }

    [Required]
    public DateTime StartTime { get; set; }

    [Required]
    public DateTime EndTime { get; set; }
}
namespace MeetingRoomBooking.Api.Exceptions;

public class RoomNotFoundException : Exception
{
    public RoomNotFoundException()
        : base("The specified room could not be found.")
    {
    }

    public RoomNotFoundException(string message)
        : base(message)
    {
    }
}
namespace MeetingRoomBooking.Api.Exceptions;

public class InvalidBookingException : Exception
{
    public InvalidBookingException()
        : base("The booking request is invalid.")
    {
    }

    public InvalidBookingException(string message)
        : base(message)
    {
    }
}
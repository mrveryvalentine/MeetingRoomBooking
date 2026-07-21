namespace MeetingRoomBooking.Api.Exceptions;

public class BookingNotFoundException : Exception
{
    public BookingNotFoundException()
        : base("The specified booking could not be found.")
    {
    }

    public BookingNotFoundException(string message)
        : base(message)
    {
    }
}
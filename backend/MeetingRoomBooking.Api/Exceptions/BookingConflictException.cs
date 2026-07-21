namespace MeetingRoomBooking.Api.Exceptions;

public class BookingConflictException : Exception
{
    public BookingConflictException()
        : base("The room is already booked during the selected time.")
    {
    }

    public BookingConflictException(string message)
        : base(message)
    {
    }
}
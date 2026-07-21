using Microsoft.AspNetCore.Diagnostics;
using System.Net;

namespace MeetingRoomBooking.Api.Exceptions;

public class GlobalExceptionHandler : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext,
        Exception exception,
        CancellationToken cancellationToken)
    {
        var (statusCode, message) = exception switch
        {
            InvalidBookingException ex =>
                (HttpStatusCode.BadRequest, ex.Message),

            RoomNotFoundException ex =>
                (HttpStatusCode.NotFound, ex.Message),

            BookingConflictException ex =>
                (HttpStatusCode.Conflict, ex.Message),

            BookingNotFoundException ex =>
                (HttpStatusCode.NotFound, ex.Message),

            _ =>
                (HttpStatusCode.InternalServerError,
                "An unexpected error occurred.")
        };

        httpContext.Response.StatusCode = (int)statusCode;

        httpContext.Response.ContentType = "application/json";

        await httpContext.Response.WriteAsJsonAsync(
            new
            {
                status = (int)statusCode,
                message
            },
            cancellationToken);

        return true;
    }
}
using MeetingRoomBooking.Api.DTOs.Bookings;
using MeetingRoomBooking.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BookingsController : ControllerBase
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<BookingResponse>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<BookingResponse>>> GetAll()
    {
        var bookings = await _bookingService.GetAllAsync();

        return Ok(bookings);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookingResponse>> GetById(int id)
    {
        var booking = await _bookingService.GetByIdAsync(id);

        if (booking is null)
        {
            return NotFound();
        }

        return Ok(booking);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookingResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<ActionResult<BookingResponse>> Create(
        CreateBookingRequest request)
    {
        var booking = await _bookingService.CreateAsync(request);

        return CreatedAtAction(
            nameof(GetById),
            new { id = booking.Id },
            booking);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateBookingRequest request)
    {
        await _bookingService.UpdateAsync(id, request);

        return NoContent();
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _bookingService.DeleteAsync(id);

        return NoContent();
    }
}
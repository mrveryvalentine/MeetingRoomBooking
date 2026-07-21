using MeetingRoomBooking.Api.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace MeetingRoomBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomsController(IRoomService roomService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var rooms = await roomService.GetAllAsync();

        return Ok(rooms);
    }
}
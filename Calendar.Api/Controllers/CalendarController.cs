using Calendar.Api.DTO;
using Calendar.Api.Logics;
using Calendar.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CalendarController : Controller
    {
        private readonly CalendarLogic _logic;
        public CalendarController(CalendarLogic logic)
        {
            _logic = logic;
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetCalendarByUserId(Guid userId, [FromQuery] PaginationRequest paginationRequest, [FromQuery] string? search = "")
        {
            try
            {
                var data = await _logic.GetCalendarByUserId(userId, search);
                var pagination = PaginationLogic.PaginateData(data, paginationRequest);
                return Ok(pagination);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCalendar(CalendarDTO_POST dto)
        {
            try
            {
                bool ok = await _logic.AddCalendar(dto);
                if (ok) 
                {
                    return Ok();
                }
                return BadRequest("Failed to add calendar");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPut("{calendarId}")]
        public async Task<IActionResult> UpdateCalendar(Guid calendarId, CalendarDTO_PUT dto)
        {
            try
            {
                bool ok = await _logic.UpdateCalendar(calendarId, dto);
                if (ok)
                {
                    return Ok();
                }
                return BadRequest("Failed to update calendar");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpDelete("{calendarId}")]
        public async Task<IActionResult> DeleteCalendar(Guid calendarId)
        {
            try
            {
                bool ok = await _logic.DeleteCalendar(calendarId);
                if (ok)
                {
                    return Ok();
                }
                return BadRequest("Failed to delete calendar");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
    }
}

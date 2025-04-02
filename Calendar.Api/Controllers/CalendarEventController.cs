using Calendar.Api.DTO;
using Calendar.Api.Logics;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class CalendarEventController : Controller
    {
        private readonly CalendarEventLogic _logic;
        public CalendarEventController(CalendarEventLogic logic)
        {
            _logic = logic;
        }

        [HttpGet("Calendar/{calendarId}")]
        public async Task<IActionResult> GetCalendarEvent(Guid calendarId, [FromQuery] DateTime dateTimeStarted, [FromQuery] DateTime dateTimeEnded)
        {
            try
            {
                var data = await _logic.GetCalendarEvent(calendarId, dateTimeStarted, dateTimeEnded);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpGet("{calendarEventId}")]
        public async Task<IActionResult> GetCalendarEventByCalendarEventId(Guid calendarEventId)
        {
            try
            {
                var data = await _logic.GetCalendarEventByCalendarEventId(calendarEventId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpGet("{calendarEventGroupId}/Group")]
        public async Task<IActionResult> GetCalendarEventByCalendarEventGroupId(Guid calendarEventGroupId)
        {
            try
            {
                var data = await _logic.GetCalendarEventByCalendarEventGroupId(calendarEventGroupId);
                return Ok(data);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCalendarEvent(List<CalendarEventDTO_POST> dto)
        {
            try
            {
                bool ok = await _logic.AddCalendarEvent(dto);
                if (ok)
                {
                    return Ok();
                }
                return BadRequest("Failed to add calendar event");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPut("{calendarEventId}")]
        public async Task<IActionResult> MoveResizeEvent(Guid calendarEventId, CalendarEventDTO_PUT dto)
        {
            try
            {
                bool ok = await _logic.MoveResizeEvent(calendarEventId, dto);
                if (ok)
                {
                    return Ok();
                }
                return BadRequest("Failed to add calendar event");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCalendarEvent([FromQuery]Guid calendarEventId, [FromQuery] Guid calendarEventGroupId, [FromQuery] string actionStatus, List<CalendarEventDTO_POST> dto)
        {
            try
            {
                bool ok = await _logic.UpdateCalendarEvent(calendarEventId, calendarEventGroupId, actionStatus,  dto);
                if (ok)
                {
                    return Ok();
                }
                return BadRequest("Failed to update calendar event");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
       

        [HttpDelete("{calendarEventId}")]
        public async Task<IActionResult> DeleteCalendarEventByCalendarEventId(Guid calendarEventId)
        {
            try
            {
                bool ok = await _logic.DeleteCalendarEventByCalendarEventId(calendarEventId);
                if (ok)
                {
                    return Ok();
                }
                return BadRequest("Failed to delete event");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpDelete("{calendarEventGroupId}/Group")]
        public async Task<IActionResult> DeleteCalendarEventByCalendarEventGroupId(Guid calendarEventGroupId)
        {
            try
            {
                bool ok = await _logic.DeleteCalendarEventByCalendarEventGroupId(calendarEventGroupId);
                if (ok)
                {
                    return Ok();
                }
                return BadRequest("Failed to delete event");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
    }
}

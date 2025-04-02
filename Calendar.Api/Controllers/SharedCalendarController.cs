using Calendar.Api.Logics;
using Calendar.Api.Models;
using Calendar.API.DTO;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.API.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class SharedCalendarController : Controller
    {
        private readonly SharedCalendarLogic _sharedCalendar;
        public SharedCalendarController(SharedCalendarLogic sharedCalendar)
        {
            _sharedCalendar = sharedCalendar;
        }

        [HttpGet("User/{userId}")]
        public async Task<IActionResult> GetSharedCalendarByUserId(Guid userId)
        {
            try
            {
                var data = _sharedCalendar.GetSharedCalendarByUserId(userId);
                return Ok(data);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpGet("{calendarId}")]
        public async Task<IActionResult> GetAllSharedCalendarByCalendarId(Guid calendarId, [FromQuery] PaginationRequest paginationRequest, [FromQuery] string? search = "") 
        {
            try
            {
                var data = await _sharedCalendar.GetAllSharedCalendarByCalendarId(calendarId, search);
                var pagination = PaginationLogic.PaginateData(data, paginationRequest);
                return Ok(pagination);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        // Grant user
        [HttpPost]
        public async Task<IActionResult> GrantUser(SharedCalendarDTO_POST sharedCalendar)
        {
            try
            {
                bool isAdded = await _sharedCalendar.GrantUser(sharedCalendar);
                if (isAdded)
                {
                    return Ok("User granted");
                }
                return Json("Failed to permit user");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        // Revoke user
        [HttpDelete("{sharedCalendarId}")]
        public async Task<IActionResult> RevokeUser(Guid sharedCalendarId)
        {
            try
            {
                bool isDeleted = await _sharedCalendar.RevokeUser(sharedCalendarId);
                if (isDeleted)
                {
                    return Ok("User revoked");
                }
                return Json("Failed to revoke user");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
    }
}

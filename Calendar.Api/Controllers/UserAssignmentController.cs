using Calendar.Api.DTO;
using Calendar.Api.Logics;
using Microsoft.AspNetCore.Mvc;

namespace Calendar.Api.Controllers
{
    [ApiController, Route("api/[controller]")]
    public class UserAssignmentController : Controller
    {
        private readonly UserAssignmentLogic _logic;
        public UserAssignmentController(UserAssignmentLogic logic)
        {
            _logic = logic;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> LoginUser(UserAssignmentDTO_LOGIN dto)
        {
            try
            {
                var data = await _logic.LoginUser(dto);
                if(data == null)
                {
                    return BadRequest("Invalid Email or Password");
                }
                return Ok(data);
            }
            catch (Exception ex) 
            { 
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser([FromForm] UserAssignmentDTO_POST dto)
        {
            try
            {
                string response = await _logic.RegisterUser(dto);
                if (response == "success")
                {
                    return Ok();
                }
                else if(response == "exists")
                {
                    return BadRequest("Account already exists");
                }
                else
                {
                    return BadRequest("Failed to register account");
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }

        [HttpPut("User/{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromForm] UserAssignmentDTO_POST dto)
        {
            try
            {
                bool ok = await _logic.UpdateUser(userId, dto);
                if (ok)
                {
                    return Ok();
                }
                return BadRequest("Failed to register account");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message + ex.StackTrace);
            }
        }
    }
}

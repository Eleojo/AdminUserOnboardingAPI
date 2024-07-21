using Core.AdminUserServices;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;

        public AdminUserController(IAdminUserService adminUserService)
        {
            _adminUserService = adminUserService;
        }


        [HttpPost("onboard-admin-user")]
        public async Task<IActionResult> OnboardAdmin([FromBody] AdminUserDto adminUserDto)
        {
            if (adminUserDto == null)
            {
                return BadRequest("Admin user data is null.");
            }

            var result = await _adminUserService.OnboardAdminAsync(adminUserDto);

            if (result.Succeeded)
            {
                return Ok("Admin user onboarded successfully.");
            }
            else
            {
                // Extract the error messages from the result
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return BadRequest($"Failed to onboard admin user: {errors}");
            }
        }
    }
}



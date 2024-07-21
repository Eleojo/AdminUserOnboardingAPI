using Core.AdminUserServices;
using Data.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminUserController : ControllerBase
    {
        private readonly IAdminUserService _adminUserService;
        private readonly ILogger<AdminUserController> _logger;

        public AdminUserController(IAdminUserService adminUserService, ILogger<AdminUserController> logger)
        {
            _adminUserService = adminUserService;
            _logger = logger;
        }

        [HttpPost("onboard-admin-user")]
        public async Task<IActionResult> OnboardAdmin([FromBody] AdminUserDto adminUserDto)
        {
            if (adminUserDto == null)
            {
                _logger.LogWarning("Received a null AdminUserDto.");
                return BadRequest("Admin user data is null.");
            }

            try
            {
                var result = await _adminUserService.OnboardAdminAsync(adminUserDto);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Admin user onboarded successfully: {Email}", adminUserDto.Email);
                    return Ok("Admin user onboarded successfully.");
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogWarning("Failed to onboard admin user: {Email}. Errors: {Errors}", adminUserDto.Email, errors);
                    return BadRequest($"Failed to onboard admin user: {errors}");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while onboarding admin user: {Email}", adminUserDto.Email);
                return StatusCode(500, "An internal server error occurred.");
            }
        }
    }
}

using Data.Dtos;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;

namespace Core.AdminUserServices
{
    public class AdminUserService : IAdminUserService
    {
        private readonly UserManager<AdminUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<AdminUserService> _logger;

        public AdminUserService(UserManager<AdminUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<AdminUserService> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }

        public async Task<IdentityResult> OnboardAdminAsync(AdminUserDto adminUserDto)
        {
            if (adminUserDto == null)
            {
                _logger.LogWarning("AdminUserDto is null.");
                throw new ArgumentNullException(nameof(adminUserDto), "Admin user data cannot be null.");
            }

            var adminUser = new AdminUser
            {
                FullName = adminUserDto.FullName,
                UserName = adminUserDto.Email,
                Email = adminUserDto.Email,
                Role = adminUserDto.Role
            };

            try
            {
                var result = await _userManager.CreateAsync(adminUser, adminUserDto.Password);

                if (result.Succeeded)
                {
                    if (!await _roleManager.RoleExistsAsync(adminUser.Role.ToString()))
                    {
                        var roleResult = await _roleManager.CreateAsync(new IdentityRole(adminUser.Role.ToString()));
                        if (!roleResult.Succeeded)
                        {
                            var roleErrors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                            _logger.LogWarning("Failed to create role '{Role}'. Errors: {Errors}", adminUser.Role, roleErrors);
                            return IdentityResult.Failed(roleResult.Errors.ToArray());
                        }
                    }

                    var addToRoleResult = await _userManager.AddToRoleAsync(adminUser, adminUser.Role.ToString());
                    if (!addToRoleResult.Succeeded)
                    {
                        var roleErrors = string.Join(", ", addToRoleResult.Errors.Select(e => e.Description));
                        _logger.LogWarning("Failed to assign role '{Role}' to user '{Email}'. Errors: {Errors}", adminUser.Role, adminUser.Email, roleErrors);
                        return IdentityResult.Failed(addToRoleResult.Errors.ToArray());
                    }

                    _logger.LogInformation("Admin user '{Email}' onboarded successfully with role '{Role}'.", adminUser.Email, adminUser.Role);
                }
                else
                {
                    var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                    _logger.LogWarning("Failed to create admin user '{Email}'. Errors: {Errors}", adminUser.Email, errors);
                }

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while onboarding admin user '{Email}'.", adminUser.Email);
                throw; /// Re-throw the exception to ensure it is handled by the caller if necessary
            }
        }
    }
}

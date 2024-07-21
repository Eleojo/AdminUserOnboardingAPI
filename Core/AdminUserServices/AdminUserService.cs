using Data.Dtos;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AdminUserServices
{
    public class AdminUserService : IAdminUserService
    {
        private readonly UserManager<AdminUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminUserService(UserManager<AdminUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IdentityResult> OnboardAdminAsync(AdminUserDto adminUserDto)
        {
            var adminUser = new AdminUser
            {
                FullName = adminUserDto.FullName,
                UserName = adminUserDto.Email,
                Email = adminUserDto.Email,
                Role = adminUserDto.Role
            };

            var result = await _userManager.CreateAsync(adminUser, adminUserDto.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync(adminUser.Role.ToString()))
                {
                    await _roleManager.CreateAsync(new IdentityRole(adminUser.Role.ToString()));
                }

                await _userManager.AddToRoleAsync(adminUser, adminUser.Role.ToString());
            }

            return result;
        }
    }
}


using Data.Dtos;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.AdminUserServices
{


    public interface IAdminUserService
    {
        Task<IdentityResult> OnboardAdminAsync(AdminUserDto adminUserDto);
    }

}


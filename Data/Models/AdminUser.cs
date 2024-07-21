using Data.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class AdminUser:IdentityUser
    {
        [Required]
        public string FullName { get; set; }
        public UserRole Role { get; set; }
    }
}

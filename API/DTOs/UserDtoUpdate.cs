using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.DTOs
{
    public class UserDtoUpdate
    {
        [Required]
        public string UserName { get; set; }

        public string Password { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public string Role { get; set; }
    }
}
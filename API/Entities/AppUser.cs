using Microsoft.AspNetCore.Identity;

namespace API.Entities
{
    public class AppUser : IdentityUser<int>
    {
      
      public string DisplayName { get; set; } 
      public Car? Car { get; set; }
      public int? CarId { get; set; }
      public ICollection<AppUserRole> UserRoles { get; set; }
    }
}
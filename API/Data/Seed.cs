using System.Text.Json;
using API.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class Seed
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            if (await userManager.Users.AnyAsync()) return;

            var userData = await System.IO.File.ReadAllTextAsync("Data/UserSeedData.json");
            var users = JsonSerializer.Deserialize<List<AppUser>>(userData);
            if (users == null) return;

            var roles = new List<AppRole>
            {
                new AppRole{Name = "Nieaktywowany"},
                new AppRole{Name = "Operator"},
                new AppRole{Name = "Administrator"}
            };

            foreach (var role in roles)
            {
                await roleManager.CreateAsync(role);
            }

            foreach (var user in users)
            {
                user.UserName = user.UserName.ToLower();
                await userManager.CreateAsync(user, "Pa$$w0rd");
                if (user.UserName == "dbadmin")
                    await userManager.AddToRolesAsync(user, new[] { "Administrator" });
                else if (user.UserName == "dbuser")
                    await userManager.AddToRoleAsync(user, "Operator");
                else
                    await userManager.AddToRoleAsync(user, "Nieaktywowany");

            }
        }

    }
}
using Microsoft.AspNetCore.Identity;
using Million.Domain.Entities;

namespace Million.TestUserCreator
{
    public class IdentitySeeder
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public IdentitySeeder(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task SeedAsync()
        {
            var usersToSeed = new[]
            {
                new
                {
                    Username = "adminuser",
                    Email = "admin@example.com",
                    Password = "Admin1234!",
                    Role = "Admin",
                    FirstName = "Admin",
                    LastName = "User"
                },
                new
                {
                    Username = "testuser",
                    Email = "test@example.com",
                    Password = "Test1234!",
                    Role = "User",
                    FirstName = "Test",
                    LastName = "User"
                }
            };

            foreach (var entry in usersToSeed)
            {
                if (!await _roleManager.RoleExistsAsync(entry.Role))
                    await _roleManager.CreateAsync(new IdentityRole(entry.Role));

                var existingUser = await _userManager.FindByNameAsync(entry.Username);
                if (existingUser == null)
                {
                    var newUser = new ApplicationUser
                    {
                        UserName = entry.Username,
                        Email = entry.Email,
                        FirstName = entry.FirstName,
                        LastName = entry.LastName,
                        EmailConfirmed = true
                    };

                    var result = await _userManager.CreateAsync(newUser, entry.Password);
                    if (result.Succeeded)
                        await _userManager.AddToRoleAsync(newUser, entry.Role);
                }
            }
        }
    }
}

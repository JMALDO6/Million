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
            const string username = "testuser";
            const string email = "test@example.com";
            const string password = "Test1234!";
            const string role = "User";

            if (!await _roleManager.RoleExistsAsync(role))
                await _roleManager.CreateAsync(new IdentityRole(role));

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                var newUser = new ApplicationUser
                {
                    UserName = username,
                    Email = email,
                    FirstName = "Test",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(newUser, password);
                if (result.Succeeded)
                    await _userManager.AddToRoleAsync(newUser, role);
            }
        }
    }
}

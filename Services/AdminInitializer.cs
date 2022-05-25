using Microsoft.AspNetCore.Identity;
using StackOverflow.Models;

namespace StackOverflow.Services
{
    public static class AdminInitializer
    {
        public static async void Initialize(IServiceProvider serviceProvider)
        {
            RoleManager<IdentityRole> roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            UserManager<ApplicationUser> userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
                await roleManager.CreateAsync(new IdentityRole("Admin"));

            if (await userManager.FindByEmailAsync("admin@gmail.com") == null)
            {
                ApplicationUser admin = new ApplicationUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin"
                };
                
                await userManager.CreateAsync(admin, "Password123!");
                string token = await userManager.GenerateEmailConfirmationTokenAsync(admin);
                await userManager.ConfirmEmailAsync(admin, token);
                await userManager.AddToRoleAsync(admin, "Admin");
            }
        }
    }
}

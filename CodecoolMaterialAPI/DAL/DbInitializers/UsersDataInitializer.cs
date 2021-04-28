using Microsoft.AspNetCore.Identity;

namespace CodecoolMaterialAPI.DAL.DbInitializers
{
    public static class UsersDataInitializer
    {
        public static void SeedData(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }

        public static void SeedUsers(UserManager<IdentityUser> userManager)
        {
            if (userManager.FindByNameAsync("user@mail.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "user@mail.com";
                user.Email = "user@mail.com";
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync(user, "User123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "user").Wait();
                }
            }


            if (userManager.FindByNameAsync("admin@mail.com").Result == null)
            {
                IdentityUser user = new IdentityUser();
                user.UserName = "admin@mail.com";
                user.Email = "admin@mail.com";
                user.EmailConfirmed = true;

                IdentityResult result = userManager.CreateAsync(user, "Admin123!").Result;

                if (result.Succeeded)
                {
                    userManager.AddToRoleAsync(user, "admin").Wait();
                }
            }
        }

        public static void SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            if (!roleManager.RoleExistsAsync("user").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "user";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }


            if (!roleManager.RoleExistsAsync("admin").Result)
            {
                IdentityRole role = new IdentityRole();
                role.Name = "admin";
                IdentityResult roleResult = roleManager.CreateAsync(role).Result;
            }
        }
    }
}

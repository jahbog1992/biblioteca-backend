using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Biblioteca.Entities;

namespace Biblioteca.Persistence
{
    public class UserDataSeeder
    {
        public static async Task Seed(IServiceProvider service)
        {
            var userManager = service.GetRequiredService<UserManager<BibliotecaUserIdentity>>();
            var roleManager = service.GetRequiredService<RoleManager<IdentityRole>>();
            //Creating roles
            var adminRole = new IdentityRole(Constants.RoleAdmin);

            if (!await roleManager.RoleExistsAsync(Constants.RoleAdmin))
                await roleManager.CreateAsync(adminRole);

            //Admin user
            var adminUser = new BibliotecaUserIdentity()
            {
                NombreCompleto = "SysAdmin",
                UserName = "SADMIN"
            };
            var result = await userManager.CreateAsync(adminUser, "*Admin1234*");
            adminUser = await userManager.FindByNameAsync(adminUser.UserName);
            if (adminUser is not null)
                await userManager.AddToRoleAsync(adminUser, Constants.RoleAdmin);
        }
    }
}

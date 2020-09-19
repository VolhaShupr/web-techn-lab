using lab.DAL.Data;
using lab.DAL.Entities;
using Microsoft.AspNetCore.Identity;
using System.Linq;
using System.Threading.Tasks;

namespace lab.Services
{
    public class DbInitializer
    {
        public static async Task Seed(ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            //создать БД, если она еще не создана 
            context.Database.EnsureCreated();

            //проверка наличия ролей
            if (!context.Roles.Any())
            {
                var roleAdmin = new IdentityRole
                {

                    Name = "admin",
                    NormalizedName = "admin"
                };
                //создать роль admin
                await roleManager.CreateAsync(roleAdmin);
            }

            //проверка наличия пользователей 
            if (!context.Users.Any())
            {
                //создать пользователя user@gmail.com 
                var user = new ApplicationUser
                {

                    Email = "user@gmail.com", UserName = "user@gmail.com"
                };
                await userManager.CreateAsync(user, "111111");

                //создать пользователя admin@gmail.com
                var admin = new ApplicationUser
                {
                    Email = "admin@gmail.com",
                    UserName = "admin@gmail.com"
                };

                await userManager.CreateAsync(admin, "111111");

                //назначить роль admin
                admin = await userManager.FindByEmailAsync("admin@gmail.com");

                await userManager.AddToRoleAsync(admin, "admin");
            }
        }

    }
}
